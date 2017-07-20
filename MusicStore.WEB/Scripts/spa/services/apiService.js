(function (app) {
    'use strict';

    app.factory('apiService', apiService);

    apiService.$inject = ['$http', '$location', 'notificationService', '$rootScope'];

    function apiService($http, $location, notificationService, $rootScope) {
        var service = {
            get: _get,
            post: _post,
            loginpost: _loginpost
        };

        function _get(url, config, success, failure) {
            return $http.get(url, config)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status === '401') {
                        notificationService.displayError('Authentication required.');
                        $rootScope.previousState = $location.path();
                        $location.path('/login');
                    }
                    else if (failure !== null) {
                        failure(error);
                    }
                });
        }

        function _post(url, data, success, failure) {
            return $http.post(url, data)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status === '401') {
                        notificationService.displayError('Authentication required.');
                        $rootScope.previousState = $location.path();
                        $location.path('/login');
                    }
                    else if (failure !== null) {
                        failure(error);
                    }
                });
        }

        function _loginpost(url, data, header, success, failure) {
            return $http.post(url, data, { headers: header })
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status === '401') {
                        notificationService.displayError('Authentication required.');
                        $rootScope.previousState = $location.path();
                        $location.path('/login');
                    }
                    else if (failure !== null) {
                        failure(error);
                    }
                });
        }

        return service;
    }

})(angular.module('common.core'));