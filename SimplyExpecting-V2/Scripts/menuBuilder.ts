class MenuItem {
    constructor(public Id: number, public ContentId: number, public Caption: string, public Title: string, public Children: Array<MenuItem>) {
    }
}


class MenuBuilder {
    constructor(menuContent: MenuContent) {
        var menuItems = JSON.parse(menuContent.Html);
        menuItems.forEach(i => this.BuildMenu(i));
        this.Html += "</ul>";
    }

    public Html: string;

    private BuildMenu(item: MenuItem) {
        //if there is no htlm currently add a ul root element
        if (this.Html == null) this.Html = "<ul>";

        this.Html += "<li><a href='#' title='" + item.Title + "' ";

        //only add a click event if there are no MenuItem children
        if (item.Children.length == 0)
            this.Html += "onclick=\"app.GetContent(this, " + item.ContentId + ")\"";

        //close the <li> and add a <a> tag
        this.Html += ">" + item.Caption + "</a>";

        //process all the children
        if (item.Children.length > 0) {
            this.Html += "<ul>";
            item.Children.forEach((c, i, items) => this.BuildMenu(c));
            this.Html += "</ul>";
        }

        //close the <li> tag
        this.Html += "</li>";
    }
} 