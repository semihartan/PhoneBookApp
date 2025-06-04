import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contact } from '../models/contact.model';
import { ContactCreateDto } from '../dtos/contact-create.dto';
import { ContactUpdateDto } from '../dtos/contact-update.dto';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private apiUrl = 'https://localhost:7272/api/Contacts';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(this.apiUrl);
  }

  getContact(id: number): Observable<Contact> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Contact>(url);
  }

  addContact(contact: ContactCreateDto): Observable<Contact> {
    return this.http.post<Contact>(this.apiUrl, contact, this.httpOptions);
  }

  updateContact(contact: ContactUpdateDto): Observable<any> {
    const url = `${this.apiUrl}/${contact.contactID}`;
    return this.http.put(url, contact, this.httpOptions);
  }

  deleteContact(id: number): Observable<any> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete(url, this.httpOptions);
  }
}