(function (app) {
    'use strict';

    app.directive('sideBar', sideBar);

    function sideBar() {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/scripts/spa/templates/sideBar.html'
        };
    }

})(angular.module('common.ui'));

