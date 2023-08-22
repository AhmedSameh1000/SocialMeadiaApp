import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { User } from "../Models/user";
import { UserService } from "../Services/User/user.service";
import { Resolver } from "../interfaces/resolver";
import { paginationResult } from "../Models/pagination";

@Injectable({ providedIn: 'root' })
export class Lists_Resolver  {
    pageNumber=1;
    pageSize=6;
    LikeParam="Likers"
      constructor(private UserService: UserService) {}
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): paginationResult<User[]> | Observable<paginationResult<User[]>> | Promise<paginationResult<User[]>> {
        return this.UserService.GetUsers(this.pageNumber,this.pageSize,this.LikeParam)
    }
}
