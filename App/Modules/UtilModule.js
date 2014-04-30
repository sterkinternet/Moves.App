'use strict';

angular.module('UtilModule', [])

	.service('UtilService', [function ()
	{
		var that = this;

		that.stringformat = function (format) {
		    var args = arguments[1];
		    for (var i = 0; i < args.length; i++) {
		        var reg = new RegExp("\\{" + i + "\\}", "gm");
		        format = format.replace(reg, args[i]);
		    }

		    return format;
		};

		return {
			createRequestUrl: function (format, args)
			{
				var encodedArgs = _.map(_.without(arguments, arguments[0]), function (arg) {
					if (!arg)
						return arg;
					return (''+arg).replace(/#/g, '__HASH__');
				});

				return that.stringformat(format, encodedArgs);
			},
		};
	}]);