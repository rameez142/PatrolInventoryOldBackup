import { Component, OnInit ,ViewChild} from '@angular/core';
import { CommonService } from '../../services/common.service';
import { DxDataGridComponent } from 'devextreme-angular'
import notify from 'devextreme/ui/notify';
import {devicecls} from './devicecls';
import ArrayStore from 'devextreme/data/array_store';
import SelectBox from 'devextreme/ui/select_box';
let datgrid2:any;

export class Company {
  title: string;
}
export class Product {
  ID: number;
  Name: string;
  Price: number;
  Current_Inventory: number;
  Backorder: number;
  Manufacturing: number;
  Category: string;
  ImageSrc: string;
}
@Component({
  selector: 'app-devices',
  templateUrl: './devices.component.html',
  styleUrls: ['./devices.component.css']
})



export class DevicesComponent implements OnInit {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  data: any;

  loadingVisible = false;
  selahwalid:number = 1;
  rentalchk:number = 0;
  defectchk:number = 0;
  typesrc:any;
  dataSource: any;
  devicetypesrc:any;
  public deviceobj:devicecls = new devicecls();
  companies: Company[];
  //datgrid2 = new DxDataGridComponent(dataGrid);
  datgrid2 = this.dataGrid;
   products: Product[] = [
    {
      'ID': 3,
      'Name': '',
      'Price': 330,
      'Current_Inventory': 225,
      'Backorder': 0,
      'Manufacturing': 10,
      'Category': 'Video Players',
      'ImageSrc': 'images/products/1-small.png'
  },{
    'ID': 1,
    'Name': 'PAT23545',
    'Price': 330,
    'Current_Inventory': 225,
    'Backorder': 0,
    'Manufacturing': 10,
    'Category': 'Video Players',
    'ImageSrc': 'images/products/1-small.png'
}, {
    'ID': 2,
    'Name': 'PAT456',
    'Price': 400,
    'Current_Inventory': 150,
    'Backorder': 0,
    'Manufacturing': 25,
    'Category': 'Video Players',
    'ImageSrc': 'images/products/2-small.png'
}];
selectBox:any;

  constructor(private svc:CommonService) {
  //   this.data = new ArrayStore({
  //     data: this.products,
  //     key: 'ID'
  // });
    this.showLoadPanel();
   // this.typesrc = JSON.parse(window.localStorage.getItem('devicetypes'));

  }
  onValueChangeOfSelectbox(e) {
//console.log(e.name);


    if (e.name === 'selectedItem' ) {
      console.log(e.value.Name);
     // console.log(e);
     // console.log(e.component);
      //console.log(this.dataGrid);
     // this.LoadData();
     // console.log(this);
//this.LoadData();
if (e.value.Name == "") {
  this.dataGrid.instance.clearFilter();
} else {
  this.dataGrid.instance.filter(['devicenumber', 'contains', e.value.Name]);
}
     // this.dataGrid.instance.filter(['devicenumber', '=', e.value.Name]);
      
    // this.dataGrid.instance.clearFilter();
    }

  }

  ContentReady2(e) {
    if (!e.component.__isInitialized) {
        e.component.__isInitialized = true;
        // this.selectBox = new SelectBox(e.element.querySelector('.dx-datagrid-filter-row .dx-command-select'),
        //  {
        //   dataSource: this.products,
        //   valueExpr: 'ID',
        //   displayExpr: 'name',
        //   onOptionChanged: this.onValueChangeOfSelectbox
        // });
    }
  }
  Cellprepare(e)
  {
    if (e.rowType === 'filter') {
     /// alert(e.columnIndex);
      if(e.columnIndex === 0)
      {
     //  alert(e.columnIndex);

       this.selectBox = new SelectBox(e.cellElement,
         {
          dataSource: this.products,
          valueExpr: 'Name',
          displayExpr: 'Name',
          onOptionChanged:this.onValueChangeOfSelectbox.bind(this)
          ,

        });
       // e.cellElement.appendChild(this.selectBox);
      }
      }
    }

   onShown() {
    setTimeout(() => {
        this.loadingVisible = false;
    }, 3000);
  }
  typeselboxtoggled(e)
  {

  }
  showLoadPanel() {
    this.loadingVisible = true;
  }

