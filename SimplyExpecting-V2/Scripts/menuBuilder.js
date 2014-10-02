var MenuItem = (function () {
    function MenuItem(Id, ContentId, Caption, Title, Children) {
        this.Id = Id;
        this.ContentId = ContentId;
        this.Caption = Caption;
        this.Title = Title;
        this.Children = Children;
    }
    return MenuItem;
})();

var MenuBuilder = (function () {
    function MenuBuilder(menuContent) {
        var _this = this;
        var menuItems = JSON.parse(menuContent.Html);
        menuItems.forEach(function (i) {
            return _this.BuildMenu(i);
        });
        this.Html += "</ul>";
    }
    MenuBuilder.prototype.BuildMenu = function (item) {
        var _this = this;
        //if there is no htlm currently add a ul root element
        if (this.Html == null)
            this.Html = "<ul>";

        this.Html += "<li><a href='#' title='" + item.Title + "' ";

        //only add a click event if there are no MenuItem children
        if (item.Children.length == 0)
            this.Html += "onclick=\"app.GetContent(" + item.ContentId + ")\"";

        //close the <li> and add a <a> tag
        this.Html += ">" + item.Caption + "</a>";

        //process all the children
        if (item.Children.length > 0) {
            this.Html += "<ul>";
            item.Children.forEach(function (c, i, items) {
                return _this.BuildMenu(c);
            });
            this.Html += "</ul>";
        }

        //close the <li> tag
        this.Html += "</li>";
    };
    return MenuBuilder;
})();
//# sourceMappingURL=menuBuilder.js.map
