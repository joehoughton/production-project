define([
  'jquery',
  'backbone',
  'backbone.radio',
  'Marionette',
  'hbs!../templates/navigation'
], function ($, Backbone, Radio, Marionette, navigationTemplate) {
  'use strict';

  return Marionette.ItemView.extend({
    template: navigationTemplate,

    tagName: 'ul',

    className: 'nav navbar-nav',

    events: {
      'click #logout': 'logout',
      'click #account-nav': 'navigateToAccount',
      'click #find-nav': 'navigateToFind'
    },

    logout: function (e) {
      e.preventDefault();
      Backbone.history.navigate('logout', {trigger: true});
    },

    navigateToAccount: function (e) {
      e.preventDefault();
      Backbone.history.navigate('account/organisation', {trigger: true});
    },

    navigateToFind: function (e) {
      e.preventDefault();
      Backbone.history.navigate('find', {trigger: true});
    }

  });
});