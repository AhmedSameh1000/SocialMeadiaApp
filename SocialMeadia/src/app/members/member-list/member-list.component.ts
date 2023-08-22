import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Pagination, paginationResult } from 'src/app/Models/pagination';
import { User } from 'src/app/Models/user';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { UserService } from 'src/app/Services/User/user.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent {
  constructor(
    private UserService: UserService,
    private route:ActivatedRoute,
    private Toaster:ToastrService,
    private AuthService:AuthService) {
    }
    Users!: User[]
   pagination!:Pagination
  ngOnInit(): void {
    this.route.data.subscribe(({ users }) => {
      this.Users=users.result   
    this.pagination=users.pagination
    console.log(users)
    })
  }
  SendLike(recipientid:string){
    this.UserService.SendLike(this.AuthService.GetUserId(),recipientid)
    .subscribe((res:any)=>{
      console.log(res);
      this.loadUsers();
      this.Toaster.success(`Like Sended To ${this.Users.find(c=>c.id==recipientid)?.name}`)
    })
  }

  pageChanged(event:any){
    this.pagination.currentPage=event.page;
    this.loadUsers();
  }
DisLikethisUser(recipientid:any){
this.UserService.DislikeThisUser(this.AuthService.GetUserId(),recipientid).subscribe(res=>{
  this.Toaster.success(`Dislike ${this.Users.find(c=>c.id==recipientid)?.name??"this User"} successfuly `)
  this.loadUsers();
})
}


  loadUsers(){
    this.UserService.GetUsers(this.pagination.currentPage,this.pagination.itemPerPage)
    .subscribe((res:paginationResult<User[]>)=>{
      this.Users=res.result;
      this.pagination=res.pagination;
    })
  }
}
