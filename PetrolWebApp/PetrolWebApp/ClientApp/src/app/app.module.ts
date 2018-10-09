import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { DevicesComponent } from './maintainence/devices/devices.component';
import { DxSelectBoxModule,DxPopupModule,DxDataGridModule,DxButtonModule ,DxTemplateModule,DxLoadIndicatorModule,DxLoadPanelModule, DxTabPanelModule,DxCheckBoxModule } from "devextreme-angular";
import { CommonService } from './services/common.service';
import { DeviceinventoryComponent } from './maintainence/deviceinventory/deviceinventory.component';
import { DispatchComponent } from './dispatcher/dispatch/dispatch.component';
import { PatrolcarsComponent } from './maintainence/patrolcars/patrolcars.component';
import { HandheldsComponent } from './maintainence/handhelds/handhelds.component';
import { AccessoriesComponent } from './maintainence/accessories/accessories.component';
//import {TabModule} from 'angular-tabs-component';
import { HandheldinventoryComponent } from './maintainence/deviceinventory/handheldinventory/handheldinventory.component';
import { AccessoryinventoryComponent } from './maintainence/deviceinventory/accessoryinventory/accessoryinventory.component';
import { EmployeesComponent } from './dispatcher/employees/employees.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    DevicesComponent,
    DeviceinventoryComponent,
    DispatchComponent,
    DispatchComponent,
    PatrolcarsComponent,
    HandheldsComponent,
    AccessoriesComponent,
    HandheldinventoryComponent,
    AccessoryinventoryComponent,
    EmployeesComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,DxSelectBoxModule,
    DxPopupModule,DxDataGridModule,DxButtonModule ,DxTemplateModule,DxLoadIndicatorModule,DxLoadPanelModule, DxTabPanelModule,DxCheckBoxModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'devices', component: DevicesComponent },
      { path: 'deviceinventory', component: DeviceinventoryComponent },
      { path: 'hadheld', component: HandheldsComponent },
      { path: 'accessory', component: AccessoriesComponent },
      { path: 'handheldinvent', component: HandheldinventoryComponent },
      { path: 'accessoryinvent', component: AccessoryinventoryComponent },
      { path: 'employee', component: EmployeesComponent },

      {path: 'dispatch', component: DispatchComponent}
    ])
  ],
  providers: [CommonService],
  bootstrap: [AppComponent]
})
export class AppModule { }
