'use strict';

movesApp.controller('SummaryController', ['$scope', 'ApiService', function ($scope, apiService)
{
    var currentDate = new Date();

    $scope.year = currentDate.getFullYear()
    $scope.month = currentDate.getMonth() + 1;

    $scope.search = function () {        
        apiService.getSummary($scope.year, $scope.month).then(function (response) {
            $scope.days = response.data;
        });
    }
    $scope.search();  

    $scope.$watch('year', $scope.search);
    $scope.$watch('month', $scope.search);    
	
}]);