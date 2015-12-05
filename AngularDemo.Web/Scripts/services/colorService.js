var app = angular.module('appDemo');
app.factory('Color', ['$http', '$q', function ($http, $q) {
    function Color() {
        var self = this;

        self.GetColors = function () {
            var deferred = $q.defer();
            $http.get('api/colors/GetColors')
                .success(function (rows) {
                    deferred.resolve(rows);
                }).error(function (response) {
                    deferred.reject(response);
                });
            return deferred.promise;
        }
    }
    return new Color();

}]);
