import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './admin/home/home.component';
import { LoginComponent } from './login/login.component';

import { PlayersComponent } from './admin/players/players.component';
import { TeamsComponent } from './admin/teams/teams.component';
import { MatchesComponent } from './admin/matches/matches.component';
import { SettingsComponent } from './admin/settings/settings.component';
import { MembershipRegistrationComponent } from './admin/membership-registration/membership-registration.component';
import { AuthGuardService as AuthGuard } from './auth-guard.service';
import { FileuploadComponent } from './fileupload/fileupload.component';


const routes: Routes =
  [{ path: '', component: LoginComponent },
  { path: 'Login', component: LoginComponent },
  { path: 'Registration', component: MembershipRegistrationComponent },
  // Admin Routes
  { path: 'Home', component: HomeComponent, canActivate: [AuthGuard], data: { expectedRole: '1' } },
  { path: 'Players', component: PlayersComponent, canActivate: [AuthGuard], data: { expectedRole: '1' } },
  { path: 'Teams', component: TeamsComponent, canActivate: [AuthGuard], data: { expectedRole: '1' } },
  { path: 'Matches', component: MatchesComponent, canActivate: [AuthGuard], data: { expectedRole: '1' } },
  { path: 'Settings', component: SettingsComponent, canActivate: [AuthGuard], data: { expectedRole: '1' } },
  { path: 'Upload', component: FileuploadComponent, }


    //Captain Routes


    //Payer Routes
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
