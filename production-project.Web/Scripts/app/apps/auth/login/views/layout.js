define([
  'jquery',
  'backbone',
  'backbone.radio',
  'Marionette',
  'hbs!../templates/view'
], function ($, Backbone, Radio, Marionette, loginTemplate) {
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
      this.alertChannel = Backbone.Radio.channel('alert');
      this.alertChannel.trigger('close');

      var username = $('#username').val();
      var password = $('#password').val();

      this.model.set({
        username: username,
        password: password
      });

      this.model.save(null, {
        success: function () {
          Backbone.history.navigate('/account/organisation', {trigger: true});
        }
      });
    }

  });
});

