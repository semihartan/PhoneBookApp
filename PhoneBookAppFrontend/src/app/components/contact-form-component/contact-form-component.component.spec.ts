import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactFormComponentComponent } from './contact-form-component.component';

describe('NewContactFormComponentComponent', () => {
  let component: ContactFormComponentComponent;
  let fixture: ComponentFixture<ContactFormComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContactFormComponentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ContactFormComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
