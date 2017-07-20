(function (app) {
    'use strict';

    app.controller('rootCtrl', rootCtrl);

    rootCtrl.$inject= ['$scope', '$location', 'membershipService', '$rootScope'];

    function rootCtrl($scope, $location, membershipService, $rootScope) {

        $scope.userData = {};

        $scope.userData.displayUserInfo = _displayUserInfo;
        $scope.logout = _logout;

        function _displayUserInfo() {
            $scope.userData.isUserLoggedIn = membershipService.isUserLoggedIn();
            if ($scope.userData.isUserLoggedIn) {
                $scope.UserName = $scope.repository.loggedUser.UserName;
            }
        }

        function _logout() {
            membershipService.removeCredentials();
            $location.path('/');
            $scope.userData.displayUserInfo();
        }
        $scope.userData.displayUserInfo();
    }

})(angular.module('musicStore'));