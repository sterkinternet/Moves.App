'use strict';

movesApp.directive('activityinfo', function () {
    return {
        scope: {
            ngModel: '='
        },
        require: "ngModel",
        restrict: 'AE',
        template: '<img src="/Content/Icons/{{ngModel}}.png" alt="" />',
        replace: true
    };
});

