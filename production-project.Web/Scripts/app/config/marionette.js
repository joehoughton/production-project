define([
  'backbone.radio',
  'underscore',
  'Marionette'
], function (Radio, _, Marionette) {
  'use strict';

  Marionette.Application.prototype._initChannel = function () {
    this.channelName = _.result(this, 'channelName') || 'global';
    this.channel = _.result(this, 'channel') || Radio.channel(this.channelName);
  };
});