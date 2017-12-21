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
        vm.indeedJobs = [];
        vm.indeedJobs1 = [];
        vm.indeedJobs2 = [];

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
            res.data.item.items.forEach(function (item) {
                var listItem = $(item.postUrl)[0];
                var jobTitle = listItem.innerText;
                var jobUrl = listItem.innerHTML.slice(14, 54);
                jobTitle = jobTitle.substr(5, jobTitle.length - 10);
                if (jobUrl.substr(0, 4) === "/rc/") {
                    vm.indeedJobs.push({ jobTitle: jobTitle, jobUrl: "https://www.indeed.com" + jobUrl });
                };
            });
            vm.indeedJobs1 = vm.indeedJobs.slice(0, 5);
            vm.indeedJobs2 = vm.indeedJobs.slice(5, 10);
        };

        function _httpError(err) {
            console.log(err);
        };
    }
})();