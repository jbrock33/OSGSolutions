(function () {
    "use strict";

    angular.module("publicApp")
        .controller("loginController", LoginController);

    LoginController.$inject = ["$scope", "loginService", "$state"];

    function LoginController($scope, LoginService, $state) {
        var vm = this;
        vm.$scope = $scope
        vm.$state = $state;
        vm.LoginService = LoginService;
        vm.$onInit = _$onInit;
        vm.login = _login;
        vm.httpSuccess = _httpSuccess;
        vm.httpError = _httpError;
        vm.loginUser = {};


        function _$onInit() {
            console.log("Login controller initialized");
        };

        function _login() {
            vm.LoginService.login(vm.loginUser)
                .then(vm.httpSuccess).catch(vm.httpError);
        };

        function _httpSuccess(res) {
            console.log(res);
            if (res.data === true) {
                toastr.options.progressBar = true;
                toastr.options.showMethod = "slideDown";
                toastr.success("Log In Success!");
                vm.$state.go("home");
            } else {
                vm.invalidMsg = "Incorrect email or password!";
            }
        };

        function _httpError(err) {
            console.log(err);
        };
    }
})();