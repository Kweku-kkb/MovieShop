import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { Login } from 'src/app/shared/models/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  invalidLogin: boolean | undefined;
  userLogin: Login = {
    email: '', password: ''
  }; 
  
  //DI of authentication service, router fron Angular
  constructor( private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    console.log(this.userLogin)
  }

  login() {
    console.log('button was clicked');
    //call login method in auth service
    this.authService.login(this.userLogin).subscribe(
      (resp) => {
        if(resp){
          // if auth service returns true, redirect to home page
          this.router.navigate(['/']);
        } else {
          // stay on the same page and show some error message saying uname/pword is invalid
          this.invalidLogin = true; // we must create an invalidLogin method in the view(html) to show error message
        } 
      },
      (err: any) => {
        (this.invalidLogin = true), console.log(err);
      }
    );

    
  }
}
