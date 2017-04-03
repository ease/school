(function(){

    SOURCE = "http://localhost:9000/api/";
    API_KEY = "QWxwaGEtQmlsbGluZw==";
    SIGNATURE = "+jL4gEGBnpMrhaDETCiJjo56i0LTW5Jco1dncoSRNf4=";
    REGIONS = [ "Banja Luka", "Bihac", "Doboj", "Mostar", "Sarajevo", "Trebinje", "Tuzla", "Zenica" ];
    STATES = [ "Canceled", "OrderCreated", "OrderConfirmed", "InvoiceCreated", "InvoiceSent", "InvoicePaid", "OnHold", "Ready", "Shipped" ];

    credentials = {
        token: "",
        expiration: "",
        currentUser: {
            id: 0,
            name: "",
            role: ""
        }
    };
    
    function authenticated() {
        return (credentials.currentUser.id != 0)
    } 

    var app = angular.module("Billing", ["ngRoute"]);

    app.constant("BillingConfig", {
        source: "http://localhost:9000/api/",
        apiKey: "QWxwaGEtQmlsbGluZw==",
        signature: "+jL4gEGBnpMrhaDETCiJjo56i0LTW5Jco1dncoSRNf4=",
        regions: [ "Banja Luka", "Bihac", "Doboj", "Mostar", "Sarajevo", "Trebinje", "Tuzla", "Zenica" ],
        states: [ "Canceled", "OrderCreated", "OrderConfirmed", "InvoiceCreated", "InvoiceSent", "InvoicePaid", "OnHold", "Ready", "Shipped" ]
    });

    app.config(function($routeProvider){
        $routeProvider
            .when("/agents", {
                templateUrl: "views/agents.html",
                controller: "AgentsCtrl" })
            .when("/customers", {
                templateUrl: "views/customers.html",
                controller: "CustomersCtrl" })
            .when("/login", {
                templateUrl: "views/login.html",
                controller: "LoginCtrl" })
            .otherwise({ redirectTo: "/agents" });
    }).run(function($rootScope, $location){
        $rootScope.$on("$routeChangeStart", function(event, next, current){
            if(!authenticated()){
                if(next.templateUrl != "views/login.html"){
                    $location.path("/login");
                }
            }
        })
    });
}());



