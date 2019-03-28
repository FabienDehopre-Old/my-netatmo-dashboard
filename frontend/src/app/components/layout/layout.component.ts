import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss'],
})
export class LayoutComponent implements OnInit {
  user$!: Observable<any>;

  constructor(private readonly authService: AuthService) {}

  ngOnInit(): void {
    this.user$ = this.authService.getUserInfo();
  }

  logout(): void {
    this.authService.logout();
  }
}
