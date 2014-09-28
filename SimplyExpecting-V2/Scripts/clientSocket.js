var ClientSocket = (function () {
    function ClientSocket(url, onInitialize) {
        if (typeof onInitialize === "undefined") { onInitialize = null; }
        var _this = this;
        this._socket = new WebSocket(url);
        var me = this;
        this._socket.onerror = function (ev) {
            if (onInitialize)
                onInitialize(false);
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

    ClientSocket.prototype.SendContent = function (html) {
        this._socket.send(html);
    };

    ClientSocket.prototype.GetContent = function (contentId, version) {
        if (this._socket.readyState == WebSocket.OPEN)
            this._socket.send(contentId + ":" + version);
    };
    return ClientSocket;
})();
//# sourceMappingURL=clientSocket.js.map
