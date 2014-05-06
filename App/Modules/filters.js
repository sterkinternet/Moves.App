angular.module('movesFilters', [])
    .filter('steps', function () {
        return function (input) {            
            var steps_num = parseInt(input, 10); // don't forget the second param          
            return steps_num > 0 ? steps_num + ' steps' : '';
        };
    })
    .filter('calories', function () {
        return function (input) {
            var calories_num = parseInt(input, 10); // don't forget the second param          
            return calories_num > 0 ? calories_num + ' calories' : '';
        };
    })
    .filter('distance', function () {
        return function (input) {
            var meters_num = parseInt(input, 10); // don't forget the second param
            var km = Math.floor(meters_num / 1000);
            var meters = meters_num - (km * 1000);

            return (km > 0) ? km + '.' + meters + ' km' : meters + ' meter';
        };
    })
    .filter('duration', function () {
        return function (input) {
            var sec_num = parseInt(input, 10); // don't forget the second param
            var hours = Math.floor(sec_num / 3600);
            var minutes = Math.floor((sec_num - (hours * 3600)) / 60);
            var seconds = sec_num - (hours * 3600) - (minutes * 60);

            if (hours < 10) { hours = "0" + hours; }
            if (minutes < 10) { minutes = "0" + minutes; }
            if (seconds < 10) { seconds = "0" + seconds; }
            return hours + ':' + minutes + ':' + seconds;
        };
    });