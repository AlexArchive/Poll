app.controller('pollController', function ($scope, $http) {

    $scope.poll = {
        options: []
    };

    $scope.submitPoll = function () {
        $http.post('Poll', $scope.poll).success(function (data, status, headers) {
            var pollId = headers('Location');
            window.location = 'Poll/' + pollId;
        });
    };
});