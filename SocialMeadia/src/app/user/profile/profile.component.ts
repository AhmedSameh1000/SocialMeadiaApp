import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Photo } from 'src/app/Models/photo';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { UserService } from 'src/app/Services/User/user.service';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
 constructor(private toaster:ToastrService, private _formBuilder: FormBuilder, private AuthService: AuthService,private UserService:UserService) { }
  ngOnInit(): void {
    this.GetUserData();
    this.GetUserProfileImg();
    this.CreateForm1();
    this.CreateForm2();
    this.CreateForm3();
    this.CreateForm4();
    this.CreateForm5();
    this.GetAllPhoto();
  }
  UserData: any;
PhotosForuser!:Photo[]
  firstFormGroup!: FormGroup;
  secondFormGroup!: FormGroup;
  thirdFormGroup!: FormGroup;
  UserProfile: any;
  CreateForm1() {
    this.firstFormGroup = this._formBuilder.group({
      name: ['', Validators.required],
      phone: ['', Validators.required],
    });
  }
  FurthFormGroup!: FormGroup;
  CreateForm4() {
    this.FurthFormGroup = this._formBuilder.group({
      oldpassword: ['', Validators.required],
      newpassword: ['', Validators.required],
    });
  }

  CreateForm2() {
    this.secondFormGroup = this._formBuilder.group({
      city: ['', Validators.required],
      country: ['', Validators.required],
    });
  }
  
  CreateForm3() {
    this.thirdFormGroup = this._formBuilder.group({
      interstes: ['', Validators.required],
      introduction: ['', Validators.required],
      KnownAs: ['', Validators.required],
      LookingFor: ['', Validators.required],
      DateOfBirth: ['', Validators.required],
    });
  }
  
  isLinear = false;
  UpdateData() {
    var UserId = this.AuthService.GetUserId();
    this.UserService.UpdateUserData(UserId, this.firstFormGroup.value).subscribe(res => {
 
      this.toaster.success("Data Updated");
    });
  }
  Updateplace() {
    var UserId = this.AuthService.GetUserId();
    this.UserService.UpdateUserplace(UserId, this.secondFormGroup.value).subscribe(res => {
 
      this.toaster.success("Place Updated");
    } );
  }
  Updateinformations() {
    var UserId = this.AuthService.GetUserId();
    this.UserService.UpdateUserinformations(UserId, this.thirdFormGroup.value).subscribe(res => {
 
      this.toaster.success("informations Updated");
    });
  }
  ChangePassword(oldpass:HTMLInputElement,newpass:HTMLInputElement) {
    if(oldpass.value==""||newpass.value==""){
      this.toaster.error("Invalid Operation");
      return;
    }


    var UserId = this.AuthService.GetUserId();
    this.UserService.ChangePassword(UserId, this.FurthFormGroup.value).subscribe(res => {
      this.toaster.success("Password Changed");
    }
    )
  }

  GetUserProfileImg() {
    var Id = this.AuthService.GetUserId();
    this.UserService.GetUserProfile(Id).subscribe((res:any) => {
      this.UserProfile = res;
    });
  }
  GetUserData() {
    var UserId = this.AuthService.GetUserId();
    this.UserService.GetUser(UserId).subscribe((res:any) => {
      this.UserData = res;
    });
  }
    FiveFormGroup!: FormGroup;
  CreateForm5() {
    this.FiveFormGroup = this._formBuilder.group({
      UserId: [this.AuthService.GetUserId()],
      Img: ['', ],
    });
  }
  Create(ele2:HTMLImageElement,inp:HTMLInputElement) {
    if(inp.value==""){
      this.toaster.error("please Select an image");
    }
    let MyData = new FormData();
    MyData.append('UserId', this.FiveFormGroup.value['UserId']);
    MyData.append(
      'Img',
      this.FiveFormGroup.value['Img'],
      this.FiveFormGroup.value['Img'].name
    );
    this.UserService.ChangeProfilePicture(MyData).subscribe((res:any) => {
      this.toaster.success("Profile Picture Changed Succesfuly");
      let currentImageProfile=this.PhotosForuser.find(item=>item.isMain);
      if(currentImageProfile != null){
      currentImageProfile.isMain=false;
      }
    ele2.src=res.url;
    this.UserService.smallUser.url=res.url
    const Photo:Photo= {
      id:res.id,
      url:res.url,
      isMain:res.isMain,
      userId:res.userId
    }
   this.PhotosForuser.push(Photo);
    })
  }
   SelectImage($event: any) {
    this.FiveFormGroup.get('Img')?.setValue($event.target.files[0]);
  }
  GetAllPhoto(){
    this.UserService.GetAllphotosForUser(this.AuthService.GetUserId()).subscribe(res=>{
      this.PhotosForuser=res;
    })
  }
  delete(ele:HTMLElement,id:any,ele2:HTMLImageElement){
    this.UserService.DeleteImage(id.id).subscribe(res=>{
      ele.remove();
      var cuurentimageprofile=this.PhotosForuser.find(f=>f.id==id.id);
      if(cuurentimageprofile?.isMain){
         ele2.src="https://localhost:7268/imagesprofile/Empty.jpg";
         this.UserService.smallUser.url="https://localhost:7268/imagesprofile/Empty.jpg"
      }
    
    this.toaster.success("Image Deleted Successfuly");
     })
  }
  setImageProfile(image:any,ele2:HTMLImageElement){
    this.UserService.setImageProfile(image.id).subscribe(res=>{
      this.toaster.success("Image Profile Updated Successfuly");
      ele2.src=image.url;
      this.UserService.smallUser.url=ele2.src;
      let currentImageProfile=this.PhotosForuser.find(item=>item.isMain);
      if(currentImageProfile != null){
      currentImageProfile.isMain=false;
      }
      image.isMain=true;
    })
  }
}
