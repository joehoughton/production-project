define([
  'underscore',
  'backbone',
  'backbone.radio',
  '../models/request'
], function (_, Backbone, Radio, Request) {
  'use strict';

  return Backbone.Collection.extend({
    url: '/api/bookings',
    model: Request,

    initialize: function () {
      this.sortField = 'dateRequested';
      this.order = 'ASC';
    },

    setSortField: function (field) {
      this.sortField = field;
      this.order = this.order === 'ASC' ? 'DESC' : 'ASC';
    },

    comparator: function (model) {
      return model.get(this.sortField);
    },

    sortBy: function (iterator, context) {
      var models = this.models;
      var mappedModels = this.mapModels(iterator, context, models);
      var sortedModels = this.sortModels(mappedModels);

      return _.pluck(sortedModels, 'valueItem');
    },

    mapModels: function (iterator, context, models) {
      var mappedModels = _.map(models, function (value, index, list) {
        return {
          valueItem: value,
          index: index,
          criteria: iterator.call(context, value, index, list)
        };
      });

      return mappedModels;
    },

    sortModels: function (mappedModels) {
      var that = this;
      var sortedModels = mappedModels.sort(function (left, right) {
        var a = that.order === 'ASC' ? left.criteria : right.criteria;
        var b = that.order === 'ASC' ? right.criteria : left.criteria;

        if (a !== b) {
          if (a > b || a === void 0) {
            return 1;
          }

          if (a < b || b === void 0) {
            return -1;
          }
        }

        return left.index < right.index ? -1 : 1;
      });

      return sortedModels;
    }
  });
});