(function (app) {
    'use strict';
    app.controller('albumCtrl', albumCtrl);
    albumCtrl.$inject = ['$scope', 'apiService', 'notificationService'];
    function albumCtrl($scope, apiService, notificationService) {

        $scope.pageTitle = 'Album List';
        $scope.loading = true;
        $scope.page = 0;
        $scope.pagesCount = 0;

        $scope.Items = [];

        $scope.search = _search;
        $scope.clearSearch = _clearSearch;

        $scope.AddItem = 'Add New Album';

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
            apiService.get('/api/album/search/', config,
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

            if ($scope.filterAlbum && $scope.filterAlbum.length) {
                notificationService.displayInfo(result.data.Items.length + ' found');
            }
        }

        function _itemsLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function _clearSearch() {
            $scope.filterAlbum = '';
            search();
        }

        $scope.search();
    }

})(angular.module('musicStore'));

