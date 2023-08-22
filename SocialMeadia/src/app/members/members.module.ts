import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ToastrModule } from 'ngx-toastr';
import { SharedModule } from '../shared/shared.module';
import { MemberDetailsComponent } from './member-details/member-details.component';
import { MemberListComponent } from './member-list/member-list.component';
import { MembersRoutingModule } from './members-routing.module';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { LikersAndlikeesComponent } from './likers-andlikees/likers-andlikees.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MessagesComponent } from './messages/messages.component';
import { MembermessagesComponent } from './membermessages/membermessages.component';

@NgModule({
  declarations: [
    MemberListComponent,
    MemberDetailsComponent,
    LikersAndlikeesComponent,
    MessagesComponent,
    MembermessagesComponent
  ],
  imports: [
    CommonModule,
    MembersRoutingModule,
    ToastrModule.forRoot(),
    SharedModule,
    PaginationModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ]
})
export class MembersModule { }
