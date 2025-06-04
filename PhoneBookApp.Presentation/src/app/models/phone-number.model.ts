import { Contact } from "./contact.model";
import { PhoneNumberType } from "./phone-number-type";

export interface PhoneNumber {
    phoneNumberID: number;
    contactID?: number;
    number: string;
    type: PhoneNumberType;
    isPrimary: boolean;
    contact?: Contact;
}