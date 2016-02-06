define(['hbs/handlebars'], function (Handlebars) {
  'use strict';

  Handlebars.registerHelper('formatDate', function (date) {
    var daySplit = (date.split('-')[2]).toString();
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

    var day = daySplit.slice(0, 2);
    var month = date.split('-')[1];
    var year = date.split('-')[0];

    var timeSplit = date.split('T')[1].toString();
    var hour = timeSplit.split(':')[0].toString();
    var minute = timeSplit.split(':')[1].toString();

    return (day + '-' + months[parseInt(month) - 1] + '-' + year + ' ' + hour + ':' + minute);
  });
});