import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { InsuranceCalculationModel } from 'src/Model/InsuranceCalculationModel';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class InsuranceCalcServiceService {

  baseUrl = 'https://localhost:7227/';
  constructor(protected http: HttpClient) { }

  calculateInsurace(calculationInput:InsuranceCalculationModel)
  {
    let url = this.baseUrl+'calculateInsurance';
    console.log(calculationInput);
    return this.http.post(url,calculationInput);
  }
  getOccupationList():Observable<any>{
    let url = this.baseUrl+'getOccupations';
    return this.http.get(url);
  }
}
