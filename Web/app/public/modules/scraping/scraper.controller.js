(function() {
    "use strict";
    angular.module("publicApp")
        .component("scraper", {
            templateUrl: "/app/public/modules/scraping/scraper.html",
            controller: "scraperController"
        });
})();

(function () {
    "use strict";
    angular.module("publicApp")
        .controller("scraperController", ScraperController);

    ScraperController.$inject = ["$scope", "scraperService"];

    function ScraperController($scope, ScraperService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _$onInit;
        vm.ScraperService = ScraperService;
        vm.getAll = _getAll;
        vm.httpSuccess = _httpSuccess;
        vm.httpError = _httpError;

        function _$onInit() {
            console.log("Scraper controller initialized");
            vm.getAll();
        };

        function _getAll() {
            vm.ScraperService.getAll()
                .then(vm.httpSuccess).catch(vm.httpError);
        };

        function _httpSuccess(res) {
            console.log(res);
        };

        function _httpError(err) {
            console.log(err);
        };
    }
})();