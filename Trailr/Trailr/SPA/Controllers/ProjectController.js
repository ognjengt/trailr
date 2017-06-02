var ProjectController = function ($scope,$http,$location,$interval) {
    var userMail;
    var currentId;
    var id;
    var timer = null;

    $scope.project = {};
    $scope.stopwatchActivated = false;
    $scope.time = 0;

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

    $scope.handleStopwatch = function() {
        // TODO stop stopwatch
        $scope.stopwatchActivated = !$scope.stopwatchActivated;
        console.log($scope.stopwatchActivated);
        if($scope.stopwatchActivated) {
            timer = $interval(function(){
                $scope.time++;
            },1000)
        }
        else {
            $interval.cancel(timer);
        }

    }

}

ProjectController.$inject = ['$scope','$http','$location','$interval'];