import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";

import {AppLayoutComponent} from "./layout/app.layout.component";
import {ContactComponent} from "./components/contact/contact.component";

const routes : Routes = [
    {
        path : '', component : AppLayoutComponent, children : [
            {path : 'contacts',component : ContactComponent}
        ]
    },

];
@NgModule({
    imports : [
        RouterModule.forRoot(routes)
    ],
    exports : [
        RouterModule
    ]

})
export  class AppRoutingModule{}
