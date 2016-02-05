define([
  'backbone'
], function (Backbone) {
  'use strict';

  var Patient = Backbone.Model.extend({
    defaults: {
      age: 130,
      gender: 100
    }
  });

  return Backbone.Model.extend({
    initialize: function () {
      this.set('patient', new Patient());
    },
    validation: {
      patient: {
        fn: function (patient) {
          var age = patient.attributes.age;

          if (isNaN(age)) {
            return 'Patient age is required';
          }

          if (age < 0 || age >= 130) {
            return 'Patient age can only be between 0 - 130, you entered ' + age;
          }

          var pasId = patient.attributes.pasId.toString();

          if (pasId.length < 10) {
            return 'Patient Identifier with 10 characters is required';
          }

          if (pasId.length > 10) {
            return 'Patient Identifier is too long, only 10 characters are required';
          }
        }
      }
    }
  });
});

