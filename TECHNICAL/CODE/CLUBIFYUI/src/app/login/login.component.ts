import { Component, OnInit, Input } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { Router } from '@angular/router';
import { AbstractControl, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
declare function showSuccessToast(msg: any): any;
declare function showDangerToast(msg: any): any;


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(public sharedService: SharedService,
    private router: Router,
    private service: SharedService, private formBuilder: FormBuilder) {
    service.isAuthenticated = false;
  }

  submitted = false;
  UserList: any = [];
  userID: any;
  userPass: any;
  response: any = {};

  ngOnInit(): void {
    this.form = this.formBuilder.group(
      {
        UserID: [],
        Username: [
          '',
          [Validators.required, Validators.email]],
        Password: [
          '',
          [Validators.required,
          Validators.minLength(5)]],
      }
    );
  }

  replacer(i: any, val: any) {
    if (i === 'U_password') {
      return undefined;
    } else {
      return val;
    }
  }


  // Front Form View 
  form: FormGroup = new FormGroup({
    UserID: new FormControl(''),
    Username: new FormControl(''),
    Password: new FormControl('')
  });
  // From control
  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  login() {
    this.submitted = true;
    if (this.form.invalid) {
      this.scrollToFirstInvalidControl();
      return;
    }
    var val = {
      Email: this.form.value.Username,
      Password: this.form.value.Password,
    };

    this.service.getLogin(val).subscribe((res) => {
      this.response = res;
      if (this.response['status_code'] == 100) {

        this.UserList = JSON.parse(this.response['message'])[0];

        localStorage.setItem(
          'currentUser',
          this.service.encryptData(JSON.stringify(this.UserList, this.replacer))
        );

        this.service.isAuthenticated = true;
        // this.router.navigate(['/Home']);
        window.location.href = '/Home';
      } else {
        this.service.isAuthenticated = false;
        showDangerToast(JSON.parse(this.response['message'])[0]['message']);
      }
    }, (err) => {
      this.service.isAuthenticated = false;
      showDangerToast(this.service.errorMsg);
    });
  }


  private scrollToFirstInvalidControl() {
    let form: any // <-- your formID
    form = document.getElementById('form');
    let firstInvalidControl = form.getElementsByClassName('ng-invalid')[0];
    firstInvalidControl.scrollIntoView();
    (firstInvalidControl as HTMLElement).focus();
  }
}


