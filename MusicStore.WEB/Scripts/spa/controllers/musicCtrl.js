(function (app) {
    'use strict';
    app.controller('musicCtrl', musicCtrl);
    musicCtrl.$inject = ['$scope', 'apiService', 'notificationService'];
    function musicCtrl($scope, apiService, notificationService) {

        $scope.pageTitle = 'Music List';
        $scope.loading = true;
        $scope.page = 0;
        $scope.pagesCount = 0;

        $scope.Items = [];

        $scope.search = _search;
        $scope.clearSearch = _clearSearch;

        $scope.AddItem = 'Add New Music';

        function _search(page) {
            page = page || 0;

            $scope.loading = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 4,
                    filter: $scope.filterItems
                }
            };
            apiService.get('/api/music/search/', config,
                _itemsLoadCompleted,
                _itemsLoadFailed,
            );
        }

        function _itemsLoadCompleted(result) {
            $scope.Items = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loading = false;

            if ($scope.filterMusic && $scope.filterMusic.length) {
                notificationService.displayInfo(result.data.Items.length + ' found');
            }
        }

        function _itemsLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function _clearSearch() {
            $scope.filterMusic = '';
            search();
        }

        $scope.search();
    }

})(angular.module('musicStore'));

