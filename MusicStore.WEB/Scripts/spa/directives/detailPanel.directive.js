(function (app) {
    'use strict';

    app.directive('detailPanel', detailPanel);

    function detailPanel() {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/scripts/spa/templates/detailPanel.html'
        };
    }
})(angular.module('common.ui'));