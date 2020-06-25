import { Component, EventEmitter, Output, Input } from '@angular/core';
import { DeviceGroupService } from '../../services/device-group.service';
import { DeviceGroupModel } from '../../models/device-group.model';

@Component({
  selector: 'app-device-group-select',
  templateUrl: './device-group-select.component.html'
})

export class DeviceGroupSelectComponent {
  deviceGroupList: Array<DeviceGroupModel>;
  @Output() entityEmitter = new EventEmitter<DeviceGroupModel>();
  selected: DeviceGroupModel = new DeviceGroupModel();
  @Input() id: number;
  constructor(private deviceGroupService: DeviceGroupService) {
  }

  ngOnInit() {
    this.getAllLite();
  }

  private getAllLite() {
    this.deviceGroupService.getAllDeviceGroupLite().subscribe(response => {
      this.deviceGroupList = response;
      this.prepareSelectedModel();
    }, err => {
    });
  }

  private prepareSelectedModel() {
    if (this.id && this.id > 0 && this.deviceGroupList) {
	  this.selected.groupId = this.id;
	  //
      this.selected.groupName = this.deviceGroupList.find(x => x.groupId == this.id).groupName;
    }

  }

  onDeviceGroupChange() {
    this.entityEmitter.emit(this.selected);
  }
}