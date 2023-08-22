import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from '../Guards/auth.guard';

import { member_Details_resplver } from '../Resolvers/member_Details_resplver';
import { member_list_resplver } from '../Resolvers/member_list_resplver';
import { MemberDetailsComponent } from './member-details/member-details.component';
import { MemberListComponent } from './member-list/member-list.component';
import { Lists_Resolver } from '../Resolvers/Lists_Resolver';
import { LikersAndlikeesComponent } from './likers-andlikees/likers-andlikees.component';
import { Message_resolver } from '../Resolvers/Message_resolver';
import { MessagesComponent } from './messages/messages.component';

const routes: Routes = [
  {
    path: "members",
    component: MemberListComponent,
    canActivate: [authGuard],
    resolve: {
        users:member_list_resplver
      }
  },
  {
    path: "members/:id",
    component: MemberDetailsComponent,
    canActivate: [authGuard],
    resolve: {
        user:member_Details_resplver
      }
  },
  {
    path: "Contact",
    component: LikersAndlikeesComponent,
    canActivate: [authGuard],
    resolve: {
        user:Lists_Resolver
      }
  },
  {
    path: "Messages",
    component: MessagesComponent,
    resolve: {
        Messages:Message_resolver
      }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MembersRoutingModule { }
