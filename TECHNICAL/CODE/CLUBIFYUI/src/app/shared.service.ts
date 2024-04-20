import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root',
})
export class SharedService {

  readonly APIUrl = environment.apiUrl;
  readonly MediaUrl = environment.apiPhotoUrl;
  readonly errorMsg = environment.errorMsg;

  public isAuthenticated = true; // this need to be false once login function work
  public currentUser: any;
  public currentUserID: any;
  public token: any;

  constructor(private http: HttpClient) {

    try {
      this.currentUser = localStorage.getItem('currentUser');

      if (this.currentUser) {
        this.currentUser = JSON.parse(this.decryptData(this.currentUser));
        this.token = this.currentUser.Token;
        this.isAuthenticated = true;
      }
      else {
        this.isAuthenticated = false;
      }
    }
    catch (error) {
      this.isAuthenticated = false;
    }

  }


  // General Routes

  encryptData(data: string): string {
    return CryptoJS.AES.encrypt(data, 'metasync124').toString();
  }

  decryptData(encryptedData: string): string {
    const bytes = CryptoJS.AES.decrypt(encryptedData, 'metasync124');
    return bytes.toString(CryptoJS.enc.Utf8);
  }

  getLogin(val: any) {

    return this.http.post(this.APIUrl + 'Login', val);
  }

  downloadContent(url: string): Observable<Blob> {
    return this.http.get(url, { responseType: 'blob' });
  }

  //File Upload
  upload(val: any) {
    return this.http.post(this.APIUrl + 'Upload', val, {
      reportProgress: true,
      observe: 'events',
      headers: {
        'Authorization': `Bearer ${this.token}`
      }
    });
  }

  // Admin Routes

  registerMemeber(val: any) {
    return this.http.post<any>(this.APIUrl + 'Player/Registration', val,);
  }


  getSetting(val: any) {
    return this.http.post<any>(this.APIUrl + 'Settings/Get', val, {
      headers: {
        'Authorization': `Bearer ${this.token}`
      }
    });
  }

  updateSetting(val: any) {
    return this.http.post<any>(this.APIUrl + 'Settings/Update', val, {
      headers: {
        'Authorization': `Bearer ${this.token}`
      }
    });
  }


  //Captain Routes



  //Player Routes






}
