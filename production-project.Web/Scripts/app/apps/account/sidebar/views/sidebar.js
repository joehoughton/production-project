define([
  'jquery',
  'backbone',
  'Marionette',
  'hbs!../templates/sidebar'
], function ($, Backbone, Marionette, SidebarView) {
  'use strict';

  return Marionette.ItemView.extend({

    initialize: function (options) {
      this.active = options.active;
    },

    onShow: function () {
      this.ActiveNavItem();
    },

    tagName: 'ul',

    className: 'account-sidebar',

    template: SidebarView,

    events: {
      'click #organisation': 'OrganisationView',
      'click #mydetails': 'DetailsView',
      'click #users': 'UsersView',
      'click #bedtypes' : 'BedTypesView'
    },

    OrganisationView: function (e) {
      e.preventDefault();
      Backbone.history.navigate('account/organisation', {trigger: true});
    },

    DetailsView: function (e) {
      e.preventDefault();
      Backbone.history.navigate('account/details', {trigger: true});
    },

    UsersView: function (e) {
      e.preventDefault();
    },

    BedTypesView: function (e) {
      e.preventDefault();
    },

    ActiveNavItem: function () {
      var navItem = $('#' + this.active).parent();
      navItem.toggleClass('active');
    }
  });
});