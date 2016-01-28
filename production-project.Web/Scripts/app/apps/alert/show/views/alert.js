﻿define([
  'jquery',
  'backbone',
  'backbone.radio',
  'Marionette',
  'hbs!../templates/alert'
], function ($, Backbone, Radio, Marionette, ErrorTemplate) {
  'use strict';

  return Marionette.ItemView.extend({
    template: ErrorTemplate,

    onRender: function () {
      this.$el.fadeIn();
      this.$el.delay(3000).fadeOut();
    }
  });
});