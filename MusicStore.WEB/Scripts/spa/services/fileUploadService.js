(function (app) {
    'use strict';

    app.factory('fileUploadService', fileUploadService);

    fileUploadService.$inject = ['$http', 'Upload', 'notificationService', '$location'];

    function fileUploadService($http, Upload, notificationService, $location) {
        
        var service = {
            uploadAlbum: _uploadAlbum,
            uploadArtist: _uploadArtist
        };

        function _uploadAlbum(id, file) {
            var url = 'api/album/upload/' + id;
            var redirect = '/album';
            _upload(url, file, redirect, _success, _failed);
        }

        function _uploadArtist(id, file) {
            var url = 'api/artist/upload/' + id;
            var redirect = '/artist';
            _upload(url, file, redirect, _success, _failed);
        }

        function _upload(url, file, redirect, success, failed) {
            var upload = Upload.upload({
                url: url,
                data: { key: file },
                method: 'POST'
                }).then(function (resp) {
                    return success(resp, redirect);
                }).catch(function (error) {
                    return failed(error, redirect);
                });
        }

        function _success(resp, redirect) {
            notificationService.displaySuccess('Image uploading succeeded.');
            $location.path(redirect);
        }

        function _failed(resp, redirect) {
            notificationService.displayError('Image uploading failed.');
            $location.path(redirect);
        }

        return service;
    }

})(angular.module('common.core'));