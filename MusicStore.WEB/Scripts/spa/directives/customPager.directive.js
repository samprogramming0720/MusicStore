(function (app) {
    'use strict';
    app.directive('customPager', customPager);
    function customPager() {
        return {
            scope: {
                page: '@',
                pagesCount: '@',
                totalCount: '@',
                searchFunc: '&',
                customPath: '@'
            },
            replace: true,
            restrict: 'E',
            templateUrl: '/scripts/spa/templates/pager.html',
            controller: ['$scope', function ($scope) {
                $scope.search = function (i) {
                    if ($scope.searchFunc) {
                        $scope.searchFunc({ page: i });
                    }
                };
                $scope.range = function () {
                    if (!$scope.pagesCount) {
                        return [];
                    }
                    var step = 2;
                    var doubleStep = step * 2;
                    var start = Math.max(0, $scope.page - step);
                    var end = start + 1 + doubleStep;
                    if (end > $scope.pagesCount) {
                        end = $scope.pagesCount;
                    }
                    var markers = [];
                    for (var i = start; i != end; ++i) {
                        markers.push(i);
                    }
                    return markers;
                };
                $scope.pagePlus = function (count) {
                    return + $scope.page + count;
                };
            }]
        };
    }
})(angular.module('musicStore'));