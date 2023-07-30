
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

export class ContactCreateModel {
    public firstname: string = "";
    public lastname: string = "";
    public emailaddress1: string = "";
    public jobtitle: string = "";
    public gendercode: Gendercode | null = null;
    public accountrolecode: AccountRoleCode | null = null;

    constructor(
        firstname: string = "",
        lastname: string = "",
        emailaddress1: string = "",
        jobtitle: string = "",
        gendercode: Gendercode | null = null,
        accountrolecode: AccountRoleCode | null = null
    ) {
        this.firstname = firstname;
        this.lastname = lastname;
        this.emailaddress1 = emailaddress1;
        this.jobtitle = jobtitle;
        this.gendercode = gendercode;
        this.accountrolecode = accountrolecode;
    }
}
