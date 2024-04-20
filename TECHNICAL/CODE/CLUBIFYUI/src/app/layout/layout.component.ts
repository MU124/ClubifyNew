import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  userPermissions: any;
  currentUser: any;
  role_id: any;
  Username: any;
  RoleName: any;
  ProfilePicturePath: any;
  player_id: any;


  constructor(public sharedService: SharedService,
    private router: Router,
    private service: SharedService) { }

  ngOnInit(): void {
    this.currentUser = localStorage.getItem('currentUser');
    if (this.currentUser) {
      this.currentUser = JSON.parse(this.service.decryptData(this.currentUser));
      this.player_id = this.currentUser.player_id;
      this.service.token = this.currentUser.Token;
      this.role_id = this.currentUser.role_id;
      this.Username = this.currentUser.first_name + ' ' + this.currentUser.last_name;

      if (this.role_id == 1) {
        this.RoleName = "Admin";
      }
      else if (this.role_id == 2) {
        this.RoleName = "Captain";
      }
      else {
        this.RoleName = "Player";
      }
    }
  }

  usermenuList: any = [];
  usermenu: any;

  SignOut() {
    this.service.isAuthenticated = false;
    this.service.currentUser = null;
    this.service.currentUserID = null;
    localStorage.removeItem('currentUser');
    this.router.navigate(['/Login']);
  }
  profile() {

    // this.router.navigate(['/profile']);
  }
  bindMenu(dataItem: any) {
    //console.log('data');
    //console.log(dataItem);
    //this.usermenuList = dataItem;
  }

  // refreshusermenuList() {

  //   this.currentUser = localStorage.getItem('currentUser');

  //   if (this.currentUser) {
  //     this.currentUser = JSON.parse(this.currentUser);
  //     this.UserRoleID = this.currentUser.UserRoleID;
  //     this.Username = this.currentUser.Username;
  //     this.ProfilePicturePath = this.currentUser.ProfilePicturePath
  //   }

  //   let val = { UserRoleID: this.UserRoleID }

  //   this.service.getusermenuList(val).subscribe(data => {

  //     if (data["status_code"] == 100) {
  //       this.usermenuList = JSON.parse(data["message"]);

  //       // console.log(this.usermenuList); 

  //       this.userPermissions = this?.usermenuList.reduce((acc: any, ele: any) => {
  //         if (acc.length === 0) {
  //           acc.push(this.getModifiedMainMenu(ele));
  //           return acc;
  //         } else {
  //           const existedMenu = acc.find((m: any) => m.MenuHeaderCaption === ele.MenuHeaderCaption);
  //           if (existedMenu) {
  //             existedMenu.MenuSubHeaderCaption.push(this.getSubMenu(ele));
  //             return acc;
  //           } else {
  //             acc.push(this.getModifiedMainMenu(ele));
  //             return acc;
  //           }
  //         }
  //       }, []);
  //       // console.log(this.userPermissions);     
  //     }
  //   })


  // }


  getModifiedMainMenu(obj: any) {
    return {
      MenuHeaderCaption: obj.MenuHeaderCaption,
      MenuHeaderURL: obj.MenuHeaderURL,
      MenuIconPath: obj.MenuIconPath,

      MenuSubHeaderCaption: [this.getSubMenu(obj)]
    };
  }

  getSubMenu(obj: any) {
    return {
      MenuHeaderCaption: obj.MenuHeaderCaption,
      MenuSubHeaderID: obj.MenuSubHeaderID,
      MenuSubHeaderCaption: obj.MenuSubHeaderCaption,
      MenuSubHeaderIconPath: obj.MenuSubHeaderIconPath,
      MenuSubHeaderURL: obj.MenuSubHeaderURL
    };
  }



  changeSubMenus(event: any, MenuHeaderCaption: any, id: any) {
    //console.log(MenuHeaderCaption, id);
  }

}
