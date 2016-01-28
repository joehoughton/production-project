define(['hbs/handlebars'], function (Handlebars) {
  'use strict';

  Handlebars.registerHelper('orCondition', function (v1, operator, v2, options) {
    return (v1 || v2) ? options.fn(this) : options.inverse(this);
  });
});