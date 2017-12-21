(function() {
    "use strict";
    angular.module("publicApp")
        .component("status", {
            templateUrl: "/app/public/modules/statuses/status.html",
            controller: "statusesController"
        });
})();

(function () {
    "use strict";
    angular.module("publicApp")
        .controller("statusesController", StatusesController);

    StatusesController.$inject = ["$scope", "statusesService"];

    function StatusesController($scope, StatusesService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _$onInit;
        vm.StatusesService = StatusesService;
        vm.httpError = _httpError;
        vm.getAllSuccess = _getAllSuccess;
        vm.getByIdSuccess = _getByIdSuccess;
        vm.updateSuccess = _updateSuccess;
        vm.postSuccess = _postSuccess;
        vm.deleteSuccess = _deleteSuccess;
        vm.deleteId = _deleteId;
        vm.pageUp = _pageUp;
        vm.pageBack = _pageBack;
        vm.getDisplayList = _getDisplayList;
        vm.updateEntry = _updateEntry;
        vm.submitEntry = _submitEntry;
        vm.getAll = _getAll;
        vm.activeItem = null;
        vm.statusDisplayList = [];
        vm.items = [];
        vm.bottom = 0;
        vm.begin = 0;
        vm.end = 9;
        vm.maxPage = 1;
        vm.increment = 10;
        vm.displayed = 0;
        vm.page = 1;
        vm.remove = null;

        function _$onInit() {
            console.log("Status controller initialized");
            vm.getAll();
            console.log(vm.delete);
        };

        function _getAll() {
            vm.StatusesService.getAll()
                .then(vm.getAllSuccess).catch(vm.httpError);
        };

        function _deleteId(id) {
            vm.delete = id;
            vm.StatusesService.delete(id)
                .then(vm.deleteSuccess).catch(vm.httpError);
        };

        function _getDisplayList() {
            vm.item = {};
            vm.activeEdit = null;
            vm.statusDisplayList = [];
            var j = 0;
            if (vm.items.length < vm.end) {
                vm.end = vm.items.length;
            };
            for (var i = vm.begin; i < vm.end; i++) {
                if (vm.items[i]) {
                    vm.statusDisplayList.push(vm.items[i]);
                    j++;
                };
            };
        };

        function _submitEntry() {
            console.log(vm.item);
            vm.StatusesService.insert(vm.item)
                .then(vm.postSuccess).catch(vm.httpError);
        };

        function _updateEntry() {
            console.log(vm.item);
            vm.StatusesService.update(vm.item)
                .then(vm.updateSuccess).catch(vm.httpError);
        };

        function _getMax() {
            console.log(vm.maxPage);
            vm.maxPage = ((vm.increment - (vm.items.length % vm.increment) + vm.items.length) / vm.increment);
            console.log(vm.maxPage);
        };
        

        function _getAllSuccess(res) {
            console.log(res);
            vm.items = res.data.items;
            vm.items.sort(function (a, b) {
                var x = a.Modified;
                var y = b.Modified;
                if (x < y) { return -1; }
                if (x > y) { return 1; }
                return 0;
            });
            vm.items.sort(function (a, b) {
                var x = a.statusId;
                var y = b.statusId;
                if (x < y) { return 1; }
                if (x > y) { return -1; }
                return 0;
            });
            vm.getDisplayList();
            _getMax();
            vm.end = 9;
            vm.begin = 0;
        };

        function _pageUp(last) {
            if (last) {
                vm.page = vm.maxPage;
                vm.begin = vm.items.length - 1 - vm.items.length % vm.increment;
                vm.end = vm.items.length
            } else {
                if (vm.page < vm.maxPage) {
                    vm.page += 1;
                    vm.begin += vm.increment - 1;
                    if (vm.end + vm.increment - 1 >= vm.items.length) {
                        vm.end = vm.items.length;
                    } else {
                        vm.end += vm.increment - 1;
                    }
                };
            };
            vm.getDisplayList();
        };

        function _pageBack(first) {
            if (first) {
                vm.page = 1;
                vm.begin = 0;
                vm.end = vm.increment - 1;
            } else {
                if (vm.page > 1) {
                    vm.page -= 1;
                    vm.begin -= vm.increment - 1;
                    vm.end -= vm.increment + - 1;
                    if (vm.end < vm.increment) {
                        vm.end = vm.increment - 1;
                    };
                    console.log(vm.end);
                };
            };
            vm.getDisplayList();
        };

        function _getByIdSuccess(res) {
            console.log(res);
        };

        function _updateSuccess(res) {
            console.log(res);
            toastr.options.progressBar = true;
            toastr.options.showMethod = "slideDown";
            toastr.success("Update Success!");
            vm.getAll();
        };

        function _postSuccess(res) {
            console.log(res);
            toastr.options.progressBar = true;
            toastr.options.showMethod = "slideDown";
            toastr.success("Submit Success!");
            vm.getAll();
        };

        function _deleteSuccess(res) {
            //console.log(res);
            //var removeIndex = vm.items.map(function (item) { return item.id; })
            //    .indexOf(vm.remove);

            //~removeIndex && vm.items.splice(removeIndex, 1);
            //vm.statusDisplayList = vm.items;
            //vm.remove = null;
            toastr.options.progressBar = true;
            toastr.options.showMethod = "slideDown";
            toastr.success("Delete Success!");
            vm.getAll();
        };

        function _httpError(err) {
            console.log(err);
        };
    };
})();