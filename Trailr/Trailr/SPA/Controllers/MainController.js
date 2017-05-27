var MainController = function ($scope,$http) {
    $scope.varijabla = "Ajmooo kesuuuj";
    $scope.drugaVarijabla = "druga";

    function getProjects() {
        $http.get('/api/Projects/GetProjects').then(function(response) {
            $scope.projects = response.data;
        })
    }

    getProjects();
}

MainController.$inject = ['$scope','$http'];