import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { iif, of } from 'rxjs';
import { filter } from 'rxjs/internal/operators/filter';
import { switchMap } from 'rxjs/internal/operators/switchMap';
import { tap } from 'rxjs/internal/operators/tap';
import { mergeMap } from 'rxjs/operators';

import { AuthorizeDialogComponent } from '../../components/authorize-dialog/authorize-dialog.component';
import { NETATMO_STATE } from '../../models/consts';
import { untilComponentDestroyed } from '../../rxjs-operators/until-component-destroyed';
import { AuthService } from '../../services/auth.service';
import { isAuthorizeUrl, NetatmoService } from '../../services/netatmo.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit, OnDestroy {
  constructor(
    private readonly userService: UserService,
    private readonly netatmoService: NetatmoService,
    private readonly matDialog: MatDialog,
    private readonly authService: AuthService
  ) {}

  ngOnInit(): void {
    this.userService
      .ensureCreated()
      .pipe(
        mergeMap(result =>
          iif(
            () => result,
            of(result),
            this.netatmoService.buildAuthorizeUrl()
          )
        ),
        filter(isAuthorizeUrl),
        tap(authorizeUrl => sessionStorage.setItem(NETATMO_STATE, authorizeUrl.state)),
        switchMap(authorizeUrl =>
          this.matDialog
            .open(AuthorizeDialogComponent, {
              closeOnNavigation: true,
              data: authorizeUrl.url,
              disableClose: true,
              hasBackdrop: true,
              role: 'alertdialog',
              width: '400px',
            })
            .afterClosed()
        ),
        untilComponentDestroyed(this)
      )
      .subscribe(result => {
        if (!result) {
          this.authService.logout();
        }
      });
  }

  ngOnDestroy(): void {} // tslint:disable-line:no-empty
}
