import { EmailCreateDto } from "./email-create.dto";
import { PhoneNumberCreateDto } from "./phone-number-create.dto";

export interface ContactCreateDto {
  name: string;
  notes: string | null; 
  phoneNumbers?: PhoneNumberCreateDto[];
  emails?: EmailCreateDto[];
}