
var app = angular.module('appDemo');
app.controller('favColorController', ['$scope', '$http', 'Developer', 'Color', 'FavoriteColorPair', function ($scope, $http, Developer, Color, FavoriteColorPair) {

    $scope.storeFavColor = function () {
        if ($scope.selectedDev == undefined) alert('Please select a Developer');
        if ($scope.selectedColor == undefined) alert('Please select a color');
        if ($scope.selectedDev != undefined && $scope.selectedColor != undefined) {
            var foundMatchingDev = false;
            $.each($scope.favColorPairs, function (index, elem) {
                if (elem.DeveloperId == $scope.selectedDev.Id) {
                    foundMatchingDev = true;
                }
            });
            if (!foundMatchingDev) {
                FavoriteColorPair.AddFavorite($scope.selectedDev.Id, $scope.selectedColor.Id).then(function (data) {
                    $scope.favColorPairs.push(data);
                });
            } else {
                alert('A record already exists for this developer.');
            }
        }
    };

    $scope.editFavorite = function (favorite) {
        $scope.editFavModel = favorite; //give a reference to the model of the entire Dto of the favorite object being edited
        $scope.edit = true;
    }

    $scope.UpdateFavoriteColor = function (editFavModel) {
        if ($scope.editColor == undefined) alert("Please select a color");
        else {
            updateObjectColorWithId(editFavModel, $scope.editColor);
        }
    };

    function clearForm() {
        $scope.editColor = null;
        $scope.editFavModel = null;
        $scope.edit = false;
    }

    $scope.cancelForm = clearForm;

    function updateObjectColorWithId(editFavModel, colorObj) {

        $.each($scope.favColorPairs, (function (index, elem) {
            if (elem.Id == editFavModel.Id) {
                elem.Color = colorObj.Color;
            }
        }));
        FavoriteColorPair.UpdateFavorite(editFavModel.Id, colorObj.Id).then(function (data) {
            clearForm();
        });
    }

    FavoriteColorPair.GetFavorites().then(function (data) {
        $scope.favColorPairs = data;
        Developer.GetDevelopers().then(function (data) {
            $scope.devs = data;
            Color.GetColors().then(function (data) {
                $scope.colors = data;
            });
        });
    });

    ////Chaining Promises
    //FavoriteColorPair.GetFavorites().then(function (data) {
    //    $scope.favColorPairs = data;
    //}).then(function () {
    //    Developer.GetDevelopers().then(function (data) {
    //        $scope.devs = data;
    //    }).then(function () {
    //        Color.GetColors().then(function (data) {
    //            $scope.colors = data;
    //        });
    //    });
    //});

    //FavoriteColorPair.GetFavorites().then(function (data) {
    //    $scope.favColorPairs = data;

    //    var deferred = $q.defer();

    //    deferred.resolve('do #2');

    //    return deferred.promise;
    //}).then(function (two) {
    //            Developer.GetDevelopers().then(function (data2) {
    //                $scope.devs = data2;

    //                var deferred2 = $q.defer();

    //                deferred2.resolve('do #3');

    //                return deferred2.promise;

    //            }).then(function (three) {
    //                Color.GetColors().then(function (data3) {
    //                    $scope.colors = data3;
    //                });
    //            });
    //});
}
]);
