import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { MeterModelService } from './services/meter-model.service';
import { ReportModule } from 'src/app/modules/report/report.module';
import { MeterModelRoutingModule } from './meter-model-routing.module';
import { MeterModelSelectComponent } from 'src/app/modules/meter-model/components/meter-model-select/meter-model-select.component';
import { ChangeMeterModelDialogComponent } from './components/change-meter-model-dialog/change-meter-model-dialog.component';

@NgModule({
  imports: [
    MeterModelRoutingModule,
    SharedModule.forRoot(),
    InfiniteScrollModule,
    ReportModule
  ],
  exports: [
    MeterModelSelectComponent,
    ChangeMeterModelDialogComponent
  ],
  declarations: [
    MeterModelSelectComponent,
    ChangeMeterModelDialogComponent
  ],
  entryComponents: [
    ChangeMeterModelDialogComponent
  ],
  providers: [
    MeterModelService
  ]
})

export class MeterModelModule {
}
