import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Login } from 'src/app/shared/models/login';
import { User } from 'src/app/shared/models/user';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  //private means can be used only inside this class to push data
  private currentUserSubject = new BehaviorSubject<User>({} as User)

  //public here so that any component can subscribe to this subject and get the data
  public currentUser =  this.currentUserSubject.asObservable();

  private isAuthSubject =  new BehaviorSubject<boolean> (false);

  //Header component can use this to check whether user is authenticated so that if true, hide login/register buttons in header
  public isAuth =  this.isAuthSubject.asObservable();

  private user!: User;

  constructor( private apiServie: ApiService) { }


  //take username and password from login component and post it to API service that will post to API
  login(userLogin: Login) : | Observable<boolean> {
    return this.apiServie.create('account/login', userLogin)
    .pipe(map(resp => {
      if(resp){
        //we save to local storage if resp is not null
        localStorage.setItem('token', resp.token);
        this.populateUserInfo();
        return true;
      }
      return false;
    }));
  }

  populateUserInfo() {
    //get the token from localstorage and decode it and get the user info such as email, first name etc to show in the header
    var token = localStorage.getItem('token');
    if(token){
      //decode token
      var decodedToken =  new JwtHelperService().decodeToken(token);
      this.user = decodedToken;

      //pushing data to the observable
      this.currentUserSubject.next(this.user);
      this.isAuthSubject.next(true);
    }
  }

  logout () {
    //remove the token from local storage and cnage the subjects to default values
    localStorage.removeItem('token');
    this.currentUserSubject.next( {} as User);
    this.isAuthSubject.next(false);
  }
}
