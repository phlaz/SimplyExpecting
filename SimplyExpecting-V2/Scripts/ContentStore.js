﻿///<reference path="clientSocket.ts"/>
///<reference path="models.ts"/>
///<reference path="typings\localForage\localForage.d.ts"/>
///<reference path="typings\angularjs\angular.d.ts"/>

var ContentStore = (function () {
    function ContentStore() {
        this.contentUrl = window.location.protocol + "//" + window.location.host + "/api/content/";
        this.pushUrl = this.contentUrl.replace("https", "wss");
        this.useWebSocket = true;
        this.localForage = window["localforage"];
        this.processBeforeStorage = new Array();
    }
    ContentStore.prototype.Initialize = function () {
        var _this = this;
        var cls = this;

        //var deferred = cls.qService.defer();
        var deferred = $.Deferred();
        if (WebSocket && this.useWebSocket) {
            this.socket = new ClientSocket(this.pushUrl, function (connected) {
                cls.useWebSocket = true;
                _this.socket.NewMessageReceived = function (message) {
                    cls.StoreContent(message);
                    if (cls.HasNewContent)
                        cls.HasNewContent(message);
                };
                deferred.resolve(connected);
            });
        } else
            deferred.resolve(true);

        return deferred;
    };

    ContentStore.prototype.RegisterBeforeStorageFunction = function (sectionId, func) {
        if (sectionId == undefined || func == undefined)
            return;

        if (this.processBeforeStorage.filter(function (item) {
            return item.SectionId == sectionId;
        }).length == 0)
            this.processBeforeStorage.push({ SectionId: sectionId, Functions: new Array() }); // Array<(content: IContent) => IContent>() });

        this.processBeforeStorage.filter(function (item) {
            return item.SectionId == sectionId;
        })[0].Functions.push(func);
    };

    //Gets content from the store for a particular Section.If there is no content, its sourced from the service.
    //If there is, the sevice is queried for a newer version and downloaded if necessary.
    ContentStore.prototype.Get = function (sectionId) {
        var _this = this;
        var cls = this;
        var deferred = $.Deferred();

        var content = this.GetContentFromStore(sectionId).then(function (content) {
            if (cls.useWebSocket) {
                deferred.resolve(cls.GetContentFromWebSocket(sectionId, content));
            } else {
                //otherwise query the server for a newer version with whats already in localStorage
                _this.GetContentFromService(sectionId, content).then(function (content) {
                    deferred.resolve(content);
                });
            }
        });

        return deferred;
    };

    ContentStore.prototype.GetContentFromStore = function (sectionId) {
        return this.localForage.getItem(sectionId.toString());
    };

    ContentStore.prototype.GetContentFromWebSocket = function (sectionId, content) {
        //if there is alrady valid content it will be the latest so use it
        if (content != null)
            return content;

        //if we're here, there is no content for this page yet, get from the websocket by sending a version value of 0,
        //this will get the latest version from the service
        this.socket.GetContent(sectionId, 0);
    };

    ContentStore.prototype.GetContentFromService = function (sectionId, content) {
        var deferred = $.Deferred();
        var cls = this;

        var version = content != null ? content.Version : 0;
        $.get(this.contentUrl + sectionId + "/" + version, function (data) {
            var content = data;
            if (content != null && content.Version != version) {
                //this content is a newer version
                cls.StoreContent(content);
            }

            deferred.resolve(content);
        });

        return deferred;
    };

    ContentStore.prototype.StoreContent = function (content) {
        //call any registered fucntions to process this content before storing it
        var functions = this.processBeforeStorage.filter(function (item) {
            return item.SectionId == content.SectionId;
        });
        if (functions.length == 1)
            functions[0].Functions.forEach(function (f) {
                return content = f(content);
            });

        //check that theres enough space to store the new content
        return this.localForage.setItem(content.SectionId.toString(), content);
    };
    return ContentStore;
})();
//# sourceMappingURL=contentStore.js.map