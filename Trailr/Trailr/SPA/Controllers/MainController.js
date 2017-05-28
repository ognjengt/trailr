var MainController = function ($scope,$http) {
    $scope.varijabla = "Ajmooo kesuuuj";
    $scope.drugaVarijabla = "druga";
    var userMail;

    function getProjects() {
        $http.get('/api/Projects/GetProjects').then(function(response) {
            $scope.projects = response.data;
        })
    }

    function getUserMail() {
        $http.get('/Dashboard/GetUserMail').then(function(response) {
            userMail = response.data;
            console.log(userMail);
        })
    }

    getProjects();
    getUserMail();
}

MainController.$inject = ['$scope','$http'];