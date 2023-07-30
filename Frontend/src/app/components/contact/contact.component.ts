import {Component, ElementRef,  OnInit, ViewChild} from '@angular/core';
import {Customer, Representative} from "../../demo/api/customer";
import {Table} from "primeng/table";
import {Product} from "../../demo/api/product";
import {CustomerService} from "../../demo/service/customer.service";
import {ProductService} from "../../demo/service/product.service";
import {ContactService} from "../../services/contact.service";
import {Contact} from "../../models/Contact/Contact.model";
import {DialogService, DynamicDialogRef} from "primeng/dynamicdialog";
import {EditContactComponent} from "./edit-contact/edit-contact.component";
import {ConfirmationService, ConfirmEventType, MessageService} from "primeng/api";
import {AddContactComponent} from "./add-contact/add-contact.component";

interface expandedRows {
    [key: string]: boolean;
}

enum AccountRoleCode {
    DecisionMaker = 1,
    Employee = 2,
    Influencer = 3,
}

enum Gendercode {
    Male = 1,
    Female = 2
}
enum Statecode {
    Active = 0,
    Inactive = 1
}
@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss'],
  providers: [DialogService,ConfirmationService, MessageService]
})
export class ContactComponent implements OnInit{
    //Mine
    contacts : Contact[] = [];
    AccountRoleCode = AccountRoleCode;
    ref: DynamicDialogRef | undefined;
    Statecode = Statecode;
    Gendercode = Gendercode;
    //Others
    customers1: Customer[] = [];

    customers2: Customer[] = [];

    customers3: Customer[] = [];


    representatives: Representative[] = [];

    statuses: any[] = [];

    products: Product[] = [];

    rowGroupMetadata: any;

    expandedRows: expandedRows = {};

    activityValues: number[] = [0, 100];

    isExpanded: boolean = false;

    idFrozen: boolean = false;

    loading: boolean = true;

    @ViewChild('filter') filter!: ElementRef;

    constructor(
                private customerService: CustomerService,
                private productService: ProductService,
                private contactService : ContactService,
                private dialogService: DialogService,
                private confirmationService: ConfirmationService,
                private messageService: MessageService) { }

    ngOnInit() {

        //this.initializeContacts();
        this.contactService.contactsSubject.subscribe({
            next: value => {
                this.contacts = value;
            },
            error: err => {}
        });
        this.contactService.getContacts();

        this.customerService.getCustomersLarge().then(customers => {
            this.customers1 = customers;
            this.loading = false;

            // @ts-ignore
            this.customers1.forEach(customer => customer.date = new Date(customer.date));
        });
        this.customerService.getCustomersMedium().then(customers => this.customers2 = customers);
        this.customerService.getCustomersLarge().then(customers => this.customers3 = customers);
        this.productService.getProductsWithOrdersSmall().then(data => this.products = data);

        this.representatives = [
            { name: 'Amy Elsner', image: 'amyelsner.png' },
            { name: 'Anna Fali', image: 'annafali.png' },
            { name: 'Asiya Javayant', image: 'asiyajavayant.png' },
            { name: 'Bernardo Dominic', image: 'bernardodominic.png' },
            { name: 'Elwin Sharvill', image: 'elwinsharvill.png' },
            { name: 'Ioni Bowcher', image: 'ionibowcher.png' },
            { name: 'Ivan Magalhaes', image: 'ivanmagalhaes.png' },
            { name: 'Onyama Limba', image: 'onyamalimba.png' },
            { name: 'Stephen Shaw', image: 'stephenshaw.png' },
            { name: 'XuXue Feng', image: 'xuxuefeng.png' }
        ];

        this.statuses = [
            { label: 'ACTIVE', value: Statecode[1] },
            { label: 'INACTIVE', value: Statecode[2] },
        ];
    }


    onSort() {
        this.updateRowGroupMetaData();
    }

    updateRowGroupMetaData() {
        this.rowGroupMetadata = {};

        if (this.customers3) {
            for (let i = 0; i < this.customers3.length; i++) {
                const rowData = this.customers3[i];
                const representativeName = rowData?.representative?.name || '';

                if (i === 0) {
                    this.rowGroupMetadata[representativeName] = { index: 0, size: 1 };
                }
                else {
                    const previousRowData = this.customers3[i - 1];
                    const previousRowGroup = previousRowData?.representative?.name;
                    if (representativeName === previousRowGroup) {
                        this.rowGroupMetadata[representativeName].size++;
                    }
                    else {
                        this.rowGroupMetadata[representativeName] = { index: i, size: 1 };
                    }
                }
            }
        }
    }

    expandAll() {
        if (!this.isExpanded) {
            this.products.forEach(product => product && product.name ? this.expandedRows[product.name] = true : '');

        } else {
            this.expandedRows = {};
        }
        this.isExpanded = !this.isExpanded;
    }

    formatCurrency(value: number) {
        return value.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    clear(table: Table) {
        table.clear();
        this.filter.nativeElement.value = '';
    }

    /*initializeContacts(){
        this.contactService.getContacts().subscribe({
            next : value => {
                this.contacts = value.result as Contact[];
                console.log(value);
            },
            error : err => {
                console.log(err);
            }
        });
    }*/

    getContactStatus(statuscode: any) {
         switch (statuscode) {
             case 'Active':
                return 'qualified';
            case 'Inactive':
                return 'unqualified';
            default:
                 return  'warning';
        }
    }


    UpdateAccount(contact: Contact) {

    }

    DeleteContact(contactId: string) {
       /* this.contactService.DeleteContact(contactId).subscribe({
            next : value => {
                let isSuccess  = value.isSuccess;
                if (isSuccess){
                    this.contacts = this.contacts.filter(c => c.contactId != contactId);
                }
            },
            error : err => {
                console.log(err);
            }
        });*/
        this.contactService.DeleteContact(contactId)
            .then(
                value => {

                }
            )
    }

    confirmDelete(contactId: string) {
        this.confirmationService.confirm({
            message: 'Do you want to delete this record?',
            header: 'Delete Confirmation',
            icon: 'pi pi-info-circle',
            accept: () => {
                this.DeleteContact(contactId);
                this.messageService.add({ severity: 'info', summary: 'Confirmed', detail: 'Record deleted' });
            },
            reject: (type : ConfirmEventType) => {
                switch (type) {
                case ConfirmEventType.REJECT:
                    this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have rejected' });
                    break;
                case ConfirmEventType.CANCEL:
                    this.messageService.add({ severity: 'warn', summary: 'Cancelled', detail: 'You have cancelled' });
                    break;
                }
            }
        });
    }


    UpdateDialog(contact : Contact) {
        this.ref = this.dialogService.open(EditContactComponent, {
            data: contact,
            width : '60%',
            header: 'Update Contact',
            maximizable: true
        });
        this.ref.onClose.subscribe((contactUpdate) => {
            if (contactUpdate) {
                this.messageService.add({ severity: 'success', summary: 'Update Contact' ,detail : 'Contact updated successfully'});
            }else {
                this.messageService.add({ severity: 'warn', summary: 'Cancelled' ,detail : 'You have cancelled'});
            }
        });
    }

    AddDialog() {
        this.ref = this.dialogService.open(AddContactComponent, {
            width : '60%',
            header: 'Add new  contact',
            maximizable: true
        });
        this.ref.onClose.subscribe((contactCreate: any) => {
            if(contactCreate) {
                this.messageService.add({ severity: 'success', summary: 'Add Contact',detail : "Contact added successfully"});
            }else {
                this.messageService.add({ severity: 'warn', summary: 'Cancelled' ,detail : 'You have cancelled'});
            }

        });
    }
}
