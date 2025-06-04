import { EmailCreateDto } from "./email-create.dto";
import { PhoneNumberCreateDto } from "./phone-number-create.dto";

export interface ContactUpdateDto {
    contactID: number;
    name: string;
    notes?: string | null;
    phoneNumbers?: (PhoneNumberCreateDto & { phoneNumberID?: number })[];  
    emails?: (EmailCreateDto & { emailID?: number })[]; 
}