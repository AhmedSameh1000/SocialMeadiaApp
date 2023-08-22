import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';
import { member_Details_resplver } from './Resolvers/member_Details_resplver';
import { member_list_resplver } from './Resolvers/member_list_resplver';
import { AuthService } from './Services/Auth/auth.service';
import { UserService } from './Services/User/user.service';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { LoaderInterceptor } from './interceptors/loader.interceptor';
import { LoginRegisterModule } from './login-register/login-register.module';
import { MembersModule } from './members/members.module';
import { SharedModule } from './shared/shared.module';
import { UserModule } from './user/user.module';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { Lists_Resolver } from './Resolvers/Lists_Resolver';
import { Message_resolver } from './Resolvers/Message_resolver';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MembersModule,
    UserModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    UserModule,
    MembersModule,
    LoginRegisterModule,
    ToastrModule.forRoot({  
      positionClass: 'toast-bottom-right',
      progressAnimation:"decreasing",
      progressBar:true,
      closeButton:true,
    }),
    NgxSpinnerModule.forRoot({ type: 'square-jelly-box' }),
    PaginationModule.forRoot()


  ],
  providers: [
    UserService,
    AuthService,
    member_Details_resplver,
    member_list_resplver,
    Message_resolver,
    Lists_Resolver,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptor,
      multi: true,
    },
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
