define([
  'Marionette',
  'jquery',
  'backbone',
  'backbone.radio',
  'hbs!../templates/request-table',
  'hbs!../templates/request-row'
], function (Marionette, $, Backbone, Radio, requestTable, requestRow) {
  'use strict';

  var requestRowView = Marionette.ItemView.extend({
    template: requestRow,
    tagName: 'tr'
  });

  return Marionette.CompositeView.extend({
    initialize: function (options) {
      this.options = options;
      this.bookingRequestChannel = Backbone.Radio.channel('bookingRequest');
    },

    template: requestTable,
    childView: requestRowView,
    childViewContainer: 'tbody',
    className: 'container',
    reorderOnSort: true,
    onShow: function () {
      this.active = this.options.templateHelpers.active;
      this.availableRequests = this.options.templateHelpers.availableRequests;
      this.setActiveFilter();
    },

    setActiveFilter: function () {
      var $active = $('#' + this.active);
      $active.addClass('active');
    },

    events: {
      'click #all': function () {
        this.filterRequests('all');
      },
      'click #outgoing': function () {
        this.filterRequests('outgoing');
      },
      'click #incoming': function () {
        this.filterRequests('incoming');
      },
      'click #date': 'dateSort',
      'click #ward': 'wardSort',
      'click #id': 'idSort',
      'click #status': 'statusSort'
    },

    filterRequests: function (filterBy) {
      Backbone.Radio.channel('filterStatus').trigger('filter', filterBy);
    },

    dateSort: function () {
      var $dateSortIcon = $('#date-sort-icon');
      $dateSortIcon.toggleClass('fa-chevron-down fa-chevron-up');
      this.bookingRequestChannel.trigger('sort', 'dateRequested');
      this.collection.setSortField('dateRequested');
      this.collection.sort();
    }
  });
});

