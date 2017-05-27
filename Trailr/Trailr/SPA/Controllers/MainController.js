var MainController = function ($scope,$http) {
    var html = "";
    function loadIndex() {
        $http.get("Dashboard/getIndex").then(function (response) {
            $scope.index = response.data;
        });
    }
    loadIndex();
}

MainController.$inject = ['$scope', '$http'];