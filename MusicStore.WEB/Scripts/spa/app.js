(function () {
    'use strict';

    var app = angular.module('musicStore',
        [
            'common.core', 'common.ui'
        ]);

    app.config(config);

    app.config(barConfig);

    barConfig.$inject = ['cfpLoadingBarProvider'];

    function barConfig(cfpLoadingBarProvider) {
        cfpLoadingBarProvider.includeSpinner = true;
    }

    config.$inject = ['$routeProvider', '$locationProvider'];

    function config($routeProvider, $locationProvider) {
        $locationProvider.hashPrefix('');
        $routeProvider
            .when("/music", {
                templateUrl: "scripts/spa/views/getMusic.html",
                controller: "musicCtrl"
            })
            .when("/music/add", {
                templateUrl: "scripts/spa/views/addMusic.html",
                controller: 'addMusicCtrl'
            })
            .when("/artist", {
                templateUrl: "scripts/spa/views/getArtist.html",
                controller: "artistCtrl"
            })
            .when("/artist/:artistId/music", {
                templateUrl: "scripts/spa/views/getMusic.html",
                controller: "artistMusicCtrl"
            })
            .when("/artist/:artistId/album", {
                templateUrl: "scripts/spa/views/getAlbum.html",
                controller:"artistAlbumCtrl"
            })
            .when("/artist/add", {
                templateUrl: "scripts/spa/views/addArtist.html",
                controller: "addArtistCtrl"
            })
            .when("/album", {
                templateUrl: "scripts/spa/views/getAlbum.html",
                controller: "albumCtrl"
            })
            .when("/album/:albumId/music", {
                templateUrl: "scripts/spa/views/getMusic.html",
                controller: "albumMusicCtrl"
            })
            .when("/album/add", {
                templateUrl: "scripts/spa/views/addAlbum.html",
                controller: "addAlbumCtrl"
            })
            .when("/login", {
                templateUrl: "scripts/spa/views/login.html",
                controller: "loginCtrl"
            })
            .when("/register", {
                templateUrl: "scripts/spa/views/register.html",
                controller: "registerCtrl"
            })
            .otherwise({
                redirectTo: "/music"
            });
    }

    app.run(run);
    
    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];

    function run($rootScope, $location, $cookieStore, $http) {

        $rootScope.repository = $cookieStore.get('repository') || {};

        if ($rootScope.repository.loggedUser) {
            $http.defaults.headers.common['Authorization'] =
                'Bearer ' + $rootScope.repository.loggedUser.token;
        }

        $(document).ready(function () {
            $(".fancybox").fancybox({
                openEffect: 'none',
                closeEffect: 'none'
            });

            $('.fancybox-media').fancybox({
                openEffect: 'none',
                closeEffect: 'none',
                helpers: {
                    media: {}
                }
            });
            $('[data-toggle=offcanvas').click(function () {
                $('.row-offcanvas').toggleClass('active');
            });
        });
    }
})();

