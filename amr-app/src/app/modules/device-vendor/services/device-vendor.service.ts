import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHelperService } from 'src/app/shared/services/http-heler.service';
import { DeviceVendorModel } from 'src/app/modules/device-vendor/models/device-vendor.model';

@Injectable()
export class DeviceVendorService {
  deviceVendorModel: DeviceVendorModel;
  constructor(private http: HttpClient, private httpHelperService: HttpHelperService) { }

  getAllDeviceVendorLite(): Observable<any> {
    return this.http.get<any>(`${this.httpHelperService.baseUrl}api/DeviceVendor/GetAllDeviceVendorLite`);
  }

  getDeviceVendorDetailsByIdAsync(id): Observable<any> {
    return this.http.get<any>(`${this.httpHelperService.baseUrl}Api/DeviceVendor/GetDeviceVendorDetailsByIdAsync/${id}`);
  }
}