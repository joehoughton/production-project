define([
  'backbone',
  'Marionette',
  'hbs!../templates/layout'
], function (Backbone, Marionette, headerLayout) {
  'use strict';

  return Marionette.LayoutView.extend({
    template: headerLayout,
    regions: {
      nav: '#navbar'
    },

    events: {
      'click #home-nav': 'home'
    },

    home: function (e) {
      e.preventDefault();
      Backbone.history.navigate('account/organisation', {trigger: true});
    }
  });
});