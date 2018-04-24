import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { UserComponent } from '../User/user.component';


import { AppComponent } from './app.component';
import { HttpClient } from 'selenium-webdriver/http';
import { HttpClientModule } from '@angular/common/http';
import { UserProfileService } from './user-profile.service';


@NgModule({
  declarations: [
    AppComponent,
    UserComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  exports: [
    AppComponent
  ],
  providers: [
    UserProfileService,
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
