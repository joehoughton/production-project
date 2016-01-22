define(['hbs/handlebars'], function (Handlebars) {
  'use strict';

  Handlebars.registerHelper('isEqual', function (expectedValue, value) {
    return value === expectedValue;
  });
});