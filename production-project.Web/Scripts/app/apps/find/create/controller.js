define([
  'jquery',
  'backbone',
  'backbone.radio',
  'Marionette',
  './models/request',
  './views/create',
  '../common/views/layout'
], function ($, Backbone, Radio, Marionette, Request, CreateView, LayoutView) {
  'use strict';

  return Marionette.Object.extend({
    initialize: function (options) {
      this.search = options.model;
      this.model = new Request();
      this.region = options.region;
      this.fillRegions();
      Backbone.Radio.channel('navigation').trigger('change', 'find-nav');
    },

    setLayout: function () {
      this.layout = new LayoutView();
      this.region.show(this.layout);
    },

    fillRegions: function () {
      this.setLayout();
      this.show();
    },

    show: function () {
      this.createBooking();
    },

    createBooking: function () {
      var createView = new CreateView({
        model: this.model,
        search: this.search
      });

      Backbone.Validation.bind(createView);
      this.layout.booking.show(createView);
    }
  });
});