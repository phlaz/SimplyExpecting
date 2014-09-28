///<reference path="typings\jquery\jquery.d.ts"/>
///<reference path="models.ts"/>

class ContentEditor {

    constructor(showEditElement: string, hideEditElement: string, SectionId: Sections, public Html: string) {
        var cls = this;
        WinJS.UI.processAll().then(function () {
            
            document.getElementById(showEditElement).addEventListener("click", cls.ShowEditArea, false);
            document.getElementById(hideEditElement).addEventListener("click", cls.HideEditArea, false);
            cls.ShowEditElement = document.getElementById(showEditElement);
        });
    }

    private _deferred: JQueryDeferred<string> = $.Deferred();

    public ShowEditElement: HTMLElement;

    public ShowEditArea(): JQueryDeferred<string> {
        document.getElementById("editFlyout").winControl.show(this.ShowEditElement);
        return this._deferred;
    }

    HideEditArea() {
        this._deferred.resolve(this.Html);
    }
}                                                                         