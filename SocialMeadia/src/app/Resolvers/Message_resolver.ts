import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { paginationResult } from "../Models/pagination";
import { MessageService } from "../Services/Message/message.service";
import { Message } from "../Models/message";
import { AuthService } from "../Services/Auth/auth.service";

@Injectable({ providedIn: 'root' })
export class Message_resolver  {
    pageNumber=1;
    pageSize=6;
    MessageType="unread"
      constructor(private MessageService: MessageService,private authservice:AuthService) {}
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): paginationResult<Message[]> | Observable<paginationResult<Message[]>> | Promise<paginationResult<Message[]>> {
        return this.MessageService.GetMessages(this.authservice.GetUserId(),this.pageNumber,this.pageSize,this.MessageType)
    }
}
