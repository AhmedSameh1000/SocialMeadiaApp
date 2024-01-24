import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Message } from 'src/app/Models/message';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { MessageService } from 'src/app/Services/Message/message.service';
import { SignalRService } from 'src/app/Services/SignalR/signal-r.service';
import { UserService } from 'src/app/Services/User/user.service';

@Component({
  selector: 'app-membermessages',
  templateUrl: './membermessages.component.html',
  styleUrls: ['./membermessages.component.css'],
})
export class MembermessagesComponent {
  @Input() recipientId!: string;
  Messages!: Message[];
  _MessageToSend!: FormGroup;
  constructor(
    private MessageService: MessageService,
    private ToastrService: ToastrService,
    public authservice: AuthService,
    private FormBuilder: FormBuilder,
    private Route: ActivatedRoute,
    private UserService: UserService,
    private SignalR: SignalRService
  ) {}
  ngOnInit(): void {
    let token = this.authservice.getToken();
    this.SignalR.startConnection(token);
    this.LoadMessages();
    this.CreateMessage();
    this.SignalR.HubConnection.on('Data', (message: Message) => {
      console.log(message);
      this.Messages.push(message);
    });
  }
  CreateMessage() {
    this._MessageToSend = this.FormBuilder.group({
      senderId: [this.authservice.GetUserId(), [Validators.required]],
      recipientId: [this.Route.snapshot.params['id'], [Validators.required]],
      content: ['', [Validators.required]],
    });
  }

  SendnewMessage(inp: HTMLInputElement) {
    if (inp.value == '') {
      this.ToastrService.success('cannot send empty message');
      return;
    }

    this.MessageService.SendMessage(
      this.authservice.GetUserId(),
      this._MessageToSend.value
    ).subscribe((Message: Message) => {
      console.log(Message);

      Message.senderPhotoUrl = this.UserService.smallUser.url;
      this.Messages.push(Message);
      this.SignalR.HubConnection.invoke(
        'Refresh',
        this._MessageToSend.value.recipientId,
        this._MessageToSend.value
      ).then(
        (res: any) => {
          console.log('call sercvies ' + res);
        },
        (Error: any) => {
          console.log('error happen ' + Error);
        }
      );
    });
  }
  LoadMessages() {
    this.MessageService.GetConversation(
      this.authservice.GetUserId(),
      this.recipientId
    ).subscribe((res) => {
      this.Messages = res;
      console.log(res);
    });
  }
}
