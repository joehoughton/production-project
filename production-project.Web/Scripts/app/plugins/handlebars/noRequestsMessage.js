define(['hbs/handlebars'], function (Handlebars) {
  'use strict';

  Handlebars.registerHelper('noRequestsMessage', function (active) {
    var message = '';

    switch (active) {

      case 'incoming':
        message = 'No incoming requests';
        break;

      case 'outgoing':
        message = 'No outgoing requests';
        break;

      default:
        message = 'No incoming or outgoing requests';
        break;
    }

    return message;
  });
});