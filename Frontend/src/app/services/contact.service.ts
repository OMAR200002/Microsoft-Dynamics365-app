import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {firstValueFrom,  Subject} from "rxjs";
import {APIResponse} from  "../models/APIResponse";
import {Contact} from "../models/Contact/Contact.model";
import {ContactUpdateModel} from "../models/Contact/contactUpdate.model";
import {ContactCreateModel} from "../models/Contact/contactCreate.model";

@Injectable({
  providedIn: 'root'
})
export class ContactService{
    public contactsSubject = new Subject<Array<Contact>>();
    private contactsList!: Array<Contact>;
    private baseUrl = "https://localhost:7133/api/Contact/";

  constructor(private http : HttpClient) {

  }

  getContacts() {
      this.http.get<APIResponse>(`${this.baseUrl}GetContacts`).subscribe({
          next: value => {
              this.contactsList = value.result;
              this.contactsSubject.next(this.contactsList);
              console.log(this.contactsList);
          },
          error: err => {
                console.log(err);
          }
      });
  }
  async UpdateContact(contactUpdate: ContactUpdateModel,contactId : string) {
      let isSuccess = false;
      let errorMessage !: string;
      const contact = this.toContact(contactUpdate,contactId);
      await firstValueFrom(this.http.patch<APIResponse>(`${this.baseUrl}UpdateContact/${contactId}`,contactUpdate))
          .then(
              value => {
                  this.contactsList = this.contactsList.map(c => c.contactId == contactId ? contact : c );
                  this.contactsSubject.next(this.contactsList);
                  isSuccess = !isSuccess;
              },
              )
          .catch(
              reason => {
                  errorMessage = reason.error.errorMessages[0];
              }
          )

      if (isSuccess){
          return Promise.resolve(true);
      }else {
          return Promise.reject(errorMessage);
      }
  }

  DeleteContact(contactId : string){
      let isSuccess = false;
      firstValueFrom(this.http.delete<APIResponse>(`${this.baseUrl}DeleteContact/${contactId}`))
          .then(
              value => {
                  console.log(value);
                  this.contactsList = this.contactsList.filter(c => c.contactId != contactId);
                  this.contactsSubject.next(this.contactsList);
                  isSuccess = !isSuccess;
              }
          )
          .catch(
              reason => {
                  console.log(reason);
              }
          );
      if (isSuccess){
          return Promise.resolve(true);
      }
      else {
          return Promise.resolve("Error while deleting Contact");
      }
  }
  async CreateContact(contactCreate : ContactCreateModel) {
      let isSuccess = false;
      let errorMessage !: string;
      await firstValueFrom(this.http.post<APIResponse>(`${this.baseUrl}AddContact`,contactCreate))
           .then(
               value =>  {
                  this.contactsList = [value.result,...this.contactsList];
                  this.contactsSubject.next(this.contactsList);
                  isSuccess = !isSuccess;
               }
           ).catch(
               reason => {
                   errorMessage = reason.error.errorMessages[0];
               }
          );
      if (isSuccess){
          return Promise.resolve(true);
      }
      else {
          return Promise.reject(errorMessage);
      }
  }

  //Mapping
  toContact(contactUpdate : ContactUpdateModel,contactId : string) : Contact{
      const contact = new Contact(
          contactId,
          `${contactUpdate.firstname} ${contactUpdate.lastname}`,
          contactUpdate.firstname,
          contactUpdate.lastname,
          contactUpdate.emailaddress1,
          contactUpdate.jobtitle,
          contactUpdate.gendercode,
          contactUpdate.statecode,
          contactUpdate.accountrolecode,
      );
      return contact;
  }
  toContactUpdate(value : any) : ContactUpdateModel {
      const contactUpdate = new ContactUpdateModel(
          value.firstname,
          value.lastname,
          value.emailaddress1,
          value.jobtitle,
          value.gendercode,
          value.statecode,
          value.accountrolecode);

      return  contactUpdate;
  }

  toContactCreate(value : any) : ContactCreateModel {
      const contactCreate = new ContactCreateModel(
          value.firstname,
          value.lastname,
          value.emailaddress1,
          value.jobtitle,
          value.gendercode,
          value.accountrolecode);

      return  contactCreate;
  }

}
