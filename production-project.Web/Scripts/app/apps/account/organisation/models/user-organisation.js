define([
  'backbone',
  'backbone.radio'
], function (Backbone) {
  'use strict';

  return Backbone.Model.extend({
    urlRoot: '/api/users/current/organisation'
  });
});