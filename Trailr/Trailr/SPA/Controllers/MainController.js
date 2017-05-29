var MainController = function ($scope,$http) {
    $scope.addPopupVisible = false;
    var userMail;

    $scope.projects = localStorage.getItem('projects');

    function getProjects(email) {
        $http.get('/api/Projects/GetProjects/?email='+email).then(function(response) {
            $scope.projects = response.data;
            localStorage.setItem('projects', JSON.stringify($scope.projects));
        })
    }

    function getUserMail() {
        $http.get('/Dashboard/GetUserMail').then(function(response) {
            userMail = response.data;
            getProjects(userMail);
        })
    }

    getUserMail();

    $scope.toggleAddPopup = function() {
        $scope.addPopupVisible = !$scope.addPopupVisible;
    }
    
}

MainController.$inject = ['$scope','$http'];