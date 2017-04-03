(function() {

    var app = angular.module("Billing");

    var LoginCtrl = function($scope, $rootScope, $http, $location, LoginService, BillingConfig) {

        $scope.login = function() {
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
                    console.log(credentials.currentUser);
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