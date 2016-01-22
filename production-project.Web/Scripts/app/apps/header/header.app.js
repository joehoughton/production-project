define([
  'jquery',
  'backbone',
  'backbone.radio',
  'Marionette',
  'apps/header/show/controller'
], function ($,Backbone, Radio, Marionette, HeaderController) {
  'use strict';

  return Marionette.Object.extend({
    initialize: function () {
      this.setRegion();
      this.setListener();
    },

    setListener: function () {
      var navigation = Backbone.Radio.channel('navigation');
      var that = this;

      navigation.on('navigate', function (route) {
        if (route !== 'login') {
          that.show();
        } else {
          that.hide();
        }
      });

      navigation.on('change', function (navigationId) {
        that.removeActiveClasses();
        $('#' + navigationId).addClass('active');
      });
    },

    removeActiveClasses: function () {
      $('.nav a').removeClass('active');
    },

    setRegion: function () {
      this.region = Radio.channel('global').request('region', 'header');
    },

    show: function () {
      return new HeaderController({
        region: this.region
      });
    },

    hide: function () {
      this.region.empty();
    }
  });
});