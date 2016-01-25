define([
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

      Backbone.history.navigate('login', {trigger: true});
    }
  });
});