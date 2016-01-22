define([
  'backbone',
  'backbone.radio',
  'Marionette',
  './organisation/controller',
  './details/controller'
], function (Backbone, Radio, Marionette, OrganisationController, DetailsController) {
  'use strict';

  return Marionette.Object.extend({
    initialize: function () {
      this.setRegion();
      this.registerRoutes();
      this.setNavigation();
    },

    setRegion: function () {
      this.region = Radio.channel('global').request('region', 'main');
    },

    setNavigation: function () {
      var navigationChannel = Backbone.Radio.channel('navigation');
      navigationChannel.trigger('navigate');
    },

    registerRoutes: function () {
      this.router = new Marionette.AppRouter({
        controller: this,

        appRoutes: {
          'account/organisation(/)': 'organisation',
          'account/details(/)' : 'details'
        }
      });
    },

    organisation: function () {
      return new OrganisationController({
        region: this.region
      });
    },

    details: function () {
      return new DetailsController({
        region: this.region
      });
    }
  });
});