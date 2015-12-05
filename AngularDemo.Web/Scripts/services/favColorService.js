var app = angular.module('appDemo');
app.factory('FavoriteColorPair', ['$http', '$q', function ($http, $q) {
    function FavoriteColorPair() {
        var self = this;

        self.GetFavorites = function () {
            var deferred = $q.defer();
            $http.get('api/favoritecolors/GetFavorites')
                .success(function (rows) {
                    deferred.resolve(rows);
                }).error(function (response) {
                    deferred.reject(response);
                });
            return deferred.promise;
        };

        self.AddFavorite = function (devId, colorId) {

            var favorite = {
                DeveloperId: devId,
                ColorId: colorId
            };

            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: 'api/favoritecolors/PostFavorite',
                data: JSON.stringify(favorite),
                headers: { 'Content-Type': 'application/JSON' }
            }).success(function (data) {
                    alert("Successfully added");
                    deferred.resolve(data);
                })
            .error(function (error) {
                alert("error" + error.Message + ' ' + error.MessageDetail);
                deferred.reject(error);
            });
            return deferred.promise;
        }


        self.UpdateFavorite = function(id, colorId) {
            var favorite = {
                Id: id,
                ColorId: colorId
            };
            var deferred = $q.defer();
            $http({
                method: 'PUT',
                url: 'api/favoritecolors/PutFavorite',
                data: JSON.stringify(favorite),
                headers: { 'Content-Type': 'application/JSON' }
            }).success(function (data) {
                alert("Successfully updated");
                deferred.resolve(data);
            })
            .error(function (error) {
                alert("error" + error.Message + ' ' + error.MessageDetail);
                deferred.reject(error);
            });
            return deferred.promise;
        }


    }
    return new FavoriteColorPair();

}]);
