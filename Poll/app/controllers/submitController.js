app.controller('submitController', function ($scope, $location, $http) {

    $scope.poll = {
        options: []
    };
    $scope.poll.options.length = 3;

    $scope.submitPoll = function () {
        $scope.poll.options = $scope.poll.options.filter(function () { return true; });
        $http.post('api/poll', $scope.poll)
            .success(onPollSubmited);
    };

    $scope.addOption = function () {
        $scope.poll.options.length += 1;
    };
    
    function onPollSubmited(data, status, headers) {
        $location.path("/" + data.pollId);
    }

});