  ngOnInit() {

    this.LoadData();
  }

LoadData()
{
  this.svc.GetDevicesList().subscribe(resp =>
    {

       this.dataSource = JSON.parse(resp);
      console.log('resp' + resp);
      this.dataGrid.dataSource = this.dataSource;
      this.dataGrid.instance.refresh();

  },
    error => {

    });

    /*this.svc.GetDeviceTypesList().subscribe(resp =>
      {

         this.devicetypesrc = JSON.parse(resp);


    },
      error => {

      });*/

}
onToolbarPreparing(e) {
  e.toolbarOptions.items.unshift({
      location: 'before',
      template: 'Organization'
  }, {
          location: 'before',
          widget: 'dxSelectBox',
          options: {
              width: 200,
              items: [{
                  value: 'Org1',
                  text: 'Grouping by Org1'
              }, {
                  value: 'Org2',
                  text: 'Grouping by Org2'
              }],
              displayExpr: 'text',
              valueExpr: 'value',
              value: 'CustomerStoreState',
              onValueChanged: this.groupChanged.bind(this)
          }
      }, {
          location: 'after',
          widget: 'dxButton',
          options: {
              icon: 'refresh',
              onClick: this.refreshDataGrid.bind(this)
          }
      });
}

groupChanged(e) {
  this.dataGrid.instance.clearGrouping();
  this.dataGrid.instance.columnOption(e.value, 'groupIndex', 0);
}



refreshDataGrid() {
  this.LoadData();
  //this.dataGrid.instance.refresh();
}

cleardata()
{
  this.deviceobj.ahwalid =  -1;
  this.deviceobj.barcode = '';
  this.deviceobj.defective =  -1;
  this.deviceobj.devicenumber =  '';
  this.deviceobj.devicetypeid =  -1;
  this.deviceobj.model =  '';
  this.deviceobj.rental = -1;
  this.deviceobj.deviceid =  -1;
}

PopupInitialize(e)
{
  console.log('popupini');
  this.cleardata();
  this.cleardefaultvalues();
}
cleardefaultvalues()
{
  this.rentalchk = 0;
  this.defectchk = 0;
}
RowAdd(e)
{
  console.log(this.rentalchk);
  this.cleardata();
  this.deviceobj.ahwalid =  this.selahwalid;
  this.deviceobj.barcode =  e.data.barcode;
  this.deviceobj.defective =  this.defectchk;
  this.deviceobj.devicenumber =  e.data.devicenumber;
  this.deviceobj.devicetypeid =  1;
  this.deviceobj.model =  e.data.model;
  this.deviceobj.rental = this.rentalchk;

  this.svc.AddDevices(this.deviceobj).subscribe(resp =>
    {
      console.log('resp' + resp);
     this.LoadData();
  },
    error => {

    });
    this.cleardata();
    this.cleardefaultvalues();

  notify(' Record Added SuccessFully', 'success', 600);
}

checkBoxToggled(e)
{
  //console.log(e.value);
  if( e.value === true)
  {
    this.rentalchk = 1;
  }
  else{
    this.rentalchk = 0;
  }

}

chkdeftoggle(e)
{
  //console.log(e.value);
  if( e.value === true)
  {
    this.defectchk = 1;
  }
  else{
    this.defectchk = 0;
  }

}

RowUpdate(e)
{

  this.svc.UpdateDevices(this.deviceobj).subscribe(resp =>
    {

      console.log('resp' + resp);
     this.LoadData();
  },
    error => {

    });
}

RowDelete(e)
{
  this.cleardata();
  this.deviceobj.ahwalid =  this.selahwalid;
  this.deviceobj.barcode =  e.data.barcode;
  this.deviceobj.defective =  e.data.defective;
  this.deviceobj.devicenumber =  e.data.devicenumber;
  this.deviceobj.model =  e.data.model;
  this.deviceobj.rental = e.data.rental;
  this.deviceobj.deviceid =  e.data.deviceid;
  console.log(e);
  this.svc.DeleteDevices(this.deviceobj).subscribe(resp =>
    {

      console.log('resp' + resp);
     this.LoadData();
  },
    error => {

    });
}
selectStatus(data) {
  if (data.value === 'All') {
      this.dataGrid.instance.clearFilter();
  } else {
      this.dataGrid.instance.filter(['rental', '=', data.value]);
  }
}
}
