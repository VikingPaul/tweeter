var app = angular.module("app", [])

app.controller("registerCTRL", function ($http) {
    $('#register').attr("disabled", "disabled")
    document.getElementById("userName").addEventListener("focusout", () => {
        console.log("event")
        var name = $("#userName").val();
        $http.get("/api/TwitUsername?candidate=" + name).then((response) => {
            console.log(response.data);
            if (response.data.exists) {
                $('#register').attr("disabled", "disabled")
            } else {
                $('#register').attr("disabled", null)
            }
        });
    });
});