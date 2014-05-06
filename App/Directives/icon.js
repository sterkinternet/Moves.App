'use strict';

movesApp.directive('icon', function () {
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

