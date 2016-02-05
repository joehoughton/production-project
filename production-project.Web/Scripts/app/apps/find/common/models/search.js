define([
  'backbone'
], function (Backbone) {
  'use strict';

  return Backbone.Model.extend({
    defaults: {
      gpLocation: 'L2 2AH',
      latitude: '',
      longitude: '',
      distance: 25,
      tier: 0,
      gender: 0,
      allAges: true,
      dob: ''
    },
    validation: {
      gender: [{
        required: true
      }, {
        pattern: 'digits',
        msg: 'Please select a gender'
      }, {
        oneOf: [0, 100, 200]
      }],

      dob: {
        fn: function (dob, attr, model) {
          if (!model.allAges) {
            if (!/^\d{1,2}\/\d{1,2}\/\d{4}$/.test(dob)) {
              return 'Please enter a valid DOB in dd/mm/yyyy format';
            }

            var splitDate = dob.split('/');
            var day = parseInt(splitDate[0], 10);
            var month = parseInt(splitDate[1] - 1, 10);
            var year = parseInt(splitDate[2], 10);

            if (year < 1000 || year > 3000 || month < 0 || month > 12) {
              return 'Please enter a valid DOB in dd/mm/yyyy format';
            }

            var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

            if (year % 400 === 0 || (year % 100 !== 0 && year % 4 === 0)) {
              monthLength[1] = 29;
            }

            var validDay = day > 0 && day <= monthLength[month];
            var today = new Date();
            var dobToDateFormat = new Date(year, month, day);

            if (dobToDateFormat > today || !validDay) {
              return 'Please enter a valid DOB in dd/mm/yyyy format';
            }
          }
        }
      },

      tier: {
        required: true,
        oneOf: [0, 100, 200, 300],
        pattern: 'digits',
        msg: 'Please select a bed tier'
      },

      distance: {
        required: true,
        oneOf: [15, 25, 50, 100, 300],
        pattern: 'digits'
      },
      gpLocation: {
        required: true,
        msg: 'Please enter a GP postcode'
      }
    }
  });
});

