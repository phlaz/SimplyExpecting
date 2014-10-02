
class ClientSocket  {
    constructor (url: string, onInitialize: (connected: boolean) => void = null) {
        this._socket = new WebSocket(url);
        this._socket.onerror = ev => {
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

    private _deferred: JQueryDeferred<string>;

    private _socket: WebSocket;


    public NewMessageReceived: (content: any) => void;

    public Connect() {
        
    } 

    public SendContent(content: IContent): void {
        this._socket.send(JSON.stringify(new WebSocketMessage("ContentPost", content)));
        this._deferred = $.Deferred();
    }

    public GetContent(content : IContent) {
        if (this._socket.readyState == WebSocket.OPEN)
            this._socket.send(JSON.stringify(new WebSocketMessage("ContentRequest", content)));
    }


}