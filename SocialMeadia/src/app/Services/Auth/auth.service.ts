import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment.development';
import { UserService } from '../User/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

 constructor(private Http: HttpClient, private Route: Router,private UserService:UserService) {
    
  }
  JWTHealper = new JwtHelperService();
  login(Login: any) {
    return this.Http.post(
      environment.BaseApi + 'api/Auth/LogIn',
      Login
    );
  }
  Register(Signup: any) {
    return this.Http.post(environment.BaseApi + 'api/Auth/Register', Signup);
  }

  loggedIn() {
    try {
       const token:any = this.getToken();
        return !this.JWTHealper.isTokenExpired(token);
    }
    catch {
      return false;
    }
    
  }
  isAdmin() {
    if (this.UserService.smallUser.roles.includes("Admin")) {
      return true;
    }
    else {
      return false;    
    }
  }
  getToken() {
    if (!localStorage.getItem('token')) {
      return;
    }
    return localStorage.getItem('token');
  }
  GetUserId() {
    var token: any = this.getToken();
    var UserId = this.JWTHealper.decodeToken(token).uid;
    return UserId;

  }
  LogOut() {
    this.Route.navigate([""])
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }
 
}
