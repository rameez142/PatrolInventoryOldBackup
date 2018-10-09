import { Component,OnInit } from '@angular/core';
import { CommonService } from '../services/common.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(private svc:CommonService) {
   }

  ngOnInit() {

   /* this.svc.GetOrganizationList().subscribe(resp =>
      {
         console.log("Home" + resp)
         window.localStorage.setItem("Orgs",resp);
    },
      error => {
      });

      this.svc.GetDeviceTypes().subscribe(resp =>
        {
           console.log("Home" + resp)
           window.localStorage.setItem("devicetypes",resp);
      },
        error => {
        });*/
  }

}
