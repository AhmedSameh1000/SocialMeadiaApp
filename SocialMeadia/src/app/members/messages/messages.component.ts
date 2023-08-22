import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Message } from 'src/app/Models/message';
import { Pagination, paginationResult } from 'src/app/Models/pagination';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { MessageService } from 'src/app/Services/Message/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent {
  messages!:Message[]
  pagination!:Pagination
  messageType="Unread";
  constructor(
    private MessageService:MessageService,
    private authservice:AuthService,
    private Route:ActivatedRoute,
    private Toaster:ToastrService
    ){
  }
  ngOnInit(): void {
    // this.LoadUsers();
    this.Route.data.subscribe(({ Messages }) => {
      this.messages=Messages.result   
    this.pagination=Messages.pagination
    console.log(Messages)
    })
  }
  
  loadMessages(msgType:string){
    this.messageType=msgType;
    this.MessageService.GetMessages(this.authservice.GetUserId(),
    this.pagination.currentPage,
    this.pagination.itemPerPage,
    this.messageType).subscribe((res:paginationResult<Message[]>)=>{
      this.messages=res.result;
      this.pagination=res.pagination;
    })
  }
  
    pageChaged(event:any){
      this.pagination.currentPage=event.page;
      this.loadMessages(this.messageType);
      }
        
}
