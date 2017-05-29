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

    $scope.addProject = function(){
        var data = {
           Title: $scope.projectTitleInput,
           UserEmail: userMail
        };
        $http.post('/api/Projects/AddProject',JSON.stringify(data),{
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(function(response) {
                    // push to local storage
                    $scope.projects = response.data;
                    $scope.addPopupVisible = false;
        })
    }
    
}

MainController.$inject = ['$scope','$http'];