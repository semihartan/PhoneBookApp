import { Component, OnInit, inject } from '@angular/core'; 
import { Contact } from '../../models/contact.model';
import { ContactService } from '../../services/contact.service';
import { Router } from '@angular/router';
import { PhoneNumber } from '../../models/phone-number.model';

@Component({
  selector: 'app-contact-list',
  standalone: true, // Marks this as a standalone component 
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css']
})
export class ContactListComponent implements OnInit {
  private router = inject(Router);
  private contactService = inject(ContactService);
  contacts: Contact[] = [];
  isLoading: boolean = true;

  ngOnInit(): void {
    this.loadContacts();
  }

  loadContacts(): void {
    this.isLoading = true;
    this.contactService.getContacts().subscribe({
      next: (data) => {
        this.contacts = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error fetching contacts:', err);
        this.isLoading = false;
        // TODO: Display error to the user
      }
    });
  }
    // Add to ContactListComponent class
  expandedContactId: number | null = null;
toggleActions(contactId: number): void {
    if (this.expandedContactId === contactId) {
      this.expandedContactId = null; // Collapse if already expanded
    } else {
      this.expandedContactId = contactId; // Expand this one
    }
  }

  getPrimaryPhoneNumber(phoneNumbers?: PhoneNumber[]): string | null {
    if (!phoneNumbers || phoneNumbers.length === 0) return null;
    const primary = phoneNumbers.find(p => p.isPrimary);
    return primary ? primary.number : phoneNumbers[0].number;
  }

  getInitials(name: string): string {
    if (!name) return '';
    const parts = name.split(' ');
    if (parts.length === 1) return parts[0].substring(0, 1).toUpperCase();
    return (parts[0].substring(0, 1) + (parts.length > 1 ? parts[parts.length - 1].substring(0, 1) : ''))
      .toUpperCase();
  }

  // --- Action Handlers ---
  callContact(contact: Contact): void {
    const phoneNumber = this.getPrimaryPhoneNumber(contact.phoneNumbers);
    if (phoneNumber) {
      console.log(`Calling ${contact.name} at ${phoneNumber}...`);
      window.location.href = `tel:${phoneNumber}`; // For actual call on mobile
    } else {
      console.warn(`No phone number to call for ${contact.name}`);
      // Potentially show a message to the user
    }
    this.expandedContactId = null; // Collapse after action
  }

  messageContact(contact: Contact): void {
    const phoneNumber = this.getPrimaryPhoneNumber(contact.phoneNumbers);
    if (phoneNumber) {
      console.log(`Messaging ${contact.name} at ${phoneNumber}...`);
      window.location.href = `sms:${phoneNumber}`; // For actual SMS on mobile
    } else {
      console.warn(`No phone number to message for ${contact.name}`);
    }
    this.expandedContactId = null; // Collapse after action
  }

  viewDetails(contact: Contact): void {
    console.log(`Viewing details for ${contact.name} (ID: ${contact.contactID})...`);
    this.router.navigate(['/contacts/edit/', contact.contactID]);
    this.expandedContactId = null;
  }

  confirmDeleteContact(contact: Contact): void {
    // Using a simple browser confirm, consider a more styled modal for better UX
    if (confirm(`Are you sure you want to delete ${contact.name}? This action cannot be undone.`)) {
      this.isLoading = true; // Optional: show a loading state for delete
      this.contactService.deleteContact(contact.contactID).subscribe({
        next: () => {
          console.log(`${contact.name} deleted successfully.`);
          // Remove the contact from the local array to update the UI
          this.contacts = this.contacts.filter(c => c.contactID !== contact.contactID);
          this.expandedContactId = null; // Collapse actions
          this.isLoading = false;
          // Optionally, show a success notification (e.g., a toast message)
        },
        error: (err) => {
          console.error(`Error deleting contact ${contact.name}:`, err);
          this.isLoading = false;
          this.expandedContactId = null; // Still collapse actions
          // Optionally, show an error notification
        }
      });
    } else {
      // User cancelled the deletion, optionally collapse actions
      // this.expandedContactId = null;
    }
  }

  onAddNewContact(): void {
    console.log('Add new contact clicked');
    this.router.navigate(['/contacts/new']);
  }
  // We'll add edit/delete functionality later
}