import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { AbstractControl, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';
import { Registration } from '../../model/Registration'
import { DatePipe } from '@angular/common'

declare function showModal(id: any): any;
declare function closePopup(id: any): any;
declare function showSuccessToast(msg: any): any;
declare function showDangerToast(msg: any): any;
declare function showInfoToast(msg: any): any;
declare function showWarningToast(msg: any): any;
declare function showTab(id: any): any;

@Component({
  selector: 'app-membership-registration',
  templateUrl: './membership-registration.component.html',
  styleUrls: ['./membership-registration.component.css']
})
export class MembershipRegistrationComponent implements OnInit {

  constructor(private service: SharedService,
    private formBuilder: FormBuilder,
    private spinner: NgxSpinnerService,
    private router: Router) { }

  form: FormGroup = new FormGroup({
    first_name: new FormControl(''),
    last_name: new FormControl(''),
    email: new FormControl(''),
    date_of_birth: new FormControl(null),
    password: new FormControl('')
  });
  submitted = false;
  isRegister = false;
  ngOnInit(): void {
    this.form = this.formBuilder.group({
      first_name: ['', Validators.required],
      last_name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      date_of_birth: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  Register() {
    //Add branch code
    this.submitted = true;
    if (this.form.invalid) {
      this.scrollToFirstInvalidControl();
      return;
    }
    this.spinner.show();
    var val = {
      FirstName: this.form.value.first_name,
      LastName: this.form.value.last_name,
      Email: this.form.value.email,
      DateOfBirth: this.form.value.date_of_birth,
      password: this.form.value.password
    }

    this.service.registerMemeber(val).subscribe(data => {
      console.log(data);
      if (data["status_code"] == 100) {
        // this.branchId = JSON.parse(data["message"])[0]["identity"];
        this.spinner.hide();
        this.onReset();
        this.isRegister = true;
        showSuccessToast("Registration was successful. Please wait for admin approval.");
      }
      else if (data["status_code"] == 300) {
        this.spinner.hide();
        showDangerToast(JSON.parse(data["message"])[0]["message"]);
      }
      else if (data["status_code"] == 200) {
        this.spinner.hide();
        showDangerToast(JSON.parse(data["message"])[0]["message"]);
      }
      else {
        this.spinner.hide();
        showDangerToast(this.service.errorMsg);
      }
    }, err => {
      this.spinner.hide();
      showDangerToast(this.service.errorMsg);
    });

  }

  private scrollToFirstInvalidControl() {
    let form: any
    form = document.getElementById('form');
    let firstInvalidControl = form.getElementsByClassName('ng-invalid')[0];
    firstInvalidControl.scrollIntoView();
    (firstInvalidControl as HTMLElement).focus();
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }

}
