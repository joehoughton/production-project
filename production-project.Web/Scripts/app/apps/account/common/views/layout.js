define([
  'jquery',
  'backbone',
  'backbone.radio',
  'Marionette',
  'hbs!../templates/layout'
], function ($, Backbone, Radio, Marionette, AccountLayout) {
  'use strict';

  var layoutView =  Marionette.LayoutView.extend({

    initialize: function (options) {
      this.layout = options.layout;
    },

    onShow: function () {
      var navigationChannel = Backbone.Radio.channel('navigation');
      navigationChannel.trigger('navigate', 'account');
    },

    template: AccountLayout,

    className: 'container',

    regions: {
      sidebar: '#sidebar-content',
      accountContent: '#manage-content'
    }
  });

  return layoutView;
});