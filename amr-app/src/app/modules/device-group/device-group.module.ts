import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { DeviceGroupService } from './services/device-group.service';
import { ReportModule } from 'src/app/modules/report/report.module';
import { DeviceGroupRoutingModule } from './device-group-routing.module';
import { DeviceGroupSelectComponent } from 'src/app/modules/device-group/components/device-group-select/device-group-select.component';
import { ChangeGroupDialogComponent } from 'src/app/modules/device-group/components/change-group-dialog/change-group-dialog.component';

@NgModule({
  imports: [
    DeviceGroupRoutingModule,
    SharedModule.forRoot(),
    InfiniteScrollModule,
    ReportModule
  ],
  exports: [
    DeviceGroupSelectComponent,
    ChangeGroupDialogComponent
  ],
  declarations: [
    DeviceGroupSelectComponent,
    ChangeGroupDialogComponent
  ],
  entryComponents: [
    ChangeGroupDialogComponent
  ],
  providers: [
    DeviceGroupService
  ]
})

export class DeviceGroupModule {
}
