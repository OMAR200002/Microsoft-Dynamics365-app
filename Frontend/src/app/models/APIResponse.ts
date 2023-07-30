
export class APIResponse {
    public httpStatusCode: any;
    public isSuccess: boolean = true;
    public errorMessages: string[] | null = null;
    public result: any | null = null;

    constructor(
        httpStatusCode: any,
        MyProperty: number,
        IsSuccess: boolean = true,
        ErrorMessages: string[] | null = null,
        Result: any | null = null
    ) {
        this.httpStatusCode = httpStatusCode;
        this.isSuccess = IsSuccess;
        this.errorMessages = ErrorMessages;
        this.result = Result;
    }
}
