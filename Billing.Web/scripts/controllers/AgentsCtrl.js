(function(){

    var app = angular.module("Billing");

    var AgentsCtrl = function($scope, $http) {

        $scope.showAgent = false;
        ListAgents();

        $scope.getAgent = function(currentAgent){
            $scope.agent = currentAgent;
            $scope.showAgent = true;
        };

        $scope.save = function(){
            console.log($scope.agent);
            var promise = $http({
                method: "put",
                url: "http://localhost:9000/api/agents/" + $scope.agent.id,
                data: $scope.agent
            });

            $scope.message = "Please wait...";
            promise.then(function(response){
                $scope.agent = response.data;
                $scope.message = " ";
                ListAgents();
            }, function(reason){
                $scope.message = "No data for that request";
            });
        };

        $scope.new = function(){
            $scope.agent.id = 0;
            var promise = $http({
                method: "post",
                url: "http://localhost:9000/api/agents",
                data: $scope.agent
            });
            $scope.message = "Please wait...";
            promise.then(function(response){
                $scope.agent = response.data;
                $scope.message = " ";
                ListAgents();
            }, function(reason){
                $scope.message = "No data for that request";
            });
        };

        function ListAgents(){
            var promise = $http.get("http://localhost:9000/api/agents");
            $scope.message = "Please wait...";
            promise.then(function(response){
                $scope.agents = response.data;
                $scope.message = " ";
            }, function(reason){
                $scope.message = "No data for that request";
            });
        }
    };

    app.controller("AgentsCtrl", AgentsCtrl);

}());
