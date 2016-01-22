define([
  'jquery',
  'backbone',
  'backbone.radio',
  'Marionette',
  'jquery.cookie',
  'hbs!../templates/view'
], function ($, Backbone, Radio, Marionette, cookie, loginTemplate) {
  'use strict';

  return Marionette.LayoutView.extend({
    template: loginTemplate,

    events: {
      'click button': 'loginSubmitted',
      'submit': 'loginSubmitted'
    },

    loginSubmitted: function (e) {
      e.preventDefault();

      // clear previous error messages
      $('#error-content').empty();

      var username = $('#username').val();
      var password = $('#password').val();

      this.model.set({
        username: username,
        password: password
      });

      this.model.save(null, {
        success: function (response) {
          // store the returned bearer token in a cookie - set expiry to one day
          var accessToken = response.attributes.access_token;
          $.cookie("accessToken", accessToken, { expires: 1 });

          Backbone.history.navigate('/account/organisation', { trigger: true });
        }
      });
    }

  });
});


