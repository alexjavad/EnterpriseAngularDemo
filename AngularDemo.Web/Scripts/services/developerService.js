var app = angular.module('appDemo');
app.factory('Developer', ['$http','$q', function($http, $q) {
    function Developer() {
        var self = this;

        self.GetDevelopers = function () {
            var deferred = $q.defer();
            $http.get('api/developers/GetDevelopers')
                .success(function(rows) {
                    deferred.resolve(rows);
                }).error(function(response) {
                    deferred.reject(response);
                });
            return deferred.promise;
        }
    }
    return new Developer();

}]);
