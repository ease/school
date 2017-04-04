(function() {

    var app = angular.module("Billing");

    var LoginCtrl = function($scope, $rootScope, $http, $location, LoginService) {

        $http.get("config.json").then(function(response){
            BillingConfig = response.data;
            $scope.debug = BillingConfig.debugMode;
        });

        $scope.loginAs = function(username){
            $scope.user = { name : username, pass : "billing" }
            enterSystem();
        };

        $scope.login = function() {
            enterSystem();
        };

        function enterSystem() {
            $http.defaults.headers.common.Authorization = "Basic " + LoginService.encode($scope.user.name + ":" + $scope.user.pass);
            var promise = $http({
                method: "post",
                url: BillingConfig.source + "login",
                data: {
                    "apiKey": BillingConfig.apiKey,
                    "signature": BillingConfig.signature
                }});
            promise.then(
                function(response) {
                    credentials = response.data;
                    console.log(credentials);
                    $rootScope.currentUser = credentials.currentUser.name;
                    $location.path("/agents");
                },
                function(reason){
                    authntication = false;
                    currentUser = "";
                    $location.path("/login");
                });
        };
    };
    app.controller("LoginCtrl", LoginCtrl);

    var LogoutCtrl = function($http, BillingConfig) {

        var request = $http({
            method: "get",
            url: BillingConfig.source + "logout"
        });
        request.then(
            function (response) {
                authenticated = false;
                return true;
            },
            function (reason) {
                return false;
            });
    }
    app.controller("LogoutCtrl", LogoutCtrl);

}());