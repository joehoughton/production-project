define([
  'jquery',
  'backbone',
  'backbone.radio',
  'Marionette',
  './views/booking',
  './collections/request-collection'
], function ($, Backbone, Radio, Marionette, RequestView, RequestCollection) {
  'use strict';

  return Marionette.Object.extend({
    initialize: function (options) {
      this.region = options.region;
      this.requests = new RequestCollection();
      this.fillRegions();
      this.setListeners();
      Backbone.Radio.channel('navigation').trigger('change', 'bookings-nav');
    },

    setListeners: function () {
      var that = this;
      this.filterStatus = Backbone.Radio.channel('filterStatus');

      this.filterStatus.on('filter', function (filterBy) {
        that.filter(filterBy);
      });
    },

    setLayout: function () {
      this.layout = new RequestView({collection: this.requests, templateHelpers: {active: 'all'}});
    },

    fillRegions: function () {
      this.setLayout();
      this.show();
    },

    show: function () {
      this.listBookings();
    },

    listBookings: function () {
      var that = this;
      var incoming = false;
      var outgoing = false;
      var all = false;

      this.requests.fetch().success(function () {
        that.requests.each(function (model) {
          if (model.get('incoming')) {
            incoming = true;
          }

          if (model.get('incoming') === false) {
            outgoing = true;
          }

          if (incoming || outgoing) {
            all = true;
          }
        });

        that.availableRequests = {'incoming': incoming, 'outgoing': outgoing, 'all': all};
        that.layout.options.templateHelpers.availableRequests = {'incoming': incoming, 'outgoing': outgoing, 'all': all};
        that.region.show(that.layout);
      });
    },

    filter: function (filterBy) {
      var that = this;
      var filteredRequests;

      switch (filterBy) {

        case 'incoming':
          filteredRequests = new RequestCollection(that.requests.where({incoming: true}));
          break;

        case 'outgoing':
          filteredRequests = new RequestCollection(that.requests.where({incoming: false}));
          break;

        case 'all':
          filteredRequests = that.requests;
          break;
        default:
          filteredRequests = that.requests;
          break;
      }

      var requestView = new RequestView({collection: filteredRequests, templateHelpers: {active: filterBy, availableRequests: that.availableRequests}});
      that.region.show(requestView);
    }
  });
});