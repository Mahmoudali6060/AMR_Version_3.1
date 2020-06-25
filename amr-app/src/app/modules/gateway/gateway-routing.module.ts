import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GatewayListComponent } from './components/gateway-list/gateway-list.component';

const routes: Routes = [
  { path: '', component: GatewayListComponent },
  { path: 'gateway-list', component: GatewayListComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GatewayRoutingModule {
}
