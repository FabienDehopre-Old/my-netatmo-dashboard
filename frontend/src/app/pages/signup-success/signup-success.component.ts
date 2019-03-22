import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import * as Sentry from '@sentry/browser';
import { map } from 'rxjs/operators';

import { untilComponentDestroyed } from '../../rxjs-operators/until-component-destroyed';
import { AuthService } from '../../services/auth.service';
import { LoggerService } from '../../services/logger.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-signup-success',
  templateUrl: './signup-success.component.html',
  styleUrls: ['./signup-success.component.scss'],
})
export class SignupSuccessComponent implements OnInit, OnDestroy {
  constructor(
    private readonly authService: AuthService,
    private readonly userService: UserService,
    private readonly logger: LoggerService,
    private readonly router: Router,
    private readonly matSnackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.continueToDashboard(true);
  }

  ngOnDestroy(): void {} // tslint:disable-line:no-empty

  resendVerificationEmail(): void {
    this.userService
      .resendVerificationEmail()
      .pipe(untilComponentDestroyed(this))
      .subscribe(
        () =>
          this.matSnackBar.open('A new verification e-mail has been sent.', '', {
            duration: 5000,
            panelClass: 'mat-success',
          }),
        err => {
          this.logger.error('An error occurred while re-sending the verification e-mail.', err);
          Sentry.captureException(err);
          this.matSnackBar.open('An error occurred while re-sending the verification e-mail to you. Our team has been warned.', '', {
            duration: 5000,
            panelClass: 'mat-error',
          });
        }
      );
  }

  continueToDashboard(init: boolean = false): void {
    this.authService
      .getUserInfo()
      .pipe(
        map(userInfo => userInfo.email_verified),
        map(isMailVerified => (isMailVerified == undefined ? true : isMailVerified)),
        untilComponentDestroyed(this)
      )
      .subscribe(isMailVerified => {
        if (isMailVerified) {
          this.router.navigate(['/']);
        } else if (!init) {
          this.matSnackBar.open('It appears that your e-mail address is still not validated.', '', {
            duration: 5000,
            panelClass: 'mat-warn',
          });
        }
      });
  }
}
