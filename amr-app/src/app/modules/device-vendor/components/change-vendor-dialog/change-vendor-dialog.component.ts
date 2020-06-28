import { Component, EventEmitter, Output, ViewChild, Inject } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DeviceVendorModel } from 'src/app/modules/device-vendor/models/device-vendor.model';
import { DeviceVendorService } from 'src/app/modules/device-vendor/services/device-vendor.service';

@Component({
  selector: 'app-change-vendor-dialog',
  templateUrl: './change-vendor-dialog.component.html'
})

export class ChangeVendorDialogComponent {

  entity: DeviceVendorModel;
  constructor(
    private deviceVendorService: DeviceVendorService,
    public dialogRef: MatDialogRef<ChangeVendorDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }


  hide(): void {
    this.dialogRef.close();
  }

  public onDeviceVendorChange(event: DeviceVendorModel) {
    this.deviceVendorService.deviceVendorModel = event;//Set seelcted Device vendor model in Device Vendor Service
  }
}
