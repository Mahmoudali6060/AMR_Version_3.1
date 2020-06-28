import { Component, EventEmitter, Output, ViewChild, Inject } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DeviceGroupModel } from 'src/app/modules/device-group/models/device-group.model';
import { DeviceGroupService } from 'src/app/modules/device-group/services/device-group.service';

@Component({
  selector: 'app-change-group-dialog',
  templateUrl: './change-group-dialog.component.html'
})

export class ChangeGroupDialogComponent {
  entity: DeviceGroupModel;
  constructor(
    private deviceGroupService: DeviceGroupService,
    public dialogRef: MatDialogRef<ChangeGroupDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {

  }


  hide(): void {
    this.dialogRef.close();
  }

  public onDeviceGroupChange(event: DeviceGroupModel) {
    this.deviceGroupService.deviceGroupModel = event;//Set seelcted Device group model in Device Group Service
  }
}
