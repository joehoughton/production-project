define([
  'jquery',
  'backbone',
  'backbone.radio',
  'Marionette',
  '../models/user-organisation',
  'hbs!../templates/view'
], function ($, Backbone, Radio, Marionette, UserOrganisation, userOrganiationTemplate) {
  'use strict';

  return Marionette.LayoutView.extend({
    initialize: function (options) { },

    className: 'details-view',

    template: userOrganiationTemplate
  });
});