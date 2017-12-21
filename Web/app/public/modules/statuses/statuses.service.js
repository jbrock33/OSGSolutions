(function () {
    "use strict";
    angular.module("publicApp")
        .factory("statusesService", StatusesService);

    StatusesService.$inject = ["$http", "$q"];

    function StatusesService($http, $q) {
        return {
            getAll: _getAll,
            getById: _getById,
            insert: _insert,
            update: _update,
            delete: _delete
        };

        function _getAll() {
            return $http.get('/api/statuses/getall')
                .then(httpSuccess).catch(httpError);
        };

        function _getById(id) {
            return $http.get('/api/statuses', id, { withCredentials: true })
                .then(httpSuccess).catch(httpError);
        };

        function _insert(data) {
            return $http.post('/api/statuses/', data, { withCredentials: true })
                .then(httpSuccess).catch(httpError);
        };

        function _update(data) {
            return $http.put('/api/statuses/' + data.id, data, { withCredentials: true })
                .then(httpSuccess).catch(httpError);
        };

        function _delete(id) {
            return $http.delete('/api/statuses/' + id, { withCredentials: true })
                .then(httpSuccess).catch(httpError);
        };

        function httpSuccess(res) {
            return res;
        };

        function httpError(err) {
            return $q.reject(err);
        };
    };
})();