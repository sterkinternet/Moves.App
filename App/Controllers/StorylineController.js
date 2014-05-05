'use strict';

movesApp.controller('StorylineController', ['$scope', 'ApiService', function ($scope, apiService)
{
    var currentDate = new Date();

    $scope.year = currentDate.getFullYear()
    $scope.month = currentDate.getMonth() + 1;

    $scope.search = function () {        
        apiService.getStoryline($scope.year, $scope.month).then(function (response) {
            $scope.days = response.data;
        });
    }
    $scope.search();

    $scope.map = null;
    $scope.segment = null;

    $scope.details = function (day, segment) {        
        $scope.day = day;
        $scope.segment = segment;

        if (segment && segment.place && segment.place.location) {
            $scope.map = {
                center: {
                    latitude: segment.place.location.lat,
                    longitude: segment.place.location.lon
                },
                zoom: 14
            };
        }
        else {
            $scope.map = null;
        }
    }

    $scope.$watch('year', $scope.search);
    $scope.$watch('month', $scope.search);    
	
}]);