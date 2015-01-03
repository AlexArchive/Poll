app.directive('appendInput', function() {

    return {
        restrict: 'A',
        link: function (scope, element, attributes) {
            element.html('<p>Hi</p>');
            console.log("invoked");
        }
    };
});