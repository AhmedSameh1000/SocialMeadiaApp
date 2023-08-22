import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { User } from "../Models/user";
import { UserService } from "../Services/User/user.service";
import { Resolver } from "../interfaces/resolver";

@Injectable({ providedIn: 'root' })
export class member_Details_resplver implements Resolver<User> {
      constructor(private UserService: UserService) {}
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): User | Observable<User> | Promise<User> {
        return this.UserService.GetUser(route.paramMap.get("id"))
    }
}
