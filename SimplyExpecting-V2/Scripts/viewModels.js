///<reference path="typings\angularjs\angular.d.ts" />
angular.module("viewModels", []).controller("ContentController", function ($scope, content) {
    new MainPageViewModel($scope, content);
});

var MainPageViewModel = (function () {
    function MainPageViewModel($scope, content) {
        var _this = this;
        this.content = content;
        this.SectionId = 2 /* Home */;
        this.ImageUrls = new Array();
        $scope.getPageContent = function () {
            return _this.content.Get(_this.SectionId).done(function (content) {
                _this.SectionContent = content.Html;
            });
        };

        $scope.getMenu = function () {
            return _this.content.Get(1 /* Menu */).done(function (content) {
                return _this.Menu = content.Html;
            });
        };

        this.content.Initialize().then(function (connected) {
            if (connected) {
                _this.content.HasNewContent = function (content) {
                    if (content.SectionId == _this.SectionId)
                        _this.SectionContent = content.Html;
                };

                $scope.getMenu();
                _this.SectionId = 2 /* Home */;
                $scope.getPageContent();
            }
        });
    }
    MainPageViewModel.prototype.SetupContentStore = function () {
        //register a function to transform the Menu structure to a list
        this.content.RegisterBeforeStorageFunction(1 /* Menu */, function (content) {
            content.Html = new MenuBuilder(content).Html;
            return content;
        });
    };
    return MainPageViewModel;
})();
//# sourceMappingURL=viewModels.js.map
