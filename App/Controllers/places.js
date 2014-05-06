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

	$scope.segment = null;

	$scope.details = function (day, segment) {	    
	    $scope.day = day;
	    $scope.segment = segment;

        if (segment) {
	        $scope.map = {
	            center: {
	                latitude: segment.place.location.lat,
	                longitude: segment.place.location.lon
	            },
	            zoom: 14
	        };
        }
	}

	$scope.$watch('year', search);
	$scope.$watch('month', search);
	

	$scope.map = null;

}]);