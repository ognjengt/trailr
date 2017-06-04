var MainController = function ($scope,$http) {
    $scope.addPopupVisible = false;
    var userMail;

    $scope.projects = localStorage.getItem('projects');

    function getProjects(email) {
        $http.get('/api/Projects/GetProjects/?email='+email).then(function(response) {
            response.data.forEach(function(project) {
                project.HoursSpent = project.HoursSpent.pad();
                project.MinutesSpent = project.MinutesSpent.pad();
                project.SecondsSpent = project.SecondsSpent.pad();
            });
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

    // On init
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
                    localStorage.setItem('projects', JSON.stringify(response.data));
                    console.log(response.data);
                    $scope.projects = response.data;
                    $scope.addPopupVisible = false;
        })
    }
    
}

MainController.$inject = ['$scope','$http'];