(function() {
    "use strict";
    angular.module("publicApp")
        .factory("fileUploadService", FileUploadService);

    FileUploadService.$inject = ["$http", "$q"];

    function FileUploadService($http, $q) {
        return {
            upload: _upload
        };

        function _upload(data) {
            return $http.post("/api/upload/image", data, { withCredentials: true })
                .then(success).catch(error);
        };

        function success(res) {
            return res;
        };

        function error(err) {
            return $q.reject(err);
        };
    };
})();


