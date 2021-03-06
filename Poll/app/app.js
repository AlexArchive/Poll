﻿var app = angular.module('Poll', ['ngRoute']);

app.config(function($routeProvider) {

    $routeProvider.when('/', {
        controller: 'submitController',
        templateUrl: 'app/views/submit.html'
    });

    $routeProvider.when('/:pollId', {
        controller: 'voteController',
        templateUrl: 'app/views/vote.html'
    });

    $routeProvider.when('/:pollId/results', {
        controller: 'resultsController',
        templateUrl: 'app/views/results.html'
    });

});