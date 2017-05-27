var App = angular.module('App', ['ngRoute']);

App.controller('MainController', MainController);
App.controller('ProjectController', ProjectController);

App.config(function ($routeProvider, $locationProvider) {
    $routeProvider
    .when("/", {
        templateUrl: "SPA/Views/index.html"
    })
    .when("/test", {
        templateUrl: "SPA/Views/test.html"
    })
    .when("/project/:id", {
        templateUrl: "SPA/Views/project.html",
        controller: ProjectController
    });

    $locationProvider.html5Mode(true);
});