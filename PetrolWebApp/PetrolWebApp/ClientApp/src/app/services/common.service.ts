
import { Injectable , Output,EventEmitter} from '@angular/core';
import {HttpClient,HttpHeaders,HttpErrorResponse} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, retry, map, tap } from 'rxjs/operators';
import {devicecls} from '../maintainence/devices/devicecls';
@Injectable()


export class CommonService {
  private api_url: any = 'http://localhost:56974';

  constructor(private http: HttpClient) { }
  public GetDevicesList(){
    return this.http.post(this.api_url + "/api/maintainence/deviceslist", null, { responseType: 'text' })
    }

    public AddDevices(frm:devicecls){
      return this.http.post(this.api_url + "/api/maintainence/adddevices", frm, { responseType: 'text' })
      }

      public UpdateDevices(frm:devicecls){
        return this.http.post(this.api_url + "/api/maintainence/updatedevices", frm, { responseType: 'text' })
        }

        public DeleteDevices(frm:devicecls){
          console.log(frm);
          return this.http.post(this.api_url + "/api/maintainence/deldevices", frm, { responseType: 'text' })
          }

//#region Hand Held
public GethandheldsList(){
  return this.http.post(this.api_url + "/api/maintainence/handheldslist", null, { responseType: 'text' })
  }
public Addhandhelds(frm:devicecls){
  return this.http.post(this.api_url + "/api/maintainence/addhandhelds", frm, { responseType: 'text' })
  }

  public Updatehandhelds(frm:devicecls){
    return this.http.post(this.api_url + "/api/maintainence/updatehandhelds", frm, { responseType: 'text' })
    }

    public Deletehandhelds(frm:devicecls){
      console.log(frm);
      return this.http.post(this.api_url + "/api/maintainence/delhandhelds", frm, { responseType: 'text' })
      }

      //#region Accessory
public GetaccessoryList(){
  return this.http.post(this.api_url + "/api/maintainence/accessorylist", null, { responseType: 'text' })
  }
public Addaccessory(frm:devicecls){
  return this.http.post(this.api_url + "/api/maintainence/addaccessory", frm, { responseType: 'text' })
  }

  public Updateaccessory(frm:devicecls){
    return this.http.post(this.api_url + "/api/maintainence/updateaccessory", frm, { responseType: 'text' })
    }

    public Deleteaccessory(frm:devicecls){
      console.log(frm);
      return this.http.post(this.api_url + "/api/maintainence/delaccessory", frm, { responseType: 'text' })
      }


         //#region Persons
public GetpersonList(){
  return this.http.post(this.api_url + "/api/maintainence/personslist", null, { responseType: 'text' })
  }
public Addpersons(frm:devicecls){
  return this.http.post(this.api_url + "/api/maintainence/addpersons", frm, { responseType: 'text' })
  }

  public Updatepersons(frm:devicecls){
    return this.http.post(this.api_url + "/api/maintainence/updatepersons", frm, { responseType: 'text' })
    }

    public Deletepersons(frm:devicecls){
      console.log(frm);
      return this.http.post(this.api_url + "/api/maintainence/delpersons", frm, { responseType: 'text' })
      }


        public GetDeviceTypesList(){

          return this.http.post(this.api_url + "/api/maintainence/devicetypeslist", null, { responseType: 'text' })
          }

          public GetOrganizationList(){

            return this.http.post(this.api_url + "/api/maintainence/organizationlist", null, { responseType: 'text' })
            }


    public GetDevicesInventoryList(){
      return this.http.post(this.api_url + "/api/maintainence/devicesinventory", null, { responseType: 'text' })
      }

      public GetHandHeldsInventoryList(){
        return this.http.post(this.api_url + "/api/maintainence/handheldsinventory", null, { responseType: 'text' })
        }

        public GetAccessoryInventoryList(){
          return this.http.post(this.api_url + "/api/maintainence/accessoryinventory", null, { responseType: 'text' })
          }

      public GetDispatchList(){
        return this.http.post(this.api_url + "/api/maintainence/dispatchlist", null, { responseType: 'text' })
        }

        public GetDeviceTypes(){
          return this.http.post(this.api_url + "/api/maintainence/devicetypes", null, { responseType: 'text' })
          }
}
