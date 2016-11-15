var app = angular.module("app", [])

app.controller("registerCTRL", function($http) {
    document.getElementById("userName").addEventListener("focusout", () => {
        console.log("event")
        var name = $("#userName").val();
        $http.get("/api/TwitUsername/" + name).then((response) => {
            console.log(response);
        });
    });
});