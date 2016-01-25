define([
  'backbone'
], function (Backbone) {
  'use strict';

  return Backbone.Model.extend({
    urlRoot: '/api/tokens',

    defaults: {
      username: '',
      password: ''
    },

    validation: {
      username: {
        required: true
      },

      password: {
        required: true
      }
    }
  });
});