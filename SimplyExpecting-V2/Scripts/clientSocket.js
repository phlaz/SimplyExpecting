var ClientSocket = (function () {
    function ClientSocket(url, onInitialize) {
        if (typeof onInitialize === "undefined") { onInitialize = null; }
        var _this = this;
        this._socket = new WebSocket(url);
        this._socket.onerror = function (ev) {
        };

        this._socket.onclose = function (ev) {
        };

        this._socket.onopen = function (ev) {
            if (onInitialize)
                onInitialize(true);
        };

        this._socket.onmessage = function (ev) {
            if (_this.NewMessageReceived)
                _this.NewMessageReceived(JSON.parse(ev.data));
        };
    }
    ClientSocket.prototype.Connect = function () {
    };

    ClientSocket.prototype.SendContent = function (content) {
        this._socket.send(JSON.stringify(new WebSocketMessage("ContentPost", content)));
        this._deferred = $.Deferred();
    };

    ClientSocket.prototype.GetContent = function (content) {
        if (this._socket.readyState == WebSocket.OPEN)
            this._socket.send(JSON.stringify(new WebSocketMessage("ContentRequest", content)));
    };
    return ClientSocket;
})();
//# sourceMappingURL=clientSocket.js.map
