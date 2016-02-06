define(['hbs/handlebars'], function (Handlebars) {
  'use strict';

  Handlebars.registerHelper('checkForRequests', function (activeClass, availableRequests) {
    for (var request in availableRequests) {
      if (request === activeClass) {
        if (availableRequests[activeClass] === false) {
          return true;
        } else {
          return false;
        }
      }
    }
  });
});