define([
  'backbone',
  'backbone.radio',
  'Marionette',
  './models/details',
  '../common/views/layout',
  './views/details',
  '../sidebar/views/sidebar'
], function (Backbone, Radio, Marionette, UserDetails, AccountLayout, DetailsView, SidebarView) {
  'use strict';

  return Marionette.Object.extend({
    initialize: function (options) {
      this.region = options.region;
      this.model = new UserDetails();
      this.fillRegions();
    },

    fillRegions: function () {
      this.show();
    },

    show: function () {
      var accountLayout = new AccountLayout();
      var detailsView = new DetailsView({
        model: this.model
      });

      Backbone.Validation.bind(detailsView);

      var that = this;

      this.model.fetch({silent: true}).success(function () {
        that.region.show(accountLayout);
        accountLayout.accountContent.show(detailsView);

        Backbone.Radio.channel('navigation').trigger('change', 'account-nav');

        var sidebarView = new SidebarView({
          active: 'mydetails'
        });
        accountLayout.sidebar.show(sidebarView);
      });
    }
  });
});