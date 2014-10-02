///<reference path="typings\jquery\jquery.d.ts"/>
///<reference path="models.ts"/>
///<reference path="typings\winjs\winjs-2.1.d.ts"/>
var ContentEditor = (function () {
    function ContentEditor(ShowEditorElement, hideEditorElement) {
        this.ShowEditorElement = ShowEditorElement;
        this.hideEditorElement = hideEditorElement;
        this._deferred = $.Deferred();
        var cls = this;
        this.EditorFlyout = document.getElementById("editFlyout");

        if (this.EditorFlyout == undefined)
            throw "Add an element called editFlyout to house the editor.";
    }
    ContentEditor.prototype.Edit = function () {
        this.EditorFlyout.winControl.show(this.ShowEditorElement);
        return this._deferred;
    };

    ContentEditor.prototype.HideEditArea = function () {
        this.EditorFlyout.winControl.hide();
        this._deferred.resolve(this.Html);
    };
    return ContentEditor;
})();
//# sourceMappingURL=contentEditor.js.map
