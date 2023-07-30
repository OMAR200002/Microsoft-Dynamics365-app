import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Contact} from "../../../models/Contact/Contact.model";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {ContactService} from "../../../services/contact.service";
import {MessageService} from "primeng/api";

@Component({
  selector: 'app-add-contact',
  templateUrl: './add-contact.component.html',
  styleUrls: ['./add-contact.component.scss']
})
export class AddContactComponent implements OnInit{
    Genders: any;
    Statuses : any;
    AccountRoles : any;
    createForm!: FormGroup;
    contactUpdate!: Contact;
    isLoading : boolean  = false;
    constructor(public dynamicDialogConfig  : DynamicDialogConfig,
                public dynamicDialogRef   : DynamicDialogRef,
                private formBuilder: FormBuilder,
                private contactService : ContactService,
                private messageService: MessageService
    ) {
    }

    ngOnInit() {
        this.Genders = [
            { name: 'Male', code: 1},
            { name: 'Female', code: 2},
        ];
        this.Statuses = [
            { name: 'Active', code: 0},
            { name: 'Inactive', code: 1},
        ];
        this.AccountRoles = [
            {name : 'Not Defined',code : -1},
            { name: 'DecisionMaker', code: 1},
            { name: 'Employee', code: 2},
            { name: 'Influencer', code: 3},
        ];

        this.createForm = this.formBuilder.group({
            firstname: new FormControl('',Validators.required),
            lastname : new FormControl('',Validators.required),
            emailaddress1 : new FormControl('',[Validators.required,Validators.email]),
            jobtitle : new FormControl(null),
            gendercode : new FormControl(1,Validators.required),
            accountrolecode : new FormControl(null),
        });
    }

    AddNewContact() {
        this.isLoading = !this.isLoading;
        console.log(this.createForm.value);
        //Get codes  of gendercode,statecode,accountrolecode
        let gendercode = this.createForm.value['gendercode'];
        this.createForm.value['gendercode'] = gendercode.code == undefined ? gendercode : gendercode.code
        const contactCreate = this.contactService.toContactCreate(this.createForm.value);
        this.contactService.CreateContact(contactCreate)
            .then(
                value => {
                    this.dynamicDialogRef.close(contactCreate);
                }
            )
            .catch(
                reason => {
                        this.isLoading = !this.isLoading;
                        this.messageService.add({ severity: 'error', summary: 'Error', detail: reason });
                }
            )
    }

    CancelCreate() {
        this.dynamicDialogRef.close();
    }
}
