import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Pagination, paginationResult } from 'src/app/Models/pagination';
import { User } from 'src/app/Models/user';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { UserService } from 'src/app/Services/User/user.service';

@Component({
  selector: 'app-likers-andlikees',
  templateUrl: './likers-andlikees.component.html',
  styleUrls: ['./likers-andlikees.component.css']
})
export class LikersAndlikeesComponent {
  constructor(
    private UserService: UserService,
    private route:ActivatedRoute,
    private Toaster:ToastrService,
    private AuthService:AuthService) {
}
 pagination!:Pagination
 Users!: User[]
ngOnInit(): void {
  this.likeParam="Likers"
  // this.LoadUsers();
  this.route.data.subscribe(({ user }) => {
    this.Users=user.result   
  this.pagination=user.pagination
  })

}
likeParam:any;
loadUsers(like:any){
  this.likeParam=like
  this.UserService.GetUsers(this.pagination.currentPage,this.pagination.itemPerPage,this.likeParam)
  .subscribe((res:paginationResult<User[]>)=>{
    this.Users=res.result;
    this.pagination=res.pagination;
    console.log(res)

  })
}
pageChanged(event:any){
  this.pagination.currentPage=event.page;
  this.loadUsers(this.likeParam);
}
}
