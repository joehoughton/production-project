/*global google */

define([
  'jquery',
  'backbone',
  'backbone.radio',
  'Marionette',
  '../../account/organisation/models/user-organisation',
  '../common/views/layout',
  './views/results-list',
  './collections/result-collection',
  '../common/map.options',
  'async!http://maps.google.com/maps/api/js?sensor=false'
], function ($, Backbone, Radio, Marionette, UserOrganisation, LayoutView, ResultsListView, ResultCollection, mapOptions) {
  'use strict';

  return Marionette.Object.extend({
    initialize: function (options) {
      this.model = options.model;
      this.searchForBeds = options.searchForBeds;
      this.region = options.region;
      this.fillRegions();
      this.alertChannel = Backbone.Radio.channel('alert');
      this.alertChannel.trigger('close');
      Backbone.Radio.channel('navigation').trigger('change', 'find-nav');
    },

    setHomeLocation: function () {
      var that = this;
      this.userOrganisation = new UserOrganisation();

      this.userOrganisation.fetch({silent: true}).success(function () {
        var latitude = that.userOrganisation.get('latitude');
        var longitude = that.userOrganisation.get('longitude');
        that.addHomeMarker(latitude, longitude);
        that.setFocus(latitude, longitude);
      });
    },

    setGpLocation: function () {
      var that = this;
      var gpsLocation = encodeURIComponent(this.model.get('gpLocation'));

      $.get('https://maps.googleapis.com/maps/api/geocode/json?address=' + gpsLocation, function (response) {
        if (response.results[0] !== undefined) {
          var latitude = response.results[0].geometry.location.lat;
          var longitude = response.results[0].geometry.location.lng;
          that.addMarker(latitude, longitude);
          that.model.set({latitude: latitude, longitude: longitude});
        }

        that.showSearch();
      });
    },

    setLayout: function () {
      this.layout = new LayoutView({
        model: this.model
      });

      Backbone.Validation.bind(this.layout);
      this.region.show(this.layout);
    },

    showSearch: function () {
      var that = this;

      this.resultsView = new ResultsListView({
        model: that.model,
        gpLocation: that.model.get('gpLocation')
      });

      Backbone.Validation.bind(that.resultsView);
      that.layout.search.show(that.resultsView);

      if (that.searchForBeds === true) {
        that.search();
      }
    },

    fillRegions: function () {
      this.setLayout();
      this.show();
    },

    show: function () {
      this.initMap();
      this.setHomeLocation();
      this.setGpLocation();
    },

    initMap: function () {
      this.map = new google.maps.Map(document.getElementById('map-region'), mapOptions);
      this.markers = [];
    },

    setFocus: function (lat, lng) {
      this.map.setCenter(new google.maps.LatLng(lat, lng));
    },

    addMarker: function (lat, lng) {
      var marker = new google.maps.Marker({
        position: new google.maps.LatLng(lat, lng),
        map: this.map,
        icon: '/Scripts/img/gp-location.png',
        optimized: false,
        title: this.model.get('gpLocation')
      });

      this.markers.push(marker);
    },

    addHomeMarker: function (lat, lng) {
      var marker = new google.maps.Marker({
        position: new google.maps.LatLng(lat, lng),
        map: this.map,
        icon: '/Scripts/img/home-location.png',
        optimized: false,
        title: this.userOrganisation.get('name')
      });

      this.markers.push(marker);
    },

    addGPMarker: function (lat, lng, organisationId, bedId) {
      var that = this;
      that.position = new google.maps.LatLng(lat, lng);
      var addMarker = true;

      $.each(that.markers, function (index, mapMarker) {
        if (JSON.stringify(mapMarker.position) === JSON.stringify(that.position)) {
          addMarker = false;

          return false;
        }
      });

      if (addMarker) {
        var marker = new google.maps.Marker({
          position: new google.maps.LatLng(lat, lng),
          organisationId: organisationId,
          bedId: bedId,
          map: this.map,
          icon: '/Scripts/img/bed-location.png' + '#' + organisationId,
          optimized: false
        });

        that.addMarkerListener(marker);
        that.markers.push(marker);
      }
    },

    addMarkerListener: function (marker) {
      var that = this;
      google.maps.event.addListener(marker, 'click', function () {
        $.each(that.markers, function (index, marker) {
          if (marker.organisationId) {
            $('div').find('[data-id="' + marker.organisationId + '"]').removeClass('active');
            marker.setIcon('/Scripts/img/bed-location.png' + '#' + marker.organisationId);
          }
        });

        $('div').find('[data-id="' + marker.organisationId + '"]').addClass('active');
        marker.setIcon('/Scripts/img/bed-location-active.png' + '#' + marker.organisationId);
      });
    },

    refreshMarkers: function (map) {
      for (var i = 0; i < this.markers.length; i++) {
        this.markers[i].setMap(map);
      }

      var bounds = new google.maps.LatLngBounds();

      for (i = 0; i < this.markers.length; i++) {
        bounds.extend(this.markers[i].getPosition());
      }

      this.map.fitBounds(bounds);
      this.map.setZoom(this.map.getZoom() - 2);
    },

    clearMarkers: function () {
      this.refreshMarkers(null);
      this.markers = [];
    },

    search: function () {
      var that = this;
      var alertChannel = Backbone.Radio.channel('alert');

      if (this.model.isValid(true)) {
        $.ajax({
          url: '/api/organisations/search/',
          dataType: 'json',
          type: 'post',
          contentType: 'application/json',
          data: JSON.stringify(this.model.toJSON()),
          processData: false
        }).done(function (response) {
          that.results = response;
          that.showResults();
        }).error(function () {
          that.results = undefined;
          that.showResults();
          alertChannel.trigger('warning', 'No beds could be found with your requirements');
        });
      } else {
        alertChannel.trigger('warning', 'Invalid search parameters provided');
      }
    },

    showResults: function () {
      var that = this;

      if (this.results !== undefined) {
        $(this.results).each(function () {
          that.addGPMarker(this.latitude, this.longitude, this.organisationId, this.bedId);
        });
        this.refreshMarkers(this.map);
      }

      var resultCollection = new ResultCollection(this.results);

      var resultsView = new ResultsListView({
        model: this.model,
        collection: resultCollection,
        gpLocation: this.model.get('gpLocation')
      });

      Backbone.Validation.bind(resultsView);
      this.layout.search.show(resultsView);
    }
  });
});