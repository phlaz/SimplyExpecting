///<references path="typings\jquery\jquery.d.ts" />

class simplyExpecting {
    constructor() { 
        $(window).ready(() =>
        { }
            //this.loadNavigation()
            );
   }
        
    public loadNavigation() {
        $('#navigation').load('..\templates\navigation-template.html', (responseText, textStatus, httpRequest) => { 
            var i = 1;
        });
    }  
} 