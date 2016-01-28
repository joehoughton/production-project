define([
  'jquery',
  'underscore',
  'backbone',
  'backbone.radio',
  'Marionette',
  './results/controller',
  './common/models/search'
], function ($, _, Backbone, Radio, Marionette, ResultsController, Search) {
  'use strict';

  return Marionette.Object.extend({
    initialize: function () {
      this.setRegion();
      this.registerRoutes();
      this.model = new Search();
      this.setListeners();
    },

    setListeners: function () {
      var that = this;
      this.navigation = Backbone.Radio.channel('navigation');

      this.navigation.on('createBooking', function (organisationId, bedId) {
        that.createBooking(organisationId, bedId);
      });
    },

    setRegion: function () {
      this.region = Radio.channel('global').request('region', 'main');
    },

    registerRoutes: function () {
      this.router = new Marionette.AppRouter({
        controller: this,

        appRoutes: {
          'find(/)': 'results',
          'find/:gpLocation/:queryString': 'results'
        }
      });
    },

    parseUrl: function (queryString) {
      var params = {};

      if (queryString) {
        _.each(
            _.map(decodeURI(queryString).split(/&/g), function (el) {
              var aux = el.split('='),
                o = {};

              if (aux.length >= 1) {
                var val = null;

                if (aux.length === 2) {
                  val = aux[1];
                  o[aux[0]] = val;
                }
              }

              return o;
            }),

            function (o) {
              _.extend(params, o);
            }
        );
      }

      return params;
    },

    results: function (gpLocation, queryString) {
      this.searchForBeds = false;

      if (queryString !== undefined) {
        this.searchForBeds = true;
        var urlParams = this.parseUrl(queryString);

        var allAges = urlParams.all_ages;

        if (!allAges) {
          var age = parseInt(urlParams.age);
          this.model.set({age: age});
        }

        var gender = parseInt(urlParams.gender);
        var distance = parseInt(urlParams.distance);
        var tier = parseInt(urlParams.tier);
        this.model.set({gpLocation: gpLocation, gender: gender, distance: distance, tier: tier});
      }

      return new ResultsController({
        region: this.region,
        model: this.model,
        searchForBeds: this.searchForBeds
      });
    }

  });
});