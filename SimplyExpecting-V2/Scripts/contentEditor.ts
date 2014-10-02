///<reference path="typings\jquery\jquery.d.ts"/>
///<reference path="models.ts"/>
///<reference path="typings\winjs\winjs-2.1.d.ts"/>

class ContentEditor {

    constructor(private ShowEditorElement: HTMLElement, private hideEditorElement : HTMLElement) {
        var cls = this;     
        this.EditorFlyout = document.getElementById("editFlyout");   
        
        if (this.EditorFlyout == undefined) 
            throw "Add an element called editFlyout to house the editor.";
        
        
    }

    private _deferred: JQueryDeferred<string> = $.Deferred();

    public Html: string;

    public EditorFlyout: HTMLElement;

    public Edit(): JQueryDeferred<string> {
        this.EditorFlyout.winControl.show(this.ShowEditorElement);
        return this._deferred;
    }

    public HideEditArea() {
        this.EditorFlyout.winControl.hide();
        this._deferred.resolve(this.Html);
    }
}                                                                         