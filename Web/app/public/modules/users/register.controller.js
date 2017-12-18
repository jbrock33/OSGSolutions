(function() {
    "use strict";
    angular.module("publicApp")
            .controller("registerController", RegisterController);

    RegisterController.$inject = ["$scope", "registerService", "$state"];

    function RegisterController($scope, RegisterService, $state) {
        var vm = this;
        vm.$scope = $scope;
        vm.$state = $state;
        vm.$onInit = _$onInit;
        vm.RegisterService = RegisterService;
        vm.register = _register;
        vm.httpSuccess = _httpSuccess;
        vm.httpError = _httpError;
        vm.registerUser = {};



        function _$onInit() {
            console.log("Register controller initialized");
            console.log(vm.RegisterService.register);
        };

        function _register() {
            console.log(vm.registerUser);
            vm.RegisterService.register(vm.registerUser)
                .then(vm.httpSuccess).catch(vm.httpError);
        };

        function _httpSuccess(res) {
            console.log(res);
            $state.go("home");
        };

        function _httpError(err) {
            console.log(err);
        };
    };
})();