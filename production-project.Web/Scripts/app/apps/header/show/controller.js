define([
  'Marionette',
  './views/layout',
  './views/navigation'
], function (Marionette, NavigationLayout, NavigationTemplate) {
  'use strict';

  return Marionette.Object.extend({
    initialize: function (options) {
      this.region = options.region;
      this.fillRegions();
    },

    fillRegions: function () {
      this.show();
    },

    show: function () {
      var navigationLayout = new NavigationLayout();
      var navigation = new NavigationTemplate();

      this.region.show(navigationLayout);
      navigationLayout.nav.show(navigation);
    }
  });
});