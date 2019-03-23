import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-netatmo-callback-error-dialog',
  templateUrl: './netatmo-callback-error-dialog.component.html',
  styleUrls: ['./netatmo-callback-error-dialog.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NetatmoCallbackErrorDialogComponent {
  readonly body: string;

  constructor(
    private readonly dialogRef: MatDialogRef<NetatmoCallbackErrorDialogComponent>,
    @Inject(MAT_DIALOG_DATA) readonly data: { message: string; error: any }
  ) {
    this.body = encodeURIComponent(`The error message is: ${this.data.message}.\nThe error is: ${JSON.stringify(data.error)}`);
  }

  close(): void {
    this.dialogRef.close(false);
  }
}
