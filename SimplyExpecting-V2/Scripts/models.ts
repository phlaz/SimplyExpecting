enum Sections { Menu = 1, Home = 2 }

interface IContent {
    Id: number;
    Version: number;
    SectionId: number;
    Html: string;
    ProcessDataForStorage(): any;
}

class Content implements IContent {
    constructor(public Id: number, public Version: number, public SectionId: number, public Html: string) {
    }

    ProcessDataForStorage(): any {
        return this;
    }
}

class MenuContent extends Content{
    ProcessDataForStorage(): any {
        this.Html = new MenuBuilder(this).Html;
        return this;
    }
} 

class WebSocketMessage {
    constructor(public Message: string, public Content: Content) { }
}