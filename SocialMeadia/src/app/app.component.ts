import { Component, OnInit } from '@angular/core';
import { AuthService } from './Services/Auth/auth.service';
import { smalluser } from './Models/smalluser';
import { UserService } from './Services/User/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'SocialMeadia';
  constructor(private Authservice:AuthService,private UserService:UserService){

  }
  ngOnInit(): void {
    if(localStorage.getItem("user")){
      var User:smalluser= JSON.parse(localStorage.getItem("user")!);
      this.UserService.smallUser=User;   
    }


  }
}
