(function() {
    "use strict";
    
    angular.module("publicApp")
        .factory("registerService", RegisterService);

    RegisterService.$inject = ["$http", "$q"];

    function RegisterService($http, $q) {
        return {
            register: _register
        };

        function _register(data) {
            return $http.post("http://localhost:54297/api/users", data, { withCredentials: true })
                .then(success).catch(err);
        };

        function success(res) {
            return res;
        };

        function err(err) {
            return $q.reject(err);
        };
    };
})();