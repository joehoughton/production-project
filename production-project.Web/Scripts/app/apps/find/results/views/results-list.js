define([
  'jquery',
  'backbone',
  'backbone.radio',
  'Marionette',
  'hbs!../templates/result-item',
  'hbs!../templates/result-list',
  'datepicker'
], function ($, Backbone, Radio, Marionette, resultItem, resultList) {
  'use strict';

  var resultItemView = Marionette.ItemView.extend({
    template: resultItem,
    tagName: 'li'
  });

  return Marionette.CompositeView.extend({
    initialize: function (options) {
      this.alertChannel = Backbone.Radio.channel('alert');
      this.gpLocation = options.gpLocation;
    },

    onShow: function () {
      this.initialiseDob();
    },

    template: resultList,
    childView: resultItemView,
    className: 'container',
    childViewContainer: 'ul',

    events: {
      'click .icon': 'returnToFilter',
      'click .create-booking': 'createBooking',
      'click #find-availability': 'search',
      'click #return': 'goBack'
    },

    goBack: function (e) {
      e.preventDefault();
      window.location = '#find/' + this.model.get('gpLocation');
    },

    returnToFilter: function () {
      window.location = '#find/' + this.gpLocation;
    },

    initialiseDob: function () {
      var today = new Date();

      $('.input-group.date').datepicker({autoclose: true});

      $('#dob-daterpicker').datepicker({
        onRender: function (date) {
          return date.valueOf() > today.valueOf() ? 'disabled' : '';
        }
      });

      $('#dob').on('keyup', function () {
        if ($('#dob').val()) {
          $('#allAges').prop('checked', false);
        } else {
          $('#allAges').prop('checked', true);
        }
      });

      $('#dob-daterpicker').datepicker().on('changeDate', function () {
        if ($('#dob').val()) {
          $('#allAges').prop('checked', false);
        } else {
          $('#allAges').prop('checked', true);
        }
      });
    },
    createBooking: function (e) {
      e.preventDefault();
      var organisationId = $(e.target).data('organisationid');
      var bedid = $(e.target).data('bedid');

      var age = 0;
      var allAges = $('#allAges').is(':checked');

      if (!allAges) {
        var dob = $('#dob').val();
        age = this.calculateAge(dob);
      }

      if (!isNaN(age)) {
        this.model.set({
          age: age
        }, {validate: true});
      }

      Backbone.Radio.channel('navigation').trigger('createBooking', organisationId, bedid);
    },

    search: function (e) {
      e.preventDefault();
      var gender = parseInt($('#gender option:selected').val());
      var distance = parseInt($('#distance option:selected').val());
      var tier = parseInt($('#tier option:selected').val());
      var gpLocation = $('#gpLocation').val();
      var allAges = $('#allAges').is(':checked');
      var dob = $('#dob').val();

      if (!allAges) {
        var age = this.calculateAge(dob);

        if (!isNaN(age)) {
          this.model.set({
            age: age
          }, {validate: true});
        }
      }

      this.model.set({
        dob: dob,
        gender: gender,
        distance: distance,
        tier: tier,
        gpLocation: gpLocation,
        allAges: allAges
      }, {validate: true});

      if (this.model.isValid()) {
        if (this.model.get('allAges')) {
          window.location = '#find/' + this.model.get('gpLocation') + '/' + 'all_ages=' + this.model.get('allAges') + '&gender=' + this.model.get('gender') + '&distance=' + this.model.get('distance') + '&tier=' + this.model.get('tier');
        } else {
          window.location = '#find/' + this.model.get('gpLocation') + '/' + 'age=' + this.model.get('age') + '&gender=' + this.model.get('gender') + '&distance=' + this.model.get('distance') + '&tier=' + this.model.get('tier');
        }
      }
    },

    calculateAge: function (dob) {
      var day = parseInt(dob.split('/')[0]);
      var month = parseInt(dob.split('/')[1]) - 1;
      var year = parseInt(dob.split('/')[2]);
      var today = new Date();
      var birthDate = new Date(year, month, day);

      var age = today.getFullYear() - birthDate.getFullYear();
      var currentMonth = today.getMonth() - birthDate.getMonth();

      if (currentMonth < 0 || (currentMonth === 0 && today.getDate() < birthDate.getDate())) {
        age--;
      }

      return age;
    }
  });
});
