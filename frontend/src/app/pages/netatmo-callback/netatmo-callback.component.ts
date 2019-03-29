import { Component, OnDestroy, OnInit} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import * as Sentry from '@sentry/browser';
import { catchError, map, switchMap } from 'rxjs/operators';

import { NetatmoCallbackErrorDialogComponent } from '../../components/netatmo-callback-error-dialog/netatmo-callback-error-dialog.component';
import { untilComponentDestroyed } from '../../rxjs-operators/until-component-destroyed';
import { AuthService } from '../../services/auth.service';
import { LoggerService } from '../../services/logger.service';
import { NetatmoService } from '../../services/netatmo.service';

@Component({
  selector: 'app-netatmo-callback',
  templateUrl: './netatmo-callback.component.html',
  styleUrls: ['./netatmo-callback.component.scss'],
})
export class NetatmoCallbackComponent implements OnInit, OnDestroy {
  constructor(
    private readonly router: Router,
    private readonly activatedRoute: ActivatedRoute,
    private readonly netatmoService: NetatmoService,
    private readonly loggerService: LoggerService,
    private readonly authService: AuthService,
    private readonly matDialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.activatedRoute.queryParamMap
      .pipe(
        map(paramMap => [paramMap.get('state'), paramMap.get('code'), paramMap.get('error')]),
        switchMap(([state, code, error]) => this.netatmoService.exchangeCodeForAccessToken(state, code, error)),
        map(() => true),
        catchError((error: Error) => {
          this.loggerService.error('An error occurred while authorizing netatmo access', error.message);
          const eventId = Sentry.captureMessage(
            `An error occurred while authorizing netatmo access: ${error.message}`,
            Sentry.Severity.Error
          );
          return this.matDialog
            .open(NetatmoCallbackErrorDialogComponent, {
              closeOnNavigation: true,
              data: {
                eventId
              },
              disableClose: true,
              hasBackdrop: true,
              role: 'alertdialog',
              width: '400px',
            })
            .afterClosed();
        }),
        untilComponentDestroyed(this)
      )
      .subscribe(result => {
        if (result === true) {
          this.router.navigate(['/']);
        } else {
          this.authService.logout();
        }
      });
  }

  ngOnDestroy(): void {} // tslint:disable-line:no-empty
}
