import {Component, OnInit} from '@angular/core';
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Contact} from "../../../models/Contact/Contact.model";
import {ContactService} from "../../../services/contact.service";
import {Router} from "@angular/router";
import {MessageService} from "primeng/api";




@Component({
  selector: 'app-edit-contact',
  templateUrl: './edit-contact.component.html',
  styleUrls: ['./edit-contact.component.scss'],
  providers : [
  ]
})
export class EditContactComponent implements OnInit{
    Genders: any;
    Statuses : any;
    AccountRoles : any;
    editForm!: FormGroup;
    contactUpdate!: Contact;
    isLoading : boolean = false;
    constructor(public dynamicDialogConfig  : DynamicDialogConfig,
                public dynamicDialogRef   : DynamicDialogRef,
                private formBuilder: FormBuilder,
                private contactService : ContactService,
                private messageService: MessageService
                ) {
    }

    ngOnInit() {
        this.contactUpdate = this.dynamicDialogConfig.data;
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

        this.editForm = this.formBuilder.group({
            firstname: new FormControl(this.contactUpdate.firstname,Validators.required),
            lastname : new FormControl(this.contactUpdate.lastname,Validators.required),
            emailaddress1 : new FormControl(this.contactUpdate.emailaddress1,[Validators.required,Validators.email]),
            jobtitle : new FormControl(this.contactUpdate.jobtitle),
            gendercode : new FormControl(this.contactUpdate.gendercode,Validators.required),
            statecode : new FormControl(this.contactUpdate.statecode),
            accountrolecode : new FormControl(this.contactUpdate.accountrolecode),
        });

    }

    UpdateContact() {
        this.isLoading = !this.isLoading;
        //Get codes  of gendercode,statecode,accountrolecode
        let gendercode = this.editForm.value['gendercode'];
        let statecode = this.editForm.value['statecode'];
        let accountrolecode = this.editForm.value['accountrolecode'];


        this.editForm.value['gendercode'] = gendercode == undefined ?  this.contactUpdate.gendercode : gendercode.code == undefined ? gendercode : gendercode.code;
        this.editForm.value['statecode'] = statecode == undefined ?   this.contactUpdate.statecode : statecode.code == undefined ? statecode : statecode.code;
        this.editForm.value['accountrolecode'] = accountrolecode == undefined ?  this.contactUpdate.accountrolecode : accountrolecode.code == -1 ? null : accountrolecode.code;
        //Map form values to contactUpdate model
        const contactUpdate = this.contactService.toContactUpdate(this.editForm.value);
        this.contactService.UpdateContact(contactUpdate,this.contactUpdate.contactId)
            .then(
                value => {
                    this.isLoading = !this.isLoading;
                    this.dynamicDialogRef.close(contactUpdate);
                }
            ).catch(
                reason => {
                    this.isLoading = !this.isLoading;
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: reason });
                }
        );

    }


    getGender(code : any){
        let Gender;
        switch (code){
            case 1:
                Gender =  'Male';
                break;
            case 2:
                Gender =  'Female';
                break;
            default:
                Gender = 'Not Defined';
        }

        return Gender;
    }
    getState(code : any){
        let State;
        switch (code){
            case 0:
                State =  'ACTIVE';
                break;
            case 1:
                State =  'INACTIVE';
                break;
            default:
                State = 'Not Defined';
        }
        return State;
    }
    getAccountRoles(code : any){
        let accountRole;
        switch (code){
            case 1:
                accountRole =  'DecisionMaker';
                break;
            case 2:
                accountRole =  'Employee';
                break;
            case 3:
                accountRole =  'Influencer';
                break;
            default:
                accountRole = 'Not Defined';
        }
        return accountRole;
    }

    CancelUpdate() {
        this.dynamicDialogRef.close();
    }
}


