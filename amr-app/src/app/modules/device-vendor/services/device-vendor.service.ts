import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHelperService } from 'src/app/shared/services/http-heler.service';

@Injectable()
export class DeviceVendorService {
  constructor(private http: HttpClient, private httpHelperService: HttpHelperService) { }

  getAllDeviceVendorLite(): Observable<any> {
    return this.http.get<any>(`${this.httpHelperService.baseUrl}api/DeviceVendor/GetAllDeviceVendorLite`);
  }

}