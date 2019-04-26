import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { iif, of } from 'rxjs';
import { mergeMap, switchMap, tap } from 'rxjs/operators';

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
  updateJobEnabled: boolean = false;

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
            this.userService.getProfile(),
            this.netatmoService.buildAuthorizeUrl()
          )
        ),
        mergeMap(result =>
          iif(
            () => isAuthorizeUrl(result),
            of(result).pipe(
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
              )
            ),
            of(result)
          )
        ),
        /*filter(isAuthorizeUrl),
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
        ),*/
        untilComponentDestroyed(this)
      )
      .subscribe(result => {
        if (!result) {
          this.authService.logout();
        } else {
          this.updateJobEnabled = result.enabled;
        }
      });
  }

  ngOnDestroy(): void {} // tslint:disable-line:no-empty

  toggleUpdateJob(): void {
    this.userService.toggleFetchAndUpdateJob()
      .pipe(untilComponentDestroyed(this))
      .subscribe(enabled => (this.updateJobEnabled = enabled));
  }
}
