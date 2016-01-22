define([
  'jquery',
  'backbone',
  'Marionette',
  'hbs!../templates/details'
], function ($, Backbone, Marionette, DetailsTemplate) {
  'use strict';

  return Marionette.LayoutView.extend({
    initialize: function (options) {
      this.layout = options.layout;
    },

    className: 'details-view',

    template: DetailsTemplate,

    events: {
      'click #cancelChanges': 'cancelChanges',
      'submit #user-detail-form': 'saveChanges',
      'click #apply' : 'saveChanges'
    },

    cancelChanges: function (e) {
      e.preventDefault();
      this.render();
    },

    saveChanges: function (e) {
      e.preventDefault();

      $('#error-content').empty(); // clear previous error messages

      var name = $('#name').val();
      var email = $('#email').val();
      var emailNotification = $('#email-notification').prop('checked');
      var telephone = $('#telephone').val();
      var mobile = $('#mobile').val();
      var smsNotification = $('#sms-notification').prop('checked');

      this.model.set({
        name: name,
        email: email,
        emailNotification: emailNotification,
        telephone: telephone,
        mobile: mobile,
        smsNotification: smsNotification
      });

      this.model.save(null, {
        dataType: 'text',
        success: function () {
          $('#error-content').empty();
          Backbone.Radio.channel('alert').trigger('success', 'Account details have been saved');
        },
        error: function () {
          $('#error-content').empty(); // clear previous error messages
          Backbone.Radio.channel('alert').trigger('warning', 'Server failed to update user details');
        }
      });
    }
  });
});