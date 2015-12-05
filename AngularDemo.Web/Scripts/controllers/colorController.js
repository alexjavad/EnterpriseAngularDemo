
var app = angular.module('appDemo');
app.controller('colorController', ['$scope', '$http', 'Color', function ($scope, $http, Color) {
    Color.GetColors().then(function (data) {
        $scope.rowCollection = data;
    });
}
]);