import { NgModule } from '@angular/core';
import {TableModule} from "primeng/table";
import {ButtonModule} from "primeng/button";
import {InputTextModule} from "primeng/inputtext";
import {PasswordModule} from "primeng/password";
import {CheckboxModule} from "primeng/checkbox";
import {CurrencyPipe, DatePipe, NgClass} from "@angular/common";
import {MultiSelectModule} from "primeng/multiselect";
import {SliderModule} from "primeng/slider";
import {FileUploadModule} from "primeng/fileupload";
import {DropdownModule} from "primeng/dropdown";
import {DynamicDialogModule} from "primeng/dynamicdialog";
import {ConfirmDialogModule} from "primeng/confirmdialog";



@NgModule({
    exports: [
        ButtonModule,
        TableModule,
        ButtonModule,
        InputTextModule,
        PasswordModule,
        CheckboxModule,
        DatePipe,
        CurrencyPipe,
        MultiSelectModule,
        SliderModule,
        FileUploadModule,
        NgClass,
        DropdownModule,
        DynamicDialogModule,
        ConfirmDialogModule,
        DropdownModule,

    ],
    imports: [
        ButtonModule,
        TableModule,
        ButtonModule,
        InputTextModule,
        PasswordModule,
        CheckboxModule,
        DatePipe,
        CurrencyPipe,
        MultiSelectModule,
        SliderModule,
        FileUploadModule,
        NgClass,
        DropdownModule,
        DynamicDialogModule,
        ConfirmDialogModule,
        DropdownModule,

    ]
})
export class PrimeNGModule { }
