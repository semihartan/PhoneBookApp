import { Routes } from '@angular/router';
import { ContactListComponent } from './components/contact-list/contact-list.component';
import { ContactFormComponentComponent } from './components/contact-form-component/contact-form-component.component';

export const routes: Routes = [
  {
    path: 'contacts',
    component: ContactListComponent
  },
  { path: 'contacts/new', component: ContactFormComponentComponent }, // Route for adding
  { path: 'contacts/edit/:id', component: ContactFormComponentComponent }, // Route for editing (for later)
  {
    path: '',
    redirectTo: '/contacts',
    pathMatch: 'full'
  },
];