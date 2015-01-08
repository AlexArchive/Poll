app.controller("submitController", function ($scope, $location, $http) {

    $scope.poll = {
        options: []
    };
    $scope.poll.options.length = 3;

    $scope.addOption = function () {
        $scope.poll.options.length += 1;
    };
    
    $scope.submitPoll = function () {
        $scope.poll.options = $scope.poll.options.filter(function () { return true; });
        $http.post("api/poll", $scope.poll).success(function(data) {
            $location.path("/" + data.pollId);
        });
    };
});