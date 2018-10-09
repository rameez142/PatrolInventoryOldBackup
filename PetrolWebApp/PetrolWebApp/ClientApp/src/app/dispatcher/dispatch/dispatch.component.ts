import { Component, OnInit ,ViewChild} from '@angular/core';
import { CommonService } from '../../services/common.service';
import { DxDataGridComponent } from "devextreme-angular"
import notify from 'devextreme/ui/notify';

@Component({
  selector: 'app-dispatch',
  templateUrl: './dispatch.component.html',
  styleUrls: ['./dispatch.component.css']
})
export class DispatchComponent implements OnInit {

  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  loadingVisible = false;

  constructor(private svc:CommonService) {
    this.showLoadPanel();
   }
   onShown() {
    setTimeout(() => {
        this.loadingVisible = false;
    }, 3000);
  }
  
  showLoadPanel() {
    this.loadingVisible = true;
  }
  dataSource: any;
 
  ngOnInit() {

    this.LoadData();
  }

LoadData()
{
  this.svc.GetDispatchList().subscribe(resp => 
    {
       
       this.dataSource = JSON.parse(resp);
      console.log('resp' + resp);
      this.dataGrid.dataSource = this.dataSource;
      this.dataGrid.instance.refresh();

  },
    error => {
       
    });
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
                  text: ' الصناعية'
              }, {
                  value: 'Org2',
                  text: ' شمال'
              }],
              displayExpr: 'text',
              valueExpr: 'value',
              value: 'CustomerStoreState',
              onValueChanged: this.groupChanged.bind(this)
          }
      },{
        location: 'before',
        template: 'Shifts'
    },{
        location: 'before',
        widget: 'dxSelectBox',
        options: {
            width: 200,
            items: [{
                value: 'Shift1',
                text: 'صباح'
            }, {
                value: 'Shift2',
                text: 'عصر'
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
  this.dataGrid.instance.refresh();
}


}
