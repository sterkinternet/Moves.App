'use strict';

movesApp.controller('PlacesController', ['$scope', 'ApiService', function ($scope, apiService)
{
	var currentDate = new Date();
	
	$scope.year = currentDate.getFullYear()
	$scope.month = currentDate.getMonth() + 1;

	var search = function () {
		apiService.getPlaces($scope.year, $scope.month).then(function (response) {
			$scope.days = response.data;
		});
	}
	search();

	$scope.$watch('year', search);
	$scope.$watch('month', search);
	
}]);