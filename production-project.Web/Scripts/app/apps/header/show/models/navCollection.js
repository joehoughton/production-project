define([
  'backbone',
  './navItem'
], function (Backbone, navItem) {
  'use strict';

  return Backbone.Collection.extend({
    model: navItem
  });
});