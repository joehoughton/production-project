define([
  'backbone'
], function (Backbone) {
  'use strict';

  return Backbone.Model.extend({
    urlRoot: '/token',

    defaults: {
      username: '',
      password: '',
      client_id: 'sparebedsApp',
      grant_type: 'password'
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