'use strict';

movesApp.controller('PlacesController', ['$scope', 'PlacesService', function ($scope, placesService)
{
	var currentDate = new Date();
	
	$scope.year = currentDate.getFullYear()
	$scope.month = currentDate.getMonth() + 1;

	var search = function () {
		placesService.getMonth($scope.year, $scope.month).then(function (response) {
			$scope.days = response.data;
		});
	}
	search();

	$scope.$watch('year', search);
	$scope.$watch('month', search);
	
}]);