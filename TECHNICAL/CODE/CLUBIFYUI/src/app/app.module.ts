import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedService } from './shared.service';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { LayoutComponent } from './layout/layout.component';
import { NgbPaginationModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { HomeComponent } from './admin/home/home.component';
import { NgApexchartsModule } from "ng-apexcharts";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations"
import { NgxSpinnerModule } from "ngx-spinner";
import { DragDropModule } from "@angular/cdk/drag-drop";

import { PlayersComponent } from './admin/players/players.component';
import { TeamsComponent } from './admin/teams/teams.component';
import { MatchesComponent } from './admin/matches/matches.component';
import { SettingsComponent } from './admin/settings/settings.component';
import { MembershipRegistrationComponent } from './admin/membership-registration/membership-registration.component';
import { FileuploadComponent } from './fileupload/fileupload.component'


@NgModule({
  declarations: [AppComponent,
    LoginComponent,
    LayoutComponent,
    HomeComponent,
    PlayersComponent,
    TeamsComponent,
    MatchesComponent,
    SettingsComponent,
    MembershipRegistrationComponent,
    FileuploadComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    NgbPaginationModule,
    NgbAlertModule,
    NgApexchartsModule,
    BrowserAnimationsModule,
    NgxSpinnerModule,
    DragDropModule
  ],
  providers: [SharedService],
  bootstrap: [AppComponent],
})
export class AppModule { }
