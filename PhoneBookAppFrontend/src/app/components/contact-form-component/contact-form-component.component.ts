import { Component, OnInit, inject } from '@angular/core'; 
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ContactService } from '../../services/contact.service';
import { ContactCreateDto } from '../../dtos/contact-create.dto'; // Assuming ContactCreateDto for new contacts
import { ContactUpdateDto } from '../../dtos/contact-update.dto'; // Assuming ContactCreateDto for new contacts
import { PhoneNumberCreateDto } from '../../dtos/phone-number-create.dto'; // Assuming PhoneNumberCreateDto
import { EmailCreateDto } from '../../dtos/email-create.dto';
@Component({
  selector: 'app-new-contact-form-component',
  imports: [ReactiveFormsModule ],
  templateUrl: './contact-form-component.component.html',
  styleUrl: './contact-form-component.component.css'
})
export class ContactFormComponentComponent {
 private fb = inject(FormBuilder);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private contactService = inject(ContactService);

  contactForm: FormGroup;
  isEditMode = false;
  contactIdToEdit: number | null = null; // Renamed for clarity
  isLoading = false;
  pageTitle = 'Add New Contact';

  constructor() {
    this.contactForm = this.fb.group({
      name: ['', Validators.required],
      notes: [''],
      phoneNumbers: this.fb.array([]),
      emails: this.fb.array([]) // Add FormArray for emails
    });
  }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.contactIdToEdit = Number(idParam);
      this.isEditMode = true;
      this.pageTitle = 'Edit Contact';
      this.loadContactForEdit(this.contactIdToEdit);
    } else {
      // Add one empty phone number and email field by default for new contacts
      this.addPhoneNumber();
      this.addEmail();
    }
  }

  loadContactForEdit(id: number): void {
    this.isLoading = true;
    this.contactService.getContact(id).subscribe({
      next: (contact) => {
        this.contactForm.patchValue({
          name: contact.name,
          notes: contact.notes,
        });
        // Populate PhoneNumbers FormArray
        contact.phoneNumbers?.forEach(pn => {
          this.phoneNumbers.push(this.fb.group({
            phoneNumberID: [pn.phoneNumberID], // Store ID for potential advanced updates
            number: [pn.number, Validators.required],
            type: [pn.type, Validators.required],
            isPrimary: [pn.isPrimary]
          }));
        });
        // Populate Emails FormArray
        contact.emails?.forEach(em => {
          this.emails.push(this.fb.group({
            emailID: [em.emailID], // Store ID for potential advanced updates
            address: [em.address, [Validators.required, Validators.email]],
            type: [em.type, Validators.required],
            isPrimary: [em.isPrimary]
          }));
        });
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading contact for edit:', err);
        this.isLoading = false;
        this.router.navigate(['/contacts']); // Navigate away if contact not found
      }
    });
  }

  // --- Phone Numbers FormArray ---
  get phoneNumbers(): FormArray {
    return this.contactForm.get('phoneNumbers') as FormArray;
  }
  newPhoneNumber(): FormGroup {
    return this.fb.group({
      phoneNumberID: [null], // For edit mode
      number: ['', Validators.required],
      type: ['Mobile', Validators.required],
      isPrimary: [this.phoneNumbers.length === 0] // First one is primary by default
    });
  }
  addPhoneNumber(): void {
    this.phoneNumbers.push(this.newPhoneNumber());
    if (this.phoneNumbers.length > 1 && this.phoneNumbers.controls.filter(c => c.get('isPrimary')?.value).length === 0) {
        this.phoneNumbers.at(0)?.get('isPrimary')?.setValue(true); // Ensure at least one primary if multiple exist
    }
  }
  removePhoneNumber(index: number): void {
    this.phoneNumbers.removeAt(index);
    if (this.phoneNumbers.length > 0 && !this.phoneNumbers.controls.some(control => control.get('isPrimary')?.value)) {
        this.phoneNumbers.at(0)?.get('isPrimary')?.setValue(true);
    }
  }
  onPrimaryPhoneChange(selectedIndex: number): void {
    this.phoneNumbers.controls.forEach((control, index) => {
      if (index !== selectedIndex) control.get('isPrimary')?.setValue(false);
    });
    if (!this.phoneNumbers.at(selectedIndex)?.get('isPrimary')?.value &&
        !this.phoneNumbers.controls.some((_, i) => i !== selectedIndex && this.phoneNumbers.at(i)?.get('isPrimary')?.value)) {
        this.phoneNumbers.at(selectedIndex)?.get('isPrimary')?.setValue(true);
    }
  }

  // --- Emails FormArray ---
  get emails(): FormArray {
    return this.contactForm.get('emails') as FormArray;
  }
  newEmail(): FormGroup {
    return this.fb.group({
      emailID: [null], // For edit mode
      address: ['', [Validators.required, Validators.email]],
      type: ['Personal', Validators.required],
      isPrimary: [this.emails.length === 0] // First one is primary by default
    });
  }
  addEmail(): void {
    this.emails.push(this.newEmail());
    if (this.emails.length > 1 && this.emails.controls.filter(c => c.get('isPrimary')?.value).length === 0) {
        this.emails.at(0)?.get('isPrimary')?.setValue(true);
    }
  }
  removeEmail(index: number): void {
    this.emails.removeAt(index);
     if (this.emails.length > 0 && !this.emails.controls.some(control => control.get('isPrimary')?.value)) {
        this.emails.at(0)?.get('isPrimary')?.setValue(true);
    }
  }
  onPrimaryEmailChange(selectedIndex: number): void {
    this.emails.controls.forEach((control, index) => {
      if (index !== selectedIndex) control.get('isPrimary')?.setValue(false);
    });
     if (!this.emails.at(selectedIndex)?.get('isPrimary')?.value &&
        !this.emails.controls.some((_, i) => i !== selectedIndex && this.emails.at(i)?.get('isPrimary')?.value)) {
        this.emails.at(selectedIndex)?.get('isPrimary')?.setValue(true);
    }
  }

  onSubmit(): void {
    if (this.contactForm.invalid) {
      this.contactForm.markAllAsTouched();
      return;
    }
    this.isLoading = true;
    const formValue = this.contactForm.value;

    const phoneNumbersDto: PhoneNumberCreateDto[] = formValue.phoneNumbers.map((pn: any) => ({
        number: pn.number, type: pn.type, isPrimary: pn.isPrimary
    }));
    const emailsDto: EmailCreateDto[] = formValue.emails.map((em: any) => ({
        address: em.address, type: em.type, isPrimary: em.isPrimary
    }));

    if (this.isEditMode && this.contactIdToEdit) {
      const updatedContactData: ContactUpdateDto = {
        contactID: this.contactIdToEdit,
        name: formValue.name,
        notes: formValue.notes,
        phoneNumbers: phoneNumbersDto,
        emails: emailsDto
      };
      this.contactService.updateContact( updatedContactData).subscribe({

        next: () => {
          console.log('Contact updated successfully');
          this.isLoading = false;
          this.router.navigate(['/contacts']);
        },
        error: (err) => {
          console.error('Error updating contact:', err);
          this.isLoading = false;
        }
      });
    } else {
      const newContactData: ContactCreateDto = {
        name: formValue.name,
        notes: formValue.notes,
        phoneNumbers: phoneNumbersDto,
        emails: emailsDto
      };
      this.contactService.addContact(newContactData).subscribe({
        next: (savedContact) => {
          console.log('Contact saved successfully:', savedContact);
          this.isLoading = false;
          this.router.navigate(['/contacts']);
        },
        error: (err) => {
          console.error('Error saving contact:', err);
          this.isLoading = false;
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/contacts']);
  }
}
