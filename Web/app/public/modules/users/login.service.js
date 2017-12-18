(function() {
    "use strict";

    angular.module("publicApp")
            .factory("loginService", LoginService);

    LoginService.$inject = ["$http", "$q"];

    function LoginService($http, $q) {
        return {
            login: _login
        };

        function _login(data) {
            return $http.post("http://localhost:54297/api/users/login", data, { withCredentials: true })
                .then(_success).catch(_error);
        };

        function _success(res) {
            return res;
        };

        function _error(err) {
            return $q.reject(err);
        };
    };
})();