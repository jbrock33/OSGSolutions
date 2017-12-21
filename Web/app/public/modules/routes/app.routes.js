(function () {
    'use strict';
    var app = angular.module("publicApp" + '.routes', []);

    app.config(_configureStates);

    _configureStates.$inject = ['$stateProvider', '$locationProvider', '$urlRouterProvider'];

    function _configureStates($stateProvider, $locationProvider, $urlRouterProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false,
        });
        $urlRouterProvider.otherwise('/home');
        $stateProvider
            .state({
                name: 'home',
                url: '/home',
                templateUrl: '/app/public/home.html',
                title: 'Home'
            })
            .state({
                name: 'scraping',
                url: '/scraping',
                templateUrl: '/app/public/modules/scraping/scraping.html',
                title: 'Scraping'
            })
            .state({
                name: 'register',
                url: '/register',
                templateUrl: '/app/public/modules/users/register.html',
                controller: "registerController as registerCtrl",
                title: 'Register'
            })
            .state({
                name: 'login',
                url: '/login',
                templateUrl: '/app/public/modules/users/login.html',
                controller: "loginController as loginCtrl",
                title: 'Login'
            })
            .state({
                name: 'fileupload',
                url: '/fileupload',
                templateUrl: '/app/public/modules/fileupload/fileupload.html',
                title: 'FileUpload'
            })
            .state({
                name: 'statuses',
                url: '/statuses',
                templateUrl: '/app/public/modules/statuses/statuses.html',
                title: 'Statuses'
            });
    }
})();