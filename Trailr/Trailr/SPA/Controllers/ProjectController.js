var ProjectController = function ($scope,$http,$location,$interval) {
    var userMail;
    var currentId;
    var id;
    var timer = null;

    $scope.project = {};
    $scope.stopwatchActivated = false;

    function getProjectInfo() {
      id = $location.$$absUrl.substring($location.$$absUrl.indexOf('/Project/'),$location.$$absUrl.length).replace('/Project/','');
      $http.get('/api/Projects/GetProjectInfo/?id='+id).then(function(response) {
        $scope.project = response.data;
        $scope.project.HoursSpent = $scope.project.HoursSpent.pad();
        $scope.project.MinutesSpent = $scope.project.MinutesSpent.pad();
        $scope.project.SecondsSpent = $scope.project.SecondsSpent.pad();
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
                $scope.project.SecondsSpent++;
                // Dodaj 0
                $scope.project.SecondsSpent = $scope.project.SecondsSpent.pad();
                if($scope.project.SecondsSpent == 60) {
                    $scope.project.SecondsSpent = 0;
                    $scope.project.MinutesSpent++;
                    // Dodaj 0
                    $scope.project.MinutesSpent = $scope.project.MinutesSpent.pad();

                    // Update the database every minute
                    var updateReq = {
                        Id: $scope.project.Id,
                        HoursPassed: parseInt($scope.project.HoursSpent),
                        MinutesPassed: parseInt($scope.project.MinutesSpent),
                        SecondsPassed: parseInt($scope.project.SecondsSpent)
                    };
                    $http.post('/api/Projects/UpdateProject',JSON.stringify(updateReq),{
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }).then(function(response) {
                        //console.log(response);
                    });

                    if($scope.project.MinutesSpent == 60) {
                        $scope.project.MinutesSpent = 0;
                        $scope.project.HoursSpent++;
                        // Dodaj 0
                        $scope.project.HoursSpent = $scope.project.HoursSpent.pad();
                    }
                }
            },1000)
        }
        else {
            $interval.cancel(timer);

            // Update the database on timer stop.
            var updateReq = {
                Id: $scope.project.Id,
                HoursPassed: parseInt($scope.project.HoursSpent),
                MinutesPassed: parseInt($scope.project.MinutesSpent),
                SecondsPassed: parseInt($scope.project.SecondsSpent)
            };
            $http.post('/api/Projects/UpdateProject',JSON.stringify(updateReq),{
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(function(response) {
                console.log(response);
            });

        }

    }

}

ProjectController.$inject = ['$scope','$http','$location','$interval'];