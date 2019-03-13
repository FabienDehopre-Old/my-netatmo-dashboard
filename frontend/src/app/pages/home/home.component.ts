import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  userInfo$?: Observable<any>;

  constructor(private readonly authService: AuthService) {}

  ngOnInit(): void {
    this.userInfo$ = this.authService.getUserInfo();
  }

  logout(): void {
    this.authService.logout(`${window.location.origin}/`);
  }
}
