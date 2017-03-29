(function(){

    var app = angular.module("Billing", ["ngRoute"]);

    app.config(function($routeProvider){
        $routeProvider
            .when("/main", {
                templateUrl: "views/main.html" })
            .when("/agents", {
                templateUrl: "views/agents.html",
                controller: "AgentsController" })
            .when("/customers", {
                templateUrl: "views/customers.html",
                controller: "CustomersController" })
            .otherwise({ redirectTo: "/main" });
    });

}());
