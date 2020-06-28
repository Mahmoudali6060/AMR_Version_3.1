import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHelperService } from 'src/app/shared/services/http-heler.service';
import { DeviceGroupModel } from 'src/app/modules/device-group/models/device-group.model';

@Injectable()
export class DeviceGroupService {
  public deviceGroupModel: DeviceGroupModel;
  constructor(private http: HttpClient, private httpHelperService: HttpHelperService) { }

  getAllDeviceGroupLite(): Observable<any> {
    return this.http.get<any>(`${this.httpHelperService.baseUrl}api/DeviceGroup/GetDeviceGroupLite`);
  }

}