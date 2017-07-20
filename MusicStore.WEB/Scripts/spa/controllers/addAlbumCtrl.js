(function (app) {
    'use strict';
    app.controller('addAlbumCtrl', addAlbumCtrl);
    addAlbumCtrl.$inject = ['$scope', '$location', 'apiService', 'notificationService', 'fileUploadService', 'autocompService'];
    function addAlbumCtrl($scope, $location, apiService, notificationService, fileUploadService, autocompService) {

        $scope.checkSignin = _checkSignin;
        $scope.addItem = _addItem;
        $scope.loadParent = _loadParent;
        $scope.querySearch = _querySearch;

        $scope.checkSignin();

        function _checkSignin() {
            if (!$scope.userData.isUserLoggedIn) {
                notificationService.displayError('Please sign in');
                $location.path('/login');
            }
        }

        function _querySearch(query) {
            return autocompService.search(query, $scope.parentList);
        }
        
        function _loadParent() {
            apiService.get('api/artist/list', null, _parentLoadSuccess, _parentLoadFailed);
        }
        function _parentLoadSuccess(result) {
            if (result.status === 204) {
                notificationService.displayError(result.statusText);
            }
            $scope.parentList = result.data;
        }
        function _parentLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function _addItem() {
            if ($scope.selectedItem !== null) {
                $scope.item.ArtistID = $scope.selectedItem.ID;
            }
            apiService.post('api/album/add', $scope.item, _addItemSucceeded, _addItemFailed);
        }

        function _addItemSucceeded(response) {
            notificationService.displaySuccess($scope.item.AlbumTitle + ' has been added');
            $scope.item = response.data;
            if ($scope.imgFile !== undefined) {
                fileUploadService.uploadAlbum(response.data.ID, $scope.imgFile);
            }
            else {
                notificationService.displaySuccess("No image selected. You will be redirected in shortly.");
                _redirectToList();
            }
        }

        function _addItemFailed(response) {
            notificationService.displayError(response.statusText);
            _redirectToList();
        }

        function _redirectToList() {
            $location.path('/album');
        }

        $scope.loadParent();
    }
})(angular.module('musicStore'));