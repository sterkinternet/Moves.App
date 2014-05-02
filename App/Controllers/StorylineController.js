'use strict';

movesApp.controller('StorylineController', ['$scope', 'ApiService', function ($scope, apiService)
{
	var currentDate = new Date();
	
	$scope.year = currentDate.getFullYear()
	$scope.month = currentDate.getMonth() + 1;

	var search = function () {	
		apiService.getStoryline($scope.year, $scope.month).then(function (response) {
			$scope.days = response.data;
		});
	}
	search();

	$scope.$watch('year', search);
	$scope.$watch('month', search);
	
}]);