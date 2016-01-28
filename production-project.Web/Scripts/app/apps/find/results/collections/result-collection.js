define([
  'backbone',
  '../models/result'
], function (Backbone, Result) {
  'use strict';

  return Backbone.Collection.extend({
    model: Result
  });
});