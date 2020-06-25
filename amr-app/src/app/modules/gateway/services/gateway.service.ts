import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GatewayModel } from '../models/gateway.model';
import { Observable } from 'rxjs';
import { HttpHelperService } from 'src/app/shared/services/http-heler.service';
import { DataSourceModel } from 'src/app/shared/models/data-source.model';

@Injectable()
export class GatewayService {
  constructor(private http: HttpClient, private httpHelperService: HttpHelperService) { }

  getAllGatewaysAsync(dataSource: DataSourceModel,gatewayCode: string, vendorId: number, groupId: number): Observable<any> {
    return this.http.get<any>(`${this.httpHelperService.baseUrl}api/Gateway/GetAllGatewaysAsync?pageSize=${dataSource.PageSize}&&currentPage=${dataSource.CurrentPage}&&gatewayCode=${gatewayCode}&&vendorId=${vendorId}&&groupId=${groupId}`);
  }

  delete(gatewayId: string): Observable<any> {
    return this.http.get<any>(`${this.httpHelperService.baseUrl}api/Gateway/Delete?gatewayId=${gatewayId}`);
  }

}