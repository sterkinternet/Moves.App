'use strict';

angular.module('ApiModule', [])
	.factory('ApiService', ['$http', 'UtilService', function ($http, utilService) {
	    return {	        
	    	'getPlaces': function (year, month) {	    		
	    	    return $http.get(utilService.createRequestUrl('/Api/Place/GetByMonth?year={0}&month={1}', year, month));
	    	},
	    	'getStoryline': function (year, month) {	    		
	    		return $http.get(utilService.createRequestUrl('/Api/Storyline/GetByMonth?year={0}&month={1}', year, month));
	    	},
	    	'getSummary': function (year, month) {	    		
	    	    return $http.get(utilService.createRequestUrl('/Api/Summary/GetByMonth?year={0}&month={1}', year, month));
	    	}
	    };
    }]);