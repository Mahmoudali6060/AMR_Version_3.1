import { Component, EventEmitter, Output, ViewChild, Inject } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MeterModelModel } from 'src/app/modules/meter-model/models/meter-model.model';
import { MeterModelService } from 'src/app/modules/meter-model/services/meter-model.service';

@Component({
  selector: 'app-change-meter-model-dialog',
  templateUrl: './change-meter-model-dialog.component.html'
})

export class ChangeMeterModelDialogComponent {
  entity: MeterModelModel;
  constructor(
    private meterModelService: MeterModelService,
    public dialogRef: MatDialogRef<ChangeMeterModelDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {

  }


  hide(): void {
    this.dialogRef.close();
  }

  public onMeterModelChange(event: MeterModelModel) {
    this.meterModelService.meterModelModel = event;//Set seelcted Device meter model model in Device MeterModel Service
  }
}
