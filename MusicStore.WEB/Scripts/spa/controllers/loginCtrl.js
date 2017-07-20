(function (app) {
    'use strict';

    app.controller('loginCtrl', loginCtrl);

    loginCtrl.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location'];

    function loginCtrl($scope, membershipService, notificationService, $rootScope, $location) {
        $scope.pageTitle = 'Sign-in Page';
        $scope.login = login;
        $scope.user = {};

        function login() {
            membershipService.login($scope.user, loginCompleted);
        }

        function loginCompleted(result) {
            if (result.data !== null) {
                membershipService.saveCredentials(result.data.access_token, $scope.user.UserName);
                notificationService.displaySuccess('Hello ' + $scope.user.UserName);
                $scope.userData.displayUserInfo();
                if ($rootScope.previousState)
                    $location.path($rootScope.previousState);
                else
                    $location.path('/');
            }
            else {
                notificationService.displayError('Login failed. Try again.');
            }
        }
    }

})(angular.module('common.core'));