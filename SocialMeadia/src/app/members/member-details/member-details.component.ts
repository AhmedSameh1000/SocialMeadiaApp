import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/Models/user';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { UserService } from 'src/app/Services/User/user.service';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css']
})
export class MemberDetailsComponent {
  constructor(private UserServices:UserService,
    private Toaster:ToastrService,
    private Route:ActivatedRoute,
    private authservice:AuthService) {
  }
  Default:any
ngOnInit(): void {
    this.LoadUserData();
    this.loadparams()
  }

  loadparams(){
    this.Route.queryParams.subscribe((param:any)=>{
      console.log(param.tab)
      this.Default=param.tab
      })
  }
  LoadUserData(){
    this.Route.data.subscribe(({ user }) => {
      this.User = user
      console.log(user)
    });
  }
  change(){
    this.Default=3
  }
  User!: User;
  // LoadUser() {
  //   this.UserServices.GetUser(this.Route.snapshot.params["id"]).subscribe(res => {
  //     this.User = res;
  //   })
  // }
  SendLike(recipientid:any,ele:HTMLElement){
    this.UserServices.SendLike(this.authservice.GetUserId(),recipientid)
    .subscribe(res=>{
      ele.className="fa-solid fa-heart-crack"
      this.User.isLikedByCurrentUser=true
      this.Toaster.success("like sended to "+this.User.name+" successfuly")
    })
  }
  DisLikethisUser(recipientid:any,ele:HTMLElement){
    this.UserServices.DislikeThisUser(this.authservice.GetUserId(),recipientid)
    .subscribe(res=>{ 
      ele.className="fa-solid fa-heart me-1"
      this.User.isLikedByCurrentUser=false
      this.Toaster.success("dislike "+this.User.name+" successfuly")

    })
  }

}
