import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHelperService } from 'src/app/shared/services/http-heler.service';
import { MeterModelModel } from 'src/app/modules/meter-model/models/meter-model.model';

@Injectable()
export class MeterModelService {
  public meterModelModel: MeterModelModel;
  constructor(private http: HttpClient, private httpHelperService: HttpHelperService) { }

  getAllMeterModelLite(): Observable<any> {
    return this.http.get<any>(`${this.httpHelperService.baseUrl}api/MeterModel/GetAllMeterModelLite`);
  }

}