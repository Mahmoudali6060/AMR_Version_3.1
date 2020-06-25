import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { DeviceVendorService } from './services/device-vendor.service';
import { ReportModule } from 'src/app/modules/report/report.module';
import { DeviceVendorRoutingModule } from './device-vendor-routing.module';
import { DeviceVendorSelectComponent } from 'src/app/modules/device-vendor/components/device-vendor-select/device-vendor-select.component';

@NgModule({
  imports: [
    DeviceVendorRoutingModule,
    SharedModule.forRoot(),
    InfiniteScrollModule,
    ReportModule
  ],
  exports: [
    DeviceVendorSelectComponent
  ],
  declarations: [
    DeviceVendorSelectComponent
  ],
  providers: [
    DeviceVendorService
  ]
})

export class DeviceVendorModule {
}
