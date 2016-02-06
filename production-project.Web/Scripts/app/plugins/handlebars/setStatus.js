define(['hbs/handlebars'], function (Handlebars) {
  'use strict';

  Handlebars.registerHelper('setStatus', function (status) {
    var statusClass = '';

    switch (status) {

      case 'Requested':
        statusClass = 'info';
        break;

      case 'Accepted':
        statusClass = 'success';
        break;

      case 'Reserved':
        statusClass = 'warning';
        break;

      case 'Rejected':
        statusClass = 'important';
        break;
      default:
        statusClass = 'inverse';
        break;
    }

    return statusClass;
  });
});