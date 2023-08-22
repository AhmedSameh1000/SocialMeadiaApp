import { Component } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from '../Services/Auth/auth.service';
import { UserService } from '../Services/User/user.service';
import { smalluser } from '../Models/smalluser';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
 Jwt = new JwtHelperService();
  constructor(public UserServices:UserService,public AuthService:AuthService) {
  }
  ngOnInit(): void {
    this.GetUserData();
  }
  GetUserData() {
    if (!localStorage.getItem("token")) {
      return;
    }
    var userId=this.AuthService.GetUserId();
    this.UserServices.GetUserData(userId).subscribe((res) => {
    })
  }
}
