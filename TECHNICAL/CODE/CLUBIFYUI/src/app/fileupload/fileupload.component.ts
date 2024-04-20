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
declare function FileUploadInit(): any;

@Component({
  selector: 'app-fileupload',
  templateUrl: './fileupload.component.html',
  styleUrls: ['./fileupload.component.css']
})
export class FileuploadComponent implements OnInit {

  constructor(private service: SharedService, private router: Router, private formBuilder: FormBuilder, private spinner: NgxSpinnerService) {
  }

  @ViewChild('FileInput')
  FileInput!: ElementRef;
  file: any;
  path: any = 'logo';
  ProgressValue = 0;
  showFileName = false;

  form: FormGroup = new FormGroup({
    Media_ID: new FormControl(''),
    File_Name: new FormControl(''),
    Caption: new FormControl(),
    Duration: new FormControl(),
    IsActive: new FormControl(false)
  });

  ngOnInit(): void {

    FileUploadInit();
    this.form = this.formBuilder.group(
      {
        Media_ID: [],
        File_Name: [''],
        Caption: ['', [Validators.required, Validators.pattern("^[a-zA-Z]+$")]],
        Duration: ['', Validators.required],
        IsActive: [false],
      });
  }

  btnClick() {
    this.uploadeFile().then(() => {
      // this.closeModal();
      // this.onReset();
      //this.spinner.hide();
      showSuccessToast("Image uploaded successfully.");
    }, () => {
      //this.spinner.hide();
      document.body.style.cursor = "default";
      showDangerToast("Your media details is added but file not uploaded, Please try agin later");
    });
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

}
