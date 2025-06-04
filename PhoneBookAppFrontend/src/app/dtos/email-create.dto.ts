import { EmailType } from  "../models/email-type";

export interface EmailCreateDto {
    address: string;
    type: EmailType;
    isPrimary: boolean | true;
}