(function (app) {
    'use strict';

    app.factory('autocompService', autocompService);

    autocompService.$inject = ['$q'];

    function autocompService($q) {

        var service = {
            search: _search,
            searchTitle: _searchTitle
        };

        function _search(query, list) {
            var deferred = $q.defer();
            var result = list.filter(function (item) {
                var queryToLower = angular.lowercase(query);
                var itemToLower = angular.lowercase(item.Name);
                return itemToLower.indexOf(queryToLower) === 0;
            });
            deferred.resolve(result);
            return deferred.promise;
        }

        function _searchTitle(query, list) {
            var deferred = $q.defer();
            var result = list.filter(function (item) {
                var queryToLower = angular.lowercase(query);
                var itemToLower = angular.lowercase(item.AlbumTitle);
                return itemToLower.indexOf(queryToLower) === 0;
            });
            deferred.resolve(result);
            return deferred.promise;
        }

        return service;
    }

})(angular.module('common.core'));