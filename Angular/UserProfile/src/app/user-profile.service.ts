import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './User';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class UserProfileService {

  userclient: HttpClient

  constructor(client: HttpClient){
  
      this.userclient = client;
  }
  // getUser() {

  //   return this.userclient.get<User>('https://localhost:5001/getcurrentuser').toPromise<User>().then(this.pass, this.fail);
  // }
   getUser (username : string): Observable<User> {
     return this.userclient.get<User>('http://localhost:5002/api/user/datauserprofileget/'+ username);
   }
  pass(response: any) {
      console.log(response);
  }
  fail(response: any) {
      console.log(response);
  }
}
