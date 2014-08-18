///<references path="typings\jquery\jquery.d.ts" />
var simplyExpecting = (function () {
    function simplyExpecting() {
        $(window).ready(function () {
        });
    }
    simplyExpecting.prototype.loadNavigation = function () {
        $('#navigation').load('..\templates\navigation-template.html', function (responseText, textStatus, httpRequest) {
            var i = 1;
        });
    };
    return simplyExpecting;
})();
//# sourceMappingURL=simplyExpecting.js.map
