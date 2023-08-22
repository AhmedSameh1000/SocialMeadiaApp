import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { UserService } from 'src/app/Services/User/user.service';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
constructor(
    private Fb: FormBuilder,
    private Service: AuthService,
    private UserService:UserService,
  private Router: Router,
  private Toaster:ToastrService
  ) {}
  submited = false;
  ngOnInit(): void {  
    this.CreateForm();
    
  }
  signInForm!: FormGroup;
  CreateForm() {
    this.signInForm = this.Fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      userName: ['', [Validators.required, Validators.minLength(5)]],
      name: ['', [Validators.required, Validators.minLength(3)]],
      Gender: ['', [Validators.required]],
    });
  }
  Errors = '';
  Register() {
    if (this.signInForm.invalid) {
      return;
    }
    this.Service.Register(this.signInForm.value).subscribe({
      next:(res:any)=>{
   
        localStorage.setItem('token', res.token);
        this.UserService.GetUserData(this.Service.GetUserId()).subscribe(res=>{

        })
      },
      complete:()=>{
        this.Toaster.success("Use Created Succefuly")
        this.Router.navigate(["members"])

      }
    });
  }
  Onsubmit() {
    this.submited = true;
    if (this.signInForm.invalid) {
      return;
    }
  }
}
