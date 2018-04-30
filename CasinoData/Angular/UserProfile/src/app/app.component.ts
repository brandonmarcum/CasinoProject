import { Component } from '@angular/core';
import { User } from './User';
import { UserProfileService } from './user-profile.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  
})
export class AppComponent {
  title = 'app';
  User: User;
  client: UserProfileService;

   constructor(client: UserProfileService)
   {
     
     console.log("constructed userprofileservice with route: "+ window.location.href);
     var route = window.location.href;
     var username = route.split("=")[1];
     console.log("username: " + username);
     this.client = client;
     this.client.getUser(username).subscribe(
     (userdata) => {
       this.User= userdata; 
       console.log(userdata);
       this.User.white = 0;
       this.User.blue = 0;
       this.User.red = 0;
       this.User.black = 0;
       this.User.purple = 0;
       this.User.green = 0;
       this.User.orange = 0;
      });
     
   }
   add(type: string)
   {
     switch(type){
       
      case("white"):
      this.User.white++;
      break;
      case("red"):
      this.User.red++;
      break;
      case("green"):
      this.User.green++;
      break;
      case("blue"):
      this.User.blue++;
      break;
      case("black"):
      this.User.black++;
      break;
      case("purple"):
      this.User.purple++;
      break;
      case("orange"):
      this.User.orange++;
      break;
      default:
      break;
     }
    }
     subtract(type: string)
     {
       
       switch(type){
         
        case("white"):
        if(this.User.white>0)
        this.User.white--;
        break;
        case("red"):
        if(this.User.red>0)
        this.User.red--;
        break;
        case("green"):
        if(this.User.green>0)
        this.User.green--;
        break;
        case("blue"):
        if(this.User.blue>0)
        this.User.blue--;
        break;
        case("black"):
        if(this.User.black>0)
        this.User.black--;
        break;
        case("purple"):
        if(this.User.purple>0)
        this.User.purple--;
        break;
        case("orange"):
        if(this.User.orange>0)
        this.User.orange--;
        break;
        default:
        break;
       }
   }

}