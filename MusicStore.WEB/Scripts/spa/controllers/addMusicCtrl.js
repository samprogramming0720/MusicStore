(function (app) {
    'use strict';
    app.controller('addMusicCtrl', addMusicCtrl);
    addMusicCtrl.$inject = ['$scope', '$location', 'apiService', 'notificationService', 'fileUploadService', 'autocompService'];
    function addMusicCtrl($scope, $location, apiService, notificationService, fileUploadService, autocompService) {

        $scope.checkSignin = _checkSignin;

        $scope.checkSignin();

        function _checkSignin() {
            if (!$scope.userData.isUserLoggedIn) {
                notificationService.displayError('Please sign in');
                $location.path('/login');
            }
        }

        $scope.addItem = _addItem;
        $scope.loadArtist = _loadArtist;
        $scope.querySearchArtist = _querySearchArtist;
        $scope.querySearchAlbum = _querySearchAlbum;

        //auto complete
        function _querySearchArtist(query) {
            return autocompService.search(query, $scope.artistList);
        }

        function _querySearchAlbum(query) {
            return autocompService.searchTitle(query, $scope.selectedArtist.AlbumList);
        }

        function _loadArtist() {
            apiService.get('api/artist/list', null, _artistLoadSuccess, _artistLoadFailed);
        }

        function _artistLoadSuccess(result) {
            if (result.status === 204) {
                notificationService.displayError(result.statusText);
            }
            $scope.artistList = result.data;
        }
        function _artistLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function _addItem() {
            if ($scope.selectedArtist !== null && $scope.selectedAlbum !== null) {
                $scope.item.ArtistID = $scope.selectedArtist.ID;
                $scope.item.AlbumID = $scope.selectedAlbum.ID;
                apiService.post('api/music/add',
                    $scope.item,
                    _addItemSucceeded,
                    _addItemFailed);
            }
            else {
                notificationService.displayError("Please select the Artist and Album.");
            }
        }

        function _addItemSucceeded(response) {
            notificationService.displaySuccess($scope.item.AlbumTitle + ' has been added');
            $scope.item = response.data;
            notificationService.displaySuccess("No image selected. You will be redirected in shortly.");
            _redirectToList();
        }

        function _addItemFailed(response) {
            notificationService.displayError(response.statusText);
            _redirectToList();
        }

        function _redirectToList() {
            $location.path('/Music');
        }

        $scope.loadArtist();
    }
})(angular.module('musicStore'));