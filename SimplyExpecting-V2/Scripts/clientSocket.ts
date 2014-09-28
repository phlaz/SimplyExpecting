
class ClientSocket  {
    constructor (url: string, onInitialize: (connected: boolean) => void = null) {
        this._socket = new WebSocket(url);
        var me = this;
        this._socket.onerror = ev => {
            if (onInitialize) onInitialize(false);
        };

        this._socket.onclose = ev => {
        };

        this._socket.onopen = ev => {
            if (onInitialize) onInitialize(true);
        };

        this._socket.onmessage = ev => {
            if (this.NewMessageReceived)
                this.NewMessageReceived(JSON.parse(ev.data));
        };
    }

    private _socket: WebSocket;


    public NewMessageReceived: (content: IContent) => void;

    public Connect() {
        
    } 

    public SendContent(html: string): void {
        this._socket.send(html);
    }

    public GetContent(contentId: number, version: number) {
        if(this._socket.readyState == WebSocket.OPEN)
            this._socket.send(contentId + ":" + version);
    }


}