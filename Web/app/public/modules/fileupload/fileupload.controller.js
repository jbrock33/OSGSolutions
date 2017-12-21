(function() {
    "use strict";
    angular.module("publicApp")
        .component("upload", {
            templateUrl: "/app/public/modules/fileupload/upload.html",
            controller: "fileUploadController"
        });
})();

(function () {
    "use strict";
    angular.module("publicApp")
        .controller("fileUploadController", FileUploadController);

    FileUploadController.$inject = ["$scope", "fileUploadService", "$state"];

    function FileUploadController($scope, FileUploadService, $state) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _$onInit;
        vm.$state = $state;
        vm.FileUploadService = FileUploadService;
        vm.uploadFile = _uploadFile;
        vm.httpSuccess = _httpSuccess;
        vm.httpError = _httpError;
        vm.uploadImage = {};
        vm.cropper = {};
        vm.cropper.sourceImage = null;
        vm.cropper.croppedImage = null;
        vm.bounds = {
            top: 0, left: 0, bottom: 0, right: 0
        };

        function _$onInit() {
            console.log("File upload controller initialized");
        };

        function _uploadFile() {
            var image = vm.cropper.croppedImage;
            var imageInfo = image.split(",");
            var extension = imageInfo[0].split("/")[1].split(";");
            vm.uploadImage.encodedImageFile = imageInfo[1];
            vm.uploadImage.fileExtension = "." + extension[0];

            vm.FileUploadService.upload(vm.uploadImage)
                .then(vm.httpSuccess).catch(vm.httpError);
        };

        function _httpSuccess(res) {
            toastr.options.progressBar = true;
            toastr.options.showMethod = "slideDown";
            toastr.success("File Upload Success!");
            vm.$state.go("home");
        };

        function _httpError(err) {
            console.log(err);
        };
    };
})();