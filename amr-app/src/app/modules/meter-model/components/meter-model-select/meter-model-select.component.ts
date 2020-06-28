import { Component, EventEmitter, Output, Input } from '@angular/core';
import { MeterModelService } from '../../services/meter-model.service';
import { MeterModelModel } from '../../models/meter-model.model';

@Component({
  selector: 'app-meter-model-select',
  templateUrl: './meter-model-select.component.html'
})

export class MeterModelSelectComponent {
  meterModelList: Array<MeterModelModel>;
  @Output() entityEmitter = new EventEmitter<MeterModelModel>();
  selected: MeterModelModel = new MeterModelModel();
  @Input('id') id: number;
  constructor(private meterModelService: MeterModelService) {
  }

  ngOnInit() {
    this.getAllLite();
  }

  private getAllLite() {
    this.meterModelService.getAllMeterModelLite().subscribe(response => {
      this.meterModelList = response;
      this.prepareSelectedModel();
    }, err => {
    });
  }

  private prepareSelectedModel() {
    if (this.id && this.id > 0 && this.meterModelList) {
      this.selected.modelId = this.id;
      this.selected.modelName = this.meterModelList.find(x => x.modelId == this.id).modelName;
    }

  }

  onMeterModelChange() {
    this.entityEmitter.emit(this.selected);
  }
}