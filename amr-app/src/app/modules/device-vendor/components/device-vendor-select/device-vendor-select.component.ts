import { Component, EventEmitter, Output, Input } from '@angular/core';
import { DeviceVendorService } from '../../services/device-vendor.service';
import { DeviceVendorModel } from '../../models/device-vendor.model';

@Component({
  selector: 'app-device-vendor-select',
  templateUrl: './device-vendor-select.component.html'
})

export class DeviceVendorSelectComponent {
  deviceVendorList: Array<DeviceVendorModel>;
  @Output() entityEmitter = new EventEmitter<DeviceVendorModel>();
  selected: DeviceVendorModel = new DeviceVendorModel();
  @Input('id') id: number;
  constructor(private deviceVendorService: DeviceVendorService) {
  }

  ngOnInit() {
    this.getAllLite();
  }

  private getAllLite() {
    this.deviceVendorService.getAllDeviceVendorLite().subscribe(response => {
      this.deviceVendorList = response;
      this.prepareSelectedModel();
    }, err => {
    });
  }

  private prepareSelectedModel() {
    if (this.id && this.id > 0 && this.deviceVendorList) {
	  this.selected.vendorId = this.id;
      this.selected.vendorName = this.deviceVendorList.find(x => x.vendorId == this.id).vendorName;
    }
  }

  onDeviceVendorChange() {
    this.entityEmitter.emit(this.selected);
  }
}