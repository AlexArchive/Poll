app.controller('voteController', function ($scope, $http, $routeParams, $location) {

    $http.get("api/poll/" + $routeParams.pollId)
        .then(onPollAvailable);
    
    function onPollAvailable(response) {
        $scope.poll = response.data;

        $scope.isChecked = function () {
            for (var index in $scope.poll.options) {
                var option = $scope.poll.options[index];
                if (option.checked === true) {
                    return true;
                }
            }
            return false;
        };
    }

    $scope.vote = function () {

        var data = {
            PollId: $routeParams.pollId,
            OptionIds: []
        };

        $scope.poll.options.forEach(function(option) {
            if (option.checked === true) {
                data.OptionIds.push(option.id);
            }
        });

        $http.put('api/poll/' + $routeParams.pollId, data.OptionIds)
            .success(onVoted);
    };

    function onVoted() {
        $location.path("/" + $routeParams.pollId + "/results");
    }

    $scope.updateSelection = function (position) {

        if ($scope.poll.multiChoice === true) return;

        angular.forEach($scope.poll.options, function (option, index) {
            if (position != index) {
                option.checked = false;
            }
        });
    };
});