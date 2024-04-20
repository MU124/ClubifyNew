import { Component } from '@angular/core';
import { SharedService } from './shared.service';
import { Router, NavigationEnd } from '@angular/router';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Back Office';
  currentUrl: string = '';
  isSingle = false;
  constructor(public service: SharedService, private router: Router) {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.currentUrl = event.url;

        if (this.currentUrl == "/Registration") {
          this.isSingle = true;
        }
        else {
          this.isSingle = false;
        }
      }
    });
  }
}
