app.controller("resultsController", function ($scope, $http, $routeParams) {
    $http.get("api/poll/" + $routeParams.pollId).then(function(response) {
        $scope.poll = response.data;
    });
});