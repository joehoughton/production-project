define([
  'jquery',
  'underscore',
  'backbone',
  'backbone.radio',
  'backbone.validation'
], function ($, _, Backbone) {
  'use strict';

  var alertChannel = Backbone.Radio.channel('alert');

  Backbone.Validation.configure({
    selector: 'id'
  });

  _.extend(Backbone.Validation.callbacks, {
    getInput: function (view, attr) {
      return view.$el.find('#' + attr);
    },

    valid: function (view, attr) {
      var $input = this.getInput(view, attr);
      $input.removeClass('warning');
    },

    invalid: function (view, attr, error) {
      // get input field
      var $input = this.getInput(view, attr);
      // get label for input field
      var $label = $('label[for="' + $($input).attr('id') + '"]');

      $label.addClass('warning-label');
      $input.addClass('warning');

      alertChannel.trigger('warning', error);
    }
  });
});