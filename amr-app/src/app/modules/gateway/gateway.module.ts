import { NgModule } from '@angular/core';
import { GatewayRoutingModule } from './gateway-routing.module';
import { GatewayListComponent } from './components/gateway-list/gateway-list.component';
import { SharedModule } from '../../shared/shared.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { GatewayService } from './services/gateway.service';
import { ReportModule } from 'src/app/modules/report/report.module';
import { DeviceVendorModule } from 'src/app/modules/device-vendor/device-vendor.module';
import { DeviceGroupModule } from 'src/app/modules/device-group/device-group.module';

@NgModule({
  imports: [
    GatewayRoutingModule,
    SharedModule.forRoot(),
    InfiniteScrollModule,
    ReportModule,
    DeviceVendorModule,
    DeviceGroupModule
  ],
  exports: [
    GatewayListComponent
  ],
  declarations: [
    GatewayListComponent
  ],
  providers: [
    GatewayService
  ]
})

export class GatewayModule {
}
