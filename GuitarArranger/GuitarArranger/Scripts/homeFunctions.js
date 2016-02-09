var HomeApp = angular.module('HomeApp', []);
HomeApp.controller('SearchController', function ($scope, $rootScope) {
    $scope.difficulty = ['Easy', 'Hard', 'Expert'];
    $scope.category = ['Artist', 'Title']
    $scope.selectedItem = 'Artist';
    $scope.dropboxitemselected = function (item) {
        $scope.selectedItem = item;
    }
});


HomeApp.factory('GetUserFiles', ['$http', function ($http) {
    var GetUserFilesService = {};
    GetUserFilesService.getUserFiles = function () {
        return $http.get('/Home/GetUserFiles');
    };
    return GetUserFilesService;
}]);