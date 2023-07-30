import { NgModule } from '@angular/core';
import {HashLocationStrategy, JsonPipe, LocationStrategy, NgIf} from '@angular/common';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AppLayoutModule } from './layout/app.layout.module';
import { NotfoundComponent } from './demo/components/notfound/notfound.component';
import { ProductService } from './demo/service/product.service';
import { CountryService } from './demo/service/country.service';
import { CustomerService } from './demo/service/customer.service';
import { EventService } from './demo/service/event.service';
import { IconService } from './demo/service/icon.service';
import { NodeService } from './demo/service/node.service';
import { PhotoService } from './demo/service/photo.service';
import {PrimeNGModule} from "./prime-ng/PrimeNG.module";
import {PasswordModule} from "primeng/password";
import {CheckboxModule} from "primeng/checkbox";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {ContactComponent} from "./components/contact/contact.component";
import {HttpClientModule} from "@angular/common/http";
import {TagModule} from "primeng/tag";
import { EditContactComponent } from './components/contact/edit-contact/edit-contact.component';
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {ToastModule} from "primeng/toast";
import {RouterModule} from "@angular/router";
import { AddContactComponent } from './components/contact/add-contact/add-contact.component';
import {MessageService} from "primeng/api";

@NgModule({
    declarations: [
        AppComponent, NotfoundComponent,ContactComponent, EditContactComponent, AddContactComponent
    ],
    imports: [
        AppRoutingModule,
        AppLayoutModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        HttpClientModule,
        PrimeNGModule,
        PasswordModule,
        CheckboxModule,
        TagModule,
        ToastModule,
        JsonPipe,
        NgIf
    ],
    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        CountryService, CustomerService, EventService, IconService, NodeService,
        PhotoService, ProductService,DynamicDialogConfig,DynamicDialogRef,MessageService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
