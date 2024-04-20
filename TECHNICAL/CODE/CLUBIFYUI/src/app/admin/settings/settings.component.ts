import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { AbstractControl, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { NgxSpinnerService } from "ngx-spinner";
import { catchError, map, of } from 'rxjs';
import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Router } from '@angular/router';


declare function showModal(id: any): any;
declare function closePopup(id: any): any;
declare function showSuccessToast(msg: any): any;
declare function showDangerToast(msg: any): any;
declare function showInfoToast(msg: any): any;
declare function showWarningToast(msg: any): any;


@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  constructor(private service: SharedService, private router: Router, private formBuilder: FormBuilder, private spinner: NgxSpinnerService) { }

  @ViewChild('FileInput')
  FileInput!: ElementRef;
  logoPath: any = this.service.MediaUrl + "Logo/";
  logoName: any;
  file: any;
  path: any = 'logo';
  ProgressValue = 0;
  showFileName = false;

  submitted = false;
  SettingList: any;
  // RegionList: Array<Region> = [];
  public page = 1;
  public pageSize = 10;
  public totalPage: any;

  form: FormGroup = new FormGroup({
    club_name: new FormControl(''),
    logo: new FormControl(''),
    email: new FormControl(''),
    number: new FormControl(''),
    about_us: new FormControl(''),
    smtp_server_address: new FormControl(''),
    smtp_port: new FormControl(''),
    smtp_username: new FormControl(''),
    smtp_password: new FormControl(''),
    sender_email: new FormControl(''),
    sender_name: new FormControl(''),
    use_ssl_tls: new FormControl(''),
    default_email_subject_prefix: new FormControl(''),
    default_email_body: new FormControl(''),
    email_footer: new FormControl(''),
    default_cc_email: new FormControl(''),
    default_bcc_email: new FormControl(''),
    email_sending_limit_per_hour: new FormControl('')
  });

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      club_name: ['', Validators.required],
      logo: [''],
      email: ['', [Validators.required, Validators.email]],
      number: ['', Validators.required],
      about_us: [''],
      smtp_server_address: ['', Validators.required],
      smtp_port: [null, Validators.required],
      smtp_username: ['', Validators.required],
      smtp_password: ['', Validators.required],
      sender_email: [''],
      sender_name: [''],
      use_ssl_tls: [false],
      default_email_subject_prefix: [''],
      default_email_body: [''],
      email_footer: [''],
      default_cc_email: [''],
      default_bcc_email: [''],
      email_sending_limit_per_hour: [null]
    });

    this.spinner.show();
    this.getSetting().then(() => this.spinner.hide(), () => this.spinner.hide());
  }

  getSetting() {
    return new Promise((resolve: any, reject: any) => {
      let val: any = {};

      this.service.getSetting(val).subscribe((data) => {
        if (data['status_code'] == 100) {
          this.SettingList = JSON.parse(data['message']);
          this.SettingList = this.SettingList[0];
          this.logoName = this.SettingList.logo;
          this.form = this.formBuilder.group(
            {
              club_name: [this.SettingList.club_name, Validators.required],
              logo: [this.SettingList.logo],
              email: [this.SettingList.email, [Validators.required, Validators.email]],
              number: [this.SettingList.number, Validators.required],
              about_us: [this.SettingList.about_us],
              smtp_server_address: [this.SettingList.smtp_server_address, Validators.required],
              smtp_port: [this.SettingList.smtp_port, Validators.required],
              smtp_username: [this.SettingList.smtp_username, Validators.required],
              smtp_password: [this.SettingList.smtp_password, Validators.required],
              sender_email: [''],
              sender_name: [''],
              use_ssl_tls: [this.SettingList.use_ssl_tls],
              default_email_subject_prefix: [''],
              default_email_body: [''],
              email_footer: [''],
              default_cc_email: [''],
              default_bcc_email: [''],
              email_sending_limit_per_hour: [null]
            }
          );

        } else {
          showInfoToast('No data found');
        }
        resolve();
      }, err => {
        if (err.status == 401 && err.statusText == 'Unauthorized') {
          showDangerToast('Your session has expired. please sign in')
          this.router.navigate(['/Login']);
        }
        else {
          // console.log('Api Error - ', JSON.stringify(err));
          showDangerToast(this.service.errorMsg);
        }
        reject();
      });

    });
  }

  saveSetting() {

    this.submitted = true;
    if (this.form.invalid) {
      this.scrollToFirstInvalidControl();
      return;
    }

    var logoFileName = this.form.value.logo;
    if (this.file != null) {
      logoFileName = this.file.name;
    }

    var val = {
      ClubName: this.form.value.club_name,
      Logo: logoFileName,
      Email: this.form.value.email,
      Number: this.form.value.number,
      AboutUs: this.form.value.about_us,
      SmtpServerAddress: this.form.value.smtp_server_address,
      SmtpPort: this.form.value.smtp_port,
      SmtpUsername: this.form.value.smtp_username,
      SmtpPassword: this.form.value.smtp_password,
      UseSslTls: this.form.value.use_ssl_tls,
    }
    // this.spinner.show();

    this.service.updateSetting(val).subscribe(data => {
      if (data["status_code"] == 100) {

        if (this.file != null) {
          this.uploadeFile().then(() => {
            this.getSetting();
            document.body.style.cursor = "default";
            showSuccessToast(JSON.parse(data["message"])[0]["message"]);

          }, () => {
            this.getSetting();
            document.body.style.cursor = "default";
            showDangerToast("Club setting saved but file not uploaded , Please try agin later");
          });
        }
        else {
          this.getSetting();
          showSuccessToast(JSON.parse(data["message"])[0]["message"]);
          this.spinner.hide();
        }


      }
      else if (data["status_code"] == 200) {
        showDangerToast(JSON.parse(data["message"])[0]["message"]);
        this.spinner.hide();
      }
      else if (data["status_code"] == 300) {
        showDangerToast(JSON.parse(data["message"])[0]["message"]);
        this.spinner.hide();
      }
      else {
        showDangerToast(this.service.errorMsg);
        this.spinner.hide();
      }
    }, err => {
      this.spinner.hide();
      if (err.status == 401 && err.statusText == 'Unauthorized') {
        showDangerToast('Your session has expired. please sign in')
        this.router.navigate(['/Login']);
      }
      else {
        // console.log('Api Error - ', JSON.stringify(err));
        showDangerToast(this.service.errorMsg);
      }
    });
  }

  private scrollToFirstInvalidControl() {
    let form: any
    form = document.getElementById('form');
    let firstInvalidControl = form.getElementsByClassName('ng-invalid')[0];
    firstInvalidControl.scrollIntoView();
    (firstInvalidControl as HTMLElement).focus();
  }
  uploadeFile() {
    return new Promise((resolve: any, reject: any) => {
      const fromData: FormData = new FormData();
      fromData.append(this.path, this.file, this.file.name);
      this.service.upload(fromData).pipe(
        map(events => {
          switch (events.type) {
            case HttpEventType.UploadProgress:
              this.ProgressValue = Math.round(events.loaded / events.total! * 100);
              break;
            case HttpEventType.Response:

              resolve();
              this.FileInput.nativeElement.value = '';
              this.file = null;

              setTimeout(() => {
                this.ProgressValue = 0;
              }, 2000)

              break;
          }
        }),
        catchError((error: HttpErrorResponse) => {
          console.log(error);
          reject();
          return of("Failed")
        })
      ).subscribe((data: any) => {
        // resolve();
        // this.file = null;

      }, err => {
        // alert('error');
        // console.log(err);
        // reject();
      });
    })
  }

  handelFile(event: any) {

    this.file = event.target.files[0];

    if (this.file.size > 504857600) {
      event.target.value = null;
      showDangerToast("Maximum 500mb size allowed");
      return;
    }

    var extention = this.file.name.split('.').pop().toLowerCase();

    // image/png, image/gif, image/jpeg, image/jpg, video/mp4, video/mov, video/wmv, video/avi, video/mkv, video/mpeg-2
    if (
      extention != 'png' && extention != 'jpeg' && extention != 'jpg') {
      showDangerToast("Please upload file of PNG JPEG JPG format only.");
      event.target.value = null;
      return;
    }

    // if (extention == 'mp4' || extention == 'mov' || extention == 'wmv' || extention == 'avi' || extention == 'mkv' || extention == 'mpeg-2') {
    //   this.form.get('Duration')?.disable();
    //   var video = document.createElement('video');
    //   video.preload = 'metadata';

    //   video.onloadedmetadata = () => {
    //     window.URL.revokeObjectURL(video.src);
    //     this.form.get('Duration')?.setValue(Math.trunc(video.duration));
    //   }
    //   video.src = URL.createObjectURL(this.file);
    // }
    // else {
    //   this.form.get('Duration')?.enable();
    // }
    this.showFileName = false;
    // this.form.get('File_Name')?.setValue(this.file.name);
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }
}
