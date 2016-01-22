define([
  'backbone'
], function (Backbone) {
  'use strict';

  return Backbone.Model.extend({
    initialize: function () { },

    urlRoot: '/api/users/current/details',

    validation: {
      name: [{
        required: true,
        minLength: 5
      }, {
        pattern: /^[a-zA-Z\s]+$/,
        msg: 'Name can only contain alphabet characters'
      }],

      email: [{
        required: true
      }, {
        pattern: 'email',
        msg: 'Please enter a valid email address'
      }],

      telephone: {
        required: true,
        pattern: 'digits',
        length: 11
      },

      mobile: {
        required: true,
        pattern: 'digits',
        length: 11
      }
    }
  });
});