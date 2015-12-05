var app = angular.module('appDemo', ['ngRoute']);
app.config([
    '$routeProvider', function ($routeProvider) {
        $routeProvider.when('/developers', {
            templateUrl: 'pages/developers.html',
            controller: 'devController'
        }).when('/colors', {
            templateUrl: 'pages/colors.html',
            controller: 'colorController'
        }).when('/favoritecolors', {
            templateUrl: 'pages/favoritecolors.html',
            controller: 'favColorController'
        }).otherwise({
            redirectTo: '/'
        });
    }
]);


