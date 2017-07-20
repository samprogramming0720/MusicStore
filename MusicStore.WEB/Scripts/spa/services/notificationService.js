(function (app) {
    'use strict';

    app.factory('notificationService', notificationService);

    function notificationService() {

        toastr.options = {
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "fadeIn": 300,
            "fadeOut": 1500,
            "timeOut": 1000,
            "extendedTimeOut": 1000
        };

        var notificationServiceFactory = {
            displaySuccess: _displaySuccess,
            displayError: _displayError,
            displayWarning: _displayWarning,
            displayInfo: _displayInfo
        };

        function _displaySuccess(message) {
            toastr.success(message);
        }

        function _displayError(error) {
            if (Array.isArray(error)) {
                error.forEach(function (err) {
                    toastr.error(err);
                });
            } else {
                toastr.error(error);
            }
        }

        function _displayWarning(message) {
            toastr.warning(message);
        }

        function _displayInfo(message) {
            toastr.info(message);
        }

        return notificationServiceFactory;
    }

})(angular.module('common.core'));