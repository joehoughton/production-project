define([
  'jquery',
  'bootstrap',
  'backbone',
  'Marionette',
  'backbone.radio',
  'apps/registry'
], function ($, Bootstrap, Backbone, Marionette, Radio, appRegistry) {
  'use strict';

  var ApplicationLayout = Marionette.LayoutView.extend({
    el: 'body',

    regions: {
      error :'#error-content',
      header: '#header-content',
      main: '#main-content'
    }
  });

  var Application = Marionette.Application.extend({
    initialize: function () {
      this.layout = new ApplicationLayout();
    }
  });

  var app = new Application();

  app.on('before:start', function () {
    appRegistry.initApps();
  });

  app.on('start', function () {
    if (!Backbone.History.started) {
      Backbone.history.start();

      if (Backbone.history.fragment === '') {
        Backbone.history.navigate('login', {trigger: true});
      }
    }
  });

  app.channel.reply('region', function (name) {
    return app.layout.getRegion(name);
  });

  return app;
});