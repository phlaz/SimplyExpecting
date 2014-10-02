var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Sections;
(function (Sections) {
    Sections[Sections["Menu"] = 1] = "Menu";
    Sections[Sections["Home"] = 2] = "Home";
})(Sections || (Sections = {}));

var Content = (function () {
    function Content(Id, Version, SectionId, Html) {
        this.Id = Id;
        this.Version = Version;
        this.SectionId = SectionId;
        this.Html = Html;
    }
    Content.prototype.ProcessDataForStorage = function () {
        return this;
    };
    return Content;
})();

var MenuContent = (function (_super) {
    __extends(MenuContent, _super);
    function MenuContent() {
        _super.apply(this, arguments);
    }
    MenuContent.prototype.ProcessDataForStorage = function () {
        this.Html = new MenuBuilder(this).Html;
        return this;
    };
    return MenuContent;
})(Content);

var WebSocketMessage = (function () {
    function WebSocketMessage(Message, Content) {
        this.Message = Message;
        this.Content = Content;
    }
    return WebSocketMessage;
})();
//# sourceMappingURL=models.js.map
