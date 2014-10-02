///<reference path="clientSocket.ts"/>
///<reference path="models.ts"/>                     
///<reference path="typings\localForage\localForage.d.ts"/>

interface IContentStore {
    IsConnectedToContentService: () => void;
    HasNewContent: (content: IContent) => void;
    Initialize(): JQueryDeferred<boolean>;
    RegisterBeforeStorageFunction(sectionId: Sections, func: (content: IContent) => IContent);
    Get(sectionId: number): JQueryDeferred<Content>;
}

enum HttpStatus { OK = 200 };

class ContentStore implements IContentStore {
        
    private contentUrl: string = window.location.protocol + "//" +  window.location.host + "/api/content/";
    private pushUrl = this.contentUrl.replace("http", "ws");
    private useWebSocket: boolean = false;
    private socket: ClientSocket;
    private localForage: lf.ILocalForage<IContent> = <lf.ILocalForage<IContent>>window["localforage"];
    private processBeforeStorage: Array<{ SectionId: Sections; Functions: Array<(content: IContent) => IContent> }> = new Array();
    private editor: ContentEditor;


    public IsConnectedToContentService: () => void;
    public HasNewContent: (content: IContent) => void;

    public Initialize(showEditElement: string = null, hideEditElement: string = null): JQueryDeferred<boolean> {
        var cls = this;
        var deferred = $.Deferred();
        if (WebSocket && this.useWebSocket) {
            this.socket = new ClientSocket(this.pushUrl, connected => {
                cls.useWebSocket = true;
                this.socket.NewMessageReceived = content => {
                    cls.StoreContent(content);
                    if (cls.HasNewContent) cls.HasNewContent(content);
                };
                
                deferred.resolve(connected);
            });
        }
        else
            deferred.resolve(true);

        return deferred;
    }

    public RegisterBeforeStorageFunction(sectionId: Sections, func: (content: IContent) => IContent) {
        if (sectionId == undefined || func == undefined) return;

        if (this.processBeforeStorage.filter(item => item.SectionId == sectionId).length == 0)
            this.processBeforeStorage.push({ SectionId: sectionId, Functions: new Array() });// Array<(content: IContent) => IContent>() });

        this.processBeforeStorage.filter(item => item.SectionId== sectionId)[0].Functions.push(func);
    }

    //Gets content from the store for a particular Section.If there is no content, its sourced from the service. 
    //If there is, the sevice is queried for a newer version and downloaded if necessary.
    public Get(sectionId: number): JQueryDeferred<Content>{// ng.IPromise<IContent> {
        var cls = this;
        var deferred = $.Deferred();// cls.qService.defer();

        var content = this.GetContentFromStore(sectionId).then(content => {
            if (cls.useWebSocket) {
                deferred.resolve(cls.GetContentFromWebSocket(sectionId, content));
            }
            else {
                //otherwise query the server for a newer version with whats already in localStorage
                this.GetContentFromService(sectionId, content)
                    .then(content => {
                        deferred.resolve(content);
                    });
            }
        });

        return deferred;
    }

    public PostContent(content: Content): JQueryDeferred<Content> {
        var cls = this;
        var deferred = $.Deferred();
        if (cls.useWebSocket) {
            cls.PostContentToWebSocket(content);
            return deferred;
        }
        else {
            return cls.PostContentToService(content)
        }
    } 

    private GetContentFromStore(sectionId: number): lf.IPromise<IContent> {
        return this.localForage.getItem(sectionId.toString());
    }

    private GetContentFromWebSocket(sectionId: number, content : IContent) : IContent {
        //if there is alrady valid content it will be the latest so use it
        if (content != null) return content;
        
        //if we're here, there is no content for this page yet, get from the websocket by sending a version value of 0, 
        //this will get the latest version from the service
        this.socket.GetContent(new Content(0, 0, sectionId, ""));
    }

    private PostContentToWebSocket(content: IContent): IContent {
        //if there is alrady valid content it will be the latest so use it
        if (content == null) return content;

        //if we're here, there is no content for this page yet, get from the websocket by sending a version value of 0, 
        //this will get the latest version from the service
        this.socket.SendContent(content);
    }

    private GetContentFromService(sectionId : number, content: IContent): JQueryDeferred<Content> {//: ng.IPromise<IContent> {
        var deferred = $.Deferred();// this.qService.defer();
        var cls = this;
        var currentContent = content;
        var version = content != null ? content.Version : 0;
       $.get(this.contentUrl + sectionId + "/" + version, data=>
            {
                if (data != null && data.Version != version) {
                    //this content is a newer version
                    currentContent = <Content>data;
                    cls.StoreContent(currentContent);
                }

                deferred.resolve(currentContent);
            });

        return deferred;
    }

    private PostContentToService(content: IContent): JQueryDeferred<Content> {//: ng.IPromise<IContent> {
        var deferred = $.Deferred();// this.qService.defer();
        var cls = this;

        var version = content != null ? content.Version : 0;
        $.post(this.contentUrl, JSON.stringify({ Message: "PostContent", Content: { Content: content } }), data=> {
            var content = <Content>(data.Content);
            if (content != null && content.Version != version) {
                //this content is a newer version
                cls.StoreContent(content);
            }

            deferred.resolve(content);
        });

        return deferred;
    }

    public StoreContent(content: IContent): lf.IPromise<IContent> {
        //call any registered fucntions to process this content before storing it
        var functions = this.processBeforeStorage.filter(item => item.SectionId == content.SectionId);
        if (functions.length == 1)
            functions[0].Functions.forEach(f => content = f(content));

        //check that theres enough space to store the new content
        return this.localForage.setItem(content.SectionId.toString(), content);
    }

    public EditContent(editArea: HTMLElement, sectionId: Sections) {
        var editor: ContentEditor;
        //this.GetContentFromStore(sectionId).then(content => editor = new ContentEditor(editArea, content));

    }
} 