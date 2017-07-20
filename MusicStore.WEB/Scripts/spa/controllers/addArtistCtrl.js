(function (app) {
    'use strict';
    app.controller('addArtistCtrl', addArtistCtrl);
    addArtistCtrl.$inject = ['$scope', '$location', 'apiService', 'notificationService', 'fileUploadService'];
    function addArtistCtrl($scope, $location, apiService, notificationService, fileUploadService) {

        $scope.checkSignin = _checkSignin;

        $scope.checkSignin();

        function _checkSignin() {
            if (!$scope.userData.isUserLoggedIn) {
                notificationService.displayError('Please sign in');
                $location.path('/login');
            }
        }

        $scope.addItem = _addItem;

        function _addItem() {
            apiService.post('api/artist/add', $scope.item, _addItemSucceeded, _addItemFailed);
        }
        
        function _addItemSucceeded(response) {
            notificationService.displaySuccess($scope.item.Name + ' has been added');
            $scope.item = response.data;
            if ($scope.imgFile !== undefined) {
                fileUploadService.uploadArtist(response.data.ID, $scope.imgFile);
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
            $location.path('/artist');
        }
    }
})(angular.module('musicStore'));