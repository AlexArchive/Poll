app.controller('pollController', function ($scope, $http, $routeParams) {

    $http.get("Poll/" + $routeParams.pollId).then(onPollAvailable);

    function onPollAvailable(response) {
        $scope.poll = response.data;
    }

});