app.controller('submitController', function ($scope, $location, $http) {

    $scope.poll = {
        options: []
    };

    $scope.submitPoll = function () {
        $http.post('Poll', $scope.poll)
            .success(onPollSubmited);
    };

    function onPollSubmited(data, status, headers) {
        var submitedPollId = headers('Location');
        $location.path("/" + submitedPollId);
    }

});