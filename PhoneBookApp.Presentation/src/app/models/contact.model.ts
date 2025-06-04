import { Email } from "./email.model";
import { PhoneNumber } from "./phone-number.model";

export interface Contact {
  contactID: number;
  name: string;
  notes: string | null;
  createdAt: Date;
  phoneNumbers?: PhoneNumber[]; // We'll add these later
  emails?: Email[];           // when designing detail view
}