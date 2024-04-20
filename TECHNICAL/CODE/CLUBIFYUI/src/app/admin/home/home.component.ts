import { Component, OnInit, } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { Chart } from 'chart.js';
import { Router } from '@angular/router';
declare function showDangerToast(msg: any): any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  BranchList: any = [];
  ScreenList: any = [];
  PlaylistList: any = [];
  MediaList: any = [];
  currentUser: any;
  player_id: any;

  constructor(private service: SharedService, private router: Router,) {
    // this.currentUser = localStorage.getItem('currentUser');
    // if (this.currentUser) {
    //   this.currentUser = JSON.parse(this.service.decryptData(this.currentUser));
    //   console.log(this.currentUser);
    //   this.player_id = this.currentUser.player_id;
    // }
  }

  ngOnInit(): void {

    // this.getBranch();
    // this.getScreen();
    // this.getPlaylist();
    // this.getMedia();
  }

}
