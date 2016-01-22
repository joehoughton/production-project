define([
  'jquery',
  'backbone',
  'backbone.radio',
  'jquery.cookie'
], function ($, Backbone, Radio) {
  'use strict';

  var alertChannel = Radio.channel('alert');

  // called on each http request and sets the error / success messages
  $(document).ajaxComplete(function (event, xhr) {
    var statusCode = xhr.status;

    if (statusCode === 401 || statusCode === 403) {
      Backbone.history.navigate('login', {trigger: true});
      alertChannel.trigger('error', 'Unauthorised Access');
    }

    if (statusCode === 400) {
      alertChannel.trigger('error', 'Unauthorised Access');
    }

    if (statusCode >= 500) {
      alertChannel.trigger('error', 'The server is not responding');
    }
  });
});