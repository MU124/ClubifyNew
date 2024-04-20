import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { SharedService } from './shared.service';
declare function showDangerToast(msg: any): any;
@Injectable({
  providedIn: 'root'

})
export class AuthGuardService implements CanActivate {

  constructor(public router: Router, private service: SharedService) { }

  canActivate(next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    try {
      const expectedRole = next.data['expectedRole'];
      let currentUser: any = localStorage.getItem('currentUser');
      if (currentUser) {
        currentUser = JSON.parse(this.service.decryptData(currentUser));
        if (currentUser && currentUser.role_id == expectedRole) {
          return true;
        }
        else {
          showDangerToast('You are not authorized to access this page.');
          return false;
        }
      }
      this.service.isAuthenticated = false;
      this.router.navigate(['/Login']);
      return false;

    }
    catch (error) {
      this.service.isAuthenticated = false;
      this.router.navigate(['/Login']);
      return false;
    }

  }
}
