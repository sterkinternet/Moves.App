'use strict';

movesApp.directive('moment', function () {
    return {
        restrict: 'AE',
        require: "ngModel",
        scope: {
            ngModel: '=',
            parse: '&',
            format: '&'
        },
        replace: true,
        link: function ($scope, element, attrs) {                        
            element.text(moment($scope.ngModel, attrs.ngMomentParse).format(attrs.ngMomentFormat));
        }
    };
});