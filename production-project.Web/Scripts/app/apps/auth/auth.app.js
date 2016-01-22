define([
  'backbone.radio',
  'Marionette',
  'apps/auth/login/controller',
  'apps/auth/logout/controller'
], function (Radio, Marionette, LoginController, LogoutController) {
  'use strict';

  return Marionette.Object.extend({
    initialize: function () {
      this.setRegion();
      this.registerRoutes();
    },

    setRegion: function () {
      this.region = Radio.channel('global').request('region', 'main');
    },

    registerRoutes: function () {
      this.router = new Marionette.AppRouter({
        controller: this,

        appRoutes: {
          'login(/)': 'login',
          'logout(/)': 'logout'
        }
      });
    },

    login: function () {
      return new LoginController({
        region: this.region
      });
    },
    logout: function () {
      return new LogoutController();
    }
  });
});