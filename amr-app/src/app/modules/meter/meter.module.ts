import { NgModule } from '@angular/core';
import { MeterRoutingModule } from './meter-routing.module';
import { MeterListComponent } from './components/meter-list/meter-list.component';
import { SharedModule } from '../../shared/shared.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { MeterService } from './services/meter.service';
import { ReportModule } from 'src/app/modules/report/report.module';
import { DeviceVendorModule } from 'src/app/modules/device-vendor/device-vendor.module';
import { DeviceGroupModule } from 'src/app/modules/device-group/device-group.module';

@NgModule({
  imports: [
    MeterRoutingModule,
    SharedModule.forRoot(),
    InfiniteScrollModule,
    ReportModule,
    DeviceVendorModule,
    DeviceGroupModule
    
  ],
  exports: [
    MeterListComponent
  ],
  declarations: [
    MeterListComponent
  ],
  providers: [
    MeterService
  ]
})

export class MeterModule {
}
