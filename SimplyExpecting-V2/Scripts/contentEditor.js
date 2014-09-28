///<reference path="typings\jquery\jquery.d.ts"/>
///<reference path="models.ts"/>
var ContentEditor = (function () {
    function ContentEditor(showEditElement, hideEditElement, SectionId, Html) {
        this.Html = Html;
        this._deferred = $.Deferred();
        var cls = this;
        WinJS.UI.processAll().then(function () {
            document.getElementById(showEditElement).addEventListener("click", cls.ShowEditArea, false);
            document.getElementById(hideEditElement).addEventListener("click", cls.HideEditArea, false);
            cls.ShowEditElement = document.getElementById(showEditElement);
        });
    }
    ContentEditor.prototype.ShowEditArea = function () {
        document.getElementById("editFlyout").winControl.show(this.ShowEditElement);
        return this._deferred;
    };

    ContentEditor.prototype.HideEditArea = function () {
        this._deferred.resolve(this.Html);
    };
    return ContentEditor;
})();
//# sourceMappingURL=contentEditor.js.map
