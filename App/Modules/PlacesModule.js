'use strict';

angular.module('PlacesModule', [])
	.factory('PlacesService', ['$http', 'UtilService', function ($http, utilService) {
	    return {	        
	    	'getMonth': function (year, month) {	    		
	    	    return $http.get(utilService.createRequestUrl('Api/Place/GetByMonth?year={0}&month={1}', year, month));
	        }
	    };
    }]);