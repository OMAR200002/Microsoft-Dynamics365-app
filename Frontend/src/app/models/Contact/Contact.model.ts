export enum AccountRoleCode {
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

export class Contact {
    public contactId: string;
    public fullname: string = "";
    public firstname: string = "";
    public lastname: string = "";
    public emailaddress1: string = "";
    public jobtitle: string = "";
    public gendercode: Gendercode | null = null;
    public statecode: Statecode | null = 0;
    public accountrolecode: AccountRoleCode | null = null;



    constructor(
        contactId: string,
        fullname: string = "",
        firstname: string = "",
        lastname: string = "",
        emailaddress1: string = "",
        jobtitle: string = "",
        gendercode: Gendercode | null = null,
        statecode: Statecode | null = 0,
        accountrolecode: AccountRoleCode | null = null
    ) {
        this.contactId = contactId;
        this.fullname = fullname;
        this.firstname = firstname;
        this.lastname = lastname;
        this.emailaddress1 = emailaddress1;
        this.jobtitle = jobtitle;
        this.gendercode = gendercode;
        this.statecode = statecode;
        this.accountrolecode = accountrolecode;
    }
}
