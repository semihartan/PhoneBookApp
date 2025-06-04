import { Contact } from "./contact.model";
import { EmailType } from "./email-type";

export interface Email {
    emailID: number;
    contactID: number;
    address: string;
    type: EmailType;
    isPrimary: boolean;
    contact: Contact; 
}