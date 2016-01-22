define([
  'backbone',
  'backbone.radio',
  'Marionette',
  './models/credentials',
  './views/layout'
], function (Backbone, Radio, Marionette, Credentials, LoginTemplate) {
  'use strict';

  return Marionette.Object.extend({
    initialize: function (options) {
      this.region = options.region;
      this.model = new Credentials();
      this.fillRegions();
      this.hideNavigation();
    },

    hideNavigation: function () {
      var navigationChannel = Backbone.Radio.channel('navigation');
      navigationChannel.trigger('navigate', 'login');
    },

    fillRegions: function () {
      this.show();
    },

    show: function () {
      var view = new LoginTemplate({
        model: this.model
      });

      Backbone.Validation.bind(view);
      this.region.show(view);
    }
  });
});