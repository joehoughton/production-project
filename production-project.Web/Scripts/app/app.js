define([
  'jquery',
  'bootstrap',
  'backbone',
  'Marionette',
  'backbone.radio',
  'apps/registry'
], function ($, Bootstrap, Backbone, Marionette, Radio, appRegistry) {
  'use strict';

  var ApplicationLayout = Marionette.LayoutView.extend({
    el: 'body',

    regions: {
      error :'#error-content',
      header: '#header-content',
      main: '#main-content'
    }
  });

  var Application = Marionette.Application.extend({
    initialize: function () {
      this.layout = new ApplicationLayout();
      setUpRequests();
    }
  });

  function setUpRequests() {
    // set initial login request to send form data with url parameters - not json
    Backbone.ajax = function () {
      if (arguments[0].data && arguments[0].contentType === "application/json") {
        arguments[0].data = $.param(JSON.parse(arguments[0].data));
        arguments[0].contentType = "application/x-www-form-urlencoded";
      }

      return Backbone.$.ajax.apply(Backbone.$, arguments);
    }

    // checks if a cookie has been set in localhost storage
    function cookieExists() {
      if (typeof $.cookie('accessToken') === 'undefined') {
        return false;
      }
      return true;
    }

    // set contentType when logging in to send data as form data, not json
    function setHeaderWithoutToken() {
      Backbone.ajax = function () {
        if (arguments[0].data && arguments[0].contentType === "application/json") {
          arguments[0].data = $.param(JSON.parse(arguments[0].data)); // url encode data
          arguments[0].contentType = "application/x-www-form-urlencoded";
        }

        return Backbone.$.ajax.apply(Backbone.$, arguments);
      }
    }

    // if cookie exists containing access token, add this to the header of each request 
    function setHeaderWithToken() {
      var cookie = $.cookie("accessToken");

      $(document).ajaxSend(function (event, jqxhr, settings) {
        jqxhr.setRequestHeader('Authorization', 'bearer ' + cookie);
      });
    }
    
    // modify existing options before each request is sent and before ajax calls are processed
    $.ajaxPrefilter(function (options, originalOptions, jqXHR) {
      if (!cookieExists()) { 
        setHeaderWithoutToken(); // not logged in, set header request to send form data
      } 
      else {
        setHeaderWithToken(); // logged in, add bearer token to each request
      }

      jqXHR.complete(function () {
        if (!cookieExists()) {
          setHeaderWithoutToken(); // if login fails - set content type to x-www-form-urlencoded and post data as url parameters
        }
        else {
          setHeaderWithToken(); // login successful - add bearer token to each request
        }
      });

    });
  }

  var app = new Application();

  app.on('before:start', function () {
    appRegistry.initApps();
  });

  app.on('start', function () {
    if (!Backbone.History.started) {
      Backbone.history.start();

      if (Backbone.history.fragment === '') {
        Backbone.history.navigate('login', {trigger: true});
      }
    }
  });

  app.channel.reply('region', function (name) {
    return app.layout.getRegion(name);
  });

  return app;
});