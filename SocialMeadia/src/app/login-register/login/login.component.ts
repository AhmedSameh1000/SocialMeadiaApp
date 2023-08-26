import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { SignalRService } from 'src/app/Services/SignalR/signal-r.service';
import { UserService } from 'src/app/Services/User/user.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  LogInForm!: FormGroup;
  name = 'Marriage';
    constructor(
    private FormBuilder: FormBuilder,
    public Authservice: AuthService,
      private Router: Router,
      private Toaster: ToastrService,
    private UserService:UserService,

  ) { 
    }
  
    ngOnInit(): void {
    this.CreateForm();
  }
  CreateForm() {
    this.LogInForm = this.FormBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }
  
 Login() {
  
    if (this.LogInForm.invalid) {
      return;
    }
    this.Authservice.login(this.LogInForm.value).subscribe({

      next: (e: any) => {  
        this.name = e.name;       
        localStorage.setItem("token", e.token); 
        this.UserService.GetUserData(this.Authservice.GetUserId()).subscribe(res=>{
          
        })
        
      },
      complete: () => {
        this.Toaster.success("User log in Succefuly");
         
         this.Router.navigate([""]);
      
      }   
    })
 }
    submited = false;
     Onsubmit() {
    this.submited = true;
    if (this.LogInForm.invalid) {
      return;
    }
   }
}
