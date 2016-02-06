define([
  'jquery',
  'underscore',
  'backbone',
  'backbone.radio',
  'Marionette',
  './list/controller'
], function ($, _, Backbone, Radio, Marionette, ListController) {
  'use strict';

  return Marionette.Object.extend({
    initialize: function () {
      this.setRegion();
      this.registerRoutes();
    },

    setRegion: function () {
      this.region = Radio.channel('global').request('region', 'main');
    },

    registerRoutes: function () {
      this.router = new Marionette.AppRouter({
        controller: this,

        appRoutes: {
          'bookings(/)': 'showBookings'
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

    showBookings: function () {
      return new ListController({
        region: this.region
      });
    }
  });
});