import { PhoneNumberType } from "../models/phone-number-type";

export interface PhoneNumberCreateDto {
    number: string;
    type: PhoneNumberType;
    isPrimary: boolean | true;
};
