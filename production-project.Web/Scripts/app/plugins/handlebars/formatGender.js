define(['hbs/handlebars'], function (Handlebars) {
  'use strict';

  Handlebars.registerHelper('formatGender', function (gender) {
    return gender === 100 ? 'Male' : 'Female';
  });
});