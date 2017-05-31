var ProjectController = function ($scope,$http,$location) {
    var userMail;
    var currentId;
    var id;

    $scope.project = {};

    function getProjectInfo() {
      id = $location.$$absUrl.substring($location.$$absUrl.indexOf('/Project/'),$location.$$absUrl.length).replace('/Project/','');
      $http.get('/api/Projects/GetProjectInfo/?id='+id).then(function(response) {
        $scope.project = response.data;
      })
    }

    function getUserMail() {
        $http.get('/Dashboard/GetUserMail').then(function(response) {
            userMail = response.data;
            getProjectInfo(userMail);
        })
    }

    getUserMail();

}

ProjectController.$inject = ['$scope','$http','$location'];