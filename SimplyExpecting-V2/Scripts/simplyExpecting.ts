///<references path="typings\jquery\jquery.d.ts" />
///<reference path="viewModels.ts"/>
///<reference path="typings\angularjs\angular.d.ts"/>
///<reference path="typings\angularjs\angular-route.d.ts"/>

angular.module("SimplyExpecting", ["viewModels"])
    .factory("contentStore", () => new ContentStore());
    

