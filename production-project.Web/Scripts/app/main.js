(function () {
  'use strict';

  require.config({
    paths: {
      async: '../lib/bower_components/requirejs-plugins/src/async',
      jquery: '../lib/bower_components/jquery/dist/jquery',
      bootstrap: '../lib/bower_components/bootstrap-sass-official/assets/javascripts/bootstrap',
      underscore: '../lib/bower_components/underscore/underscore',
      backbone: '../lib/bower_components/backbone/backbone',
      'backbone.radio': '../lib/bower_components/backbone.radio/build/backbone.radio.min',
      'backbone.validation': '../lib/bower_components/backbone-validation/dist/backbone-validation-amd',
      Marionette: '../lib/bower_components/marionette/lib/backbone.marionette',
      hbs: '../lib/bower_components/require-handlebars-plugin/hbs',
      datepicker: '../lib/bootstrap-datepicker'
    },

    hbs: {
      templateExtension: 'html',
      helpers: true,
      helperPathCallback: function (name) {
        return 'plugins/handlebars/' + name;
      }
    },

    deps: [
      'config/marionette',
      'config/ajax.request',
      'config/validation',
      'backbone.validation'
    ],

    shim: {
      jquery: {
        exports: '$'
      },
      bootstrap: {
        deps: ['jquery']
      },
      datepicker: {
        deps: ['jquery']
      }

    }
  });

  require([
    'app'
  ], function (app) {
    app.start();
  });
}());