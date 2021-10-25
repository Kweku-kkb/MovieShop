import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/shared/models/user';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isAuth!: boolean;
  user!: User;
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.authService.isAuth.subscribe(
      (data) => {
        this.isAuth = data;
        console.log(this.isAuth);
      }
    );

    this.authService.currentUser.subscribe(
      (data) => {
        this.user = data;
        console.log(this.user);
      }
    )
  }
  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}

