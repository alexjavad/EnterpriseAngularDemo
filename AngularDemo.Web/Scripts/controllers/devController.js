
var app = angular.module('appDemo');
app.controller('devController', ['$scope', '$http', 'Developer', function ($scope, $http, Developer) {
    Developer.GetDevelopers().then(function (data) {
        $scope.rowCollection = data;
    });
}
]);
