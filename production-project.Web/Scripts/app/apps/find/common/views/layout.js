define([
  'backbone',
  'Marionette',
  'hbs!../templates/layout'
], function (Backbone, Marionette, layoutView) {
  'use strict';

  return Marionette.LayoutView.extend({
    template: layoutView,

    regions: {
      search: '#search-region'
    }
  });
});