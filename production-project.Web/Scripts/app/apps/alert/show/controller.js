define([
  'Marionette',
  'apps/alert/show/models/alert',
  'apps/alert/show/views/alert'
], function (Marionette, Error, ErrorView) {
  'use strict';

  return Marionette.Object.extend({
    initialize: function (options) {
      this.region = options.region;
      this.alert = options.alert;
      this.fillRegions();
    },

    fillRegions: function () {  
      this.show();
    },

    show: function () {
      var alert = new Error(this.alert);

      var errorView = new ErrorView({
        model: alert
      });

      if ($("#error-content").is(':empty')) {
        this.region.show(errorView);
      } else {
        errorView.render();
        $("#error-content").append(errorView.$el.html());
      }
    }
  });
});