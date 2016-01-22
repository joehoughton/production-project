define([
  'underscore',
  'Marionette',
  'apps/alert/alert.app',
  'apps/header/header.app',
  'apps/auth/auth.app',
  'apps/account/account.app'
], function (_, Marionette, AlertApp, HeaderApp, AuthApp, AccountApp) {
  'use strict';

  var AppRegistry = Marionette.Object.extend({
    initialize: function () {
      this.apps = [];
    },

    add: function (app) {
      if (!app || !app.name || !app.app) {
        throw new Error('registering an app requires properties {name: string}, {app: class definition}');
      }

      this.apps.push(app);
    },

    initApps: function () {
      this.apps.forEach(function (registered) {
        registered.instance = new registered.app();
      });
    }
  });

  var registry = new AppRegistry();

  registry.add({
    name: 'AlertApp',
    app: AlertApp
  });

  registry.add({
    name: 'HeaderApp',
    app: HeaderApp
  });

  registry.add({
    name: 'AuthApp',
    app: AuthApp
  });

  registry.add({
    name: 'AccountApp',
    app: AccountApp
  });

  return registry;
});