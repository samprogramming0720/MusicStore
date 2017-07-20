(function (app) {
    'use strict';

    app.controller('registerCtrl', registerCtrl);

    registerCtrl.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location', '$timeout', 'cfpLoadingBar'];

    function registerCtrl($scope, membershipService, notificationService, $rootScope, $location, $timeout, cfpLoadingBar) {
        $scope.pageTitle = 'Register Page';
        $scope.register = register;
        $scope.user = {};

        function register() {
            membershipService.register($scope.user, registerCompleted);
        }

        function registerCompleted(result) {
            if (result.data.success) {
                cfpLoadingBar.start();
                notificationService.displaySuccess('Registration Completed! You will be redirected to login page in just a second');
                $timeout(function () {
                    cfpLoadingBar.complete();
                    $location.path('/login');
                }, 1500);
            }
            else {
                notificationService.displayError(result.data);
            }
        }
    }

})(angular.module('common.core'));