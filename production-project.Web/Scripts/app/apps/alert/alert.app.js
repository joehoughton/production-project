define([
  'backbone',
  'backbone.radio',
  'Marionette',
  'apps/alert/show/controller'
], function (Backbone, Radio, Marionette, ErrorController) {
  'use strict';

  return Marionette.Object.extend({
    initialize: function () {
      this.setRegion();
      this.setListeners();
    },

    setRegion: function () {
      this.region = Radio.channel('global').request('region', 'error');
    },

    setListeners: function () {
      var alertChannel = Backbone.Radio.channel('alert');
      var that = this;

      alertChannel.on('warning', function (message) {
        that.show(message, 'warning');
      });

      alertChannel.on('success', function (message) {
        that.show(message, 'success');
      });

      alertChannel.on('error', function (message) {
        that.show(message, 'error');
      });

      alertChannel.on('close', function () {
        that.region.empty();
      });
    },

    show: function (message, type) {
      return new ErrorController({
        region: this.region,
        alert: {
          type: type,
          message: message
        }
      });
    }
  });
});