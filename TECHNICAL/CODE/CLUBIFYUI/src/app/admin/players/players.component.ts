import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { AbstractControl, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';

declare function showModal(id: any): any;
declare function closePopup(id: any): any;
declare function showSuccessToast(msg: any): any;
declare function showDangerToast(msg: any): any;
declare function showInfoToast(msg: any): any;
declare function showWarningToast(msg: any): any;


@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.css']
})
export class PlayersComponent implements OnInit {
  currentUser: any;
  currentUserID: any;

  constructor(private service: SharedService, private router: Router, private formBuilder: FormBuilder, private spinner: NgxSpinnerService) {
  }




  ngOnInit(): void {
  }

}
