﻿define([
  'jquery',
  'backbone',
  'Marionette'
], function ($, Backbone, Marionette) {
  'use strict';

  return Marionette.Object.extend({
    initialize: function () {
      this.logout();
    },

    logout: function () {
      $.ajax({
        type: 'DELETE',
        url: 'api/tokens'
      });
      // clear previous error messages
      this.alertChannel = Backbone.Radio.channel('alert');
      this.alertChannel.trigger('close');

      Backbone.history.navigate('login', {trigger: true});
    }
  });
});