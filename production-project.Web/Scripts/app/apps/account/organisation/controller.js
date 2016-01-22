define([
  'jquery',
  'backbone',
  'backbone.radio',
  'Marionette',
  './models/user-organisation',
  '../common/views/layout',
  './views/view',
  '../sidebar/views/sidebar'
], function ($, Backbone, Radio, Marionette, UserOrganisation, AccountLayout, OrganisationView, SidebarView) {
  'use strict';

  return Marionette.Object.extend({
    initialize: function (options) {
      this.region = options.region;
      this.fillRegions();
      Backbone.Radio.channel('navigation').trigger('change', 'account-nav');
    },

    fillRegions: function () {
      this.show();
    },

    show: function () {
      // clear previous error messages
      $('#error-content').empty();

      this.model = new UserOrganisation();
      var accountLayout = new AccountLayout();
      this.region.show(accountLayout);

      var organisationView = new OrganisationView({
        model: this.model
      });

      this.model.fetch({silent: true}).success(function () {
        var sidebarView = new SidebarView({
          active: 'organisation'
        });

        accountLayout.sidebar.show(sidebarView);
        accountLayout.accountContent.show(organisationView);
      });
    }
  });
});