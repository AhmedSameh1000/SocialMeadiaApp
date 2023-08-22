import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { paginationResult } from 'src/app/Models/pagination';
import { Photo } from 'src/app/Models/photo';
import { smalluser } from 'src/app/Models/smalluser';
import { User } from 'src/app/Models/user';
import { environment } from 'src/environments/environment.development';
import { AuthService } from '../Auth/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private Http: HttpClient, private Route: Router) {
    
  }
  JWTHealper = new JwtHelperService();
  smallUser!:smalluser;
 GetUserData(id:any) {
    return this.Http.get(environment.BaseApi + `api/Photos/GetUserdata/${id}`).pipe(
      map((response: any) => {
        const user = response;
        if (user != null) { 
          localStorage.setItem('user',JSON.stringify(user)) 
          this.smallUser=user;
      }
      }));
  }
  UpdateUserData(id:any,body:any) {
    return this.Http.post(environment.BaseApi + `api/Users/UserData/${id}`, body);
  }
  UpdateUserplace(id:any,body:any) {
    return this.Http.post(environment.BaseApi + `api/Users/Userplace/${id}`, body);
  }
  UpdateUserinformations(id:any,body:any) {
    return this.Http.post(environment.BaseApi + `api/Users/Userinformations/${id}`, body);
  }
  ChangePassword(id:any,body:any) {
    return this.Http.post(environment.BaseApi + `api/Users/ChangePassword/${id}`, body);
  }
  GetUserProfile(id:any) {
    return this.Http.get(environment.BaseApi + `api/Photos/ImgProfile/${id}`);
  }
  ChangeProfilePicture(body:any) {
    return this.Http.post(environment.BaseApi + `api/Photos/UploadProfilePhoto`,body);
  }
    GetUsers(page?:any,itemsPerPage?:any,likeparam?:any,userid?:any):Observable<paginationResult<User[]>> {

      const paginationresult:paginationResult<User[]>=new paginationResult<User[]>();
      let params=new HttpParams();
     if(page!=null&&itemsPerPage!=null){
      params= params.append("pageNumber",page);
      params= params.append("pageSize",itemsPerPage);
    }
    if(likeparam==="Likers"){
      params= params.append("Likers","true");
    }
    if(likeparam==="Likees"){
      params= params.append("Likees","true");
    }
    return this.Http.get<paginationResult<User[]>>(environment.BaseApi+"api/Users",{observe:'response',params}).pipe(
      map((response:any)=>{
        paginationresult.result=response.body;
        if(response.headers.get("pagination")!=null){
          paginationresult.pagination=JSON.parse(response.headers.get("pagination")!);
        }
        return paginationresult;
      })
    )
  }

  GetUser(Id:any):Observable<User> {
    return this.Http.get<User>(environment.BaseApi+`api/Users/${Id}`)
  }
  GetAllphotosForUser(UserId :any):Observable<Photo[]>{
return this.Http.get<Photo[]>(environment.BaseApi+`api/Photos/AllPhotos/${UserId}`)
  }
  DeleteImage(id:any){
return this.Http.delete(environment.BaseApi+`api/Photos/deleteimage/${id}`)
  }
  setImageProfile(id:any){
    return this.Http.get(environment.BaseApi+`api/photos/SetImageProfile/${id}`);
  }
  SendLike(id:string,recipientid:string){
    return this.Http.post(environment.BaseApi+"api/Users/"+id+"/like/"+recipientid,{});
  }
  CurrentUserisLikeRecipientUser(id:any,recipientid:any){
     return this.Http.get(environment.BaseApi+"api/Users/"+id+"/islike/"+recipientid)
  }
  DislikeThisUser(id:any,recipientid:any){
    return this.Http.get(environment.BaseApi+"api/Users/"+id+"/dislike/"+recipientid)
  }
}
