///<reference path="typings\angularjs\angular.d.ts" />


angular.module("viewModels", []).controller("ContentController",
    function ($scope: IMainPageViewModel, content: ContentStore) {
        new MainPageViewModel($scope, content);
    });

interface IMainPageViewModel extends ng.IScope {

    getMenu: () => void;
    getPageContent: () => void;
}

class MainPageViewModel {
    
    constructor($scope: IMainPageViewModel, private content: ContentStore) {
        
        $scope.getPageContent = () => this.content.Get(this.SectionId).done(content => {
            this.SectionContent = content.Html; 
        });

        $scope.getMenu = () => this.content.Get(Sections.Menu).done(content => this.Menu = content.Html);


        this.content.Initialize().then(connected => {
            if (connected) {
                this.content.HasNewContent = content => {
                    if (content.SectionId == this.SectionId)
                        this.SectionContent = content.Html;
                }

                $scope.getMenu();
                this.SectionId = Sections.Home;
                $scope.getPageContent();
            }
        })
    }

    public SectionId: Sections = Sections.Home;

    public SectionContent: string;

    public ImageUrls: Array<string> = new Array<string>();

    public Menu: string;

    SetupContentStore(): void {
        //register a function to transform the Menu structure to a list
        this.content.RegisterBeforeStorageFunction(Sections.Menu, content => {
            content.Html = new MenuBuilder(content).Html;
            return content;
        });

        
    }
}