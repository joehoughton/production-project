define([
  'jquery',
  'backbone',
  'backbone.radio',
  'Marionette',
  'hbs!../templates/create'
], function ($, Backbone, Radio, Marionette, createView) {
  'use strict';

  return Marionette.ItemView.extend({
    initialize: function (options) {
      this.options = options;
      this.alertChannel = Backbone.Radio.channel('alert');
      this.search = this.options.search;
      this.setModelAttributes(this.search);
    },

    onShow: function () {
      $('#pasId').focus();
    },

    template: createView,

    className: 'container',

    events: {
      'submit #create-booking': 'createBooking',
      'click #create-booking': 'createBooking',
      'click #cancelChanges': 'cancelChanges',
      'click .icon': 'returnToResults'
    },

    cancelChanges: function (e) {
      e.preventDefault();
      this.render();
    },

    setModelAttributes: function (search) {
      this.model.get('patient').set({
        age: search.get('age'),
        gender: search.get('gender')
      });

      this.model.set({
        organisationId: search.get('bed').organisationId,
        bedId: search.get('bed').id,
        price: search.get('bed').price,
        organisationName: search.get('bed').organisationName,
        organisationType: search.get('bed').organisationType,
        name: search.get('bed').name,
        emails: search.get('bed').organisationContact,
        tier: search.get('bed').tier,
        allAges: search.get('allAges')
      });
    },

    createBooking: function (e) {
      e.preventDefault();

      var pasId = $('#pasId').val();
      var price = parseInt($('#price').val());
      var name = $('#name').val();
      var notes = $('#notes').val();
      var clinicalInformation = $('#clinical-information').is(':checked');

      this.model.get('patient').set({
        pasId: pasId
      }, {validate: true});

      this.model.set({
        price: price,
        name: name,
        notes: notes,
        clinicalInformation: clinicalInformation
      }, {validate: true});

      if (this.model.isValid(true)) {
        var that = this;

        $.ajax({
          url: '/api/bookings/',
          dataType: 'json',
          type: 'post',
          contentType: 'application/json',
          data: JSON.stringify(this.model.toJSON()),
          processData: false
        }).done(function () {
          that.alertChannel.trigger('success', 'Your booking request has been sent');
          Backbone.history.navigate('bookings', {trigger: true});
        }).error(function () {
          that.alertChannel.trigger('warning', 'Your booking request has failed, please try again');
        });
      }
    },
    returnToResults: function () {
      Backbone.history.loadUrl();
    }
  });
});