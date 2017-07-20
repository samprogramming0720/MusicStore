(function (app) {
    'use strict';

    app.factory('membershipService', membershipService);

    membershipService.$inject = ['apiService', 'notificationService', '$http', '$cookieStore', '$rootScope'];

    function membershipService(apiService, notificationService, $http, $cookieStore, $rootScope) {
        var service = {
            login: _login,
            register: _register,
            saveCredentials: _saveCredentials,
            removeCredentials: _removeCredentials,
            isUserLoggedIn: _isUserLoggedIn
        };
        
        function _login(user, completed) {
            var data = "grant_type=password&username=" + user.UserName + "&password=" + user.Password;
            var header = { 'Content-Type': 'application/x-www-form-urlencoded' };
            apiService.loginpost('/token', data, header, completed, _loginFailed);
        }
        
        function _register(user, completed) {
            apiService.post('/api/account/register', user, completed, _registrationFailed);
        }

        function _saveCredentials(token, UserName) {
            $rootScope.repository = {
                loggedUser: {
                    UserName: UserName,
                    token: token
                }
            };
            $http.defaults.headers.common['Authorization'] = 'Bearer ' + token;
            $cookieStore.put('repository', $rootScope.repository);
        }

        function _removeCredentials() {
            $rootScope.repository = {};
            $cookieStore.remove('repository');
            $http.defaults.headers.common.Authorization = '';
        }

        function _loginFailed(response) {
            notificationService.displayError(response.data.error_description);
        }

        function _registrationFailed(response) {
            notificationService.displayError(
                response.data.ModelState['']);
        }

        function _isUserLoggedIn() {
            return $rootScope.repository.loggedUser !== undefined;
        }

        return service;
    }
})(angular.module('common.core'));