import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Event } from '@angular/router';
import { InsuranceCalculationModel } from 'src/Model/InsuranceCalculationModel';
import { InsuranceCalcServiceService } from 'src/services/insurance-calc-service.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  constructor(private apiService:InsuranceCalcServiceService){
    this.apiService.getOccupationList().subscribe(res=>{
      this.occupationList = res;
      console.log(res)
    },
    err=>console.log(err));
  }
  @ViewChild('calcForm', { static: true })
  public calcFrm!: NgForm;
  title = 'InsuranceUI';
  calculatedSum = '';
  occupationList:any;
  ngOnInit(): void {
  }
  ngAfterViewInit(){
    console.log(this.occupationList);
  }
  calcDetails:InsuranceCalculationModel={
    Name: '',
    Age:0,
    DOB: '',
    OccupationID: 0,
    DeathSumInsured: 0
  }
  onchange()
  {
   console.log(this.calcDetails.OccupationID);

  }
 calculate()
 {
  if(this.calcDetails.DOB === undefined || this.calcDetails.DOB === "")
    return;
  if(this.calcDetails.Age === undefined || this.calcDetails.Age === null)
    return;
  if(this.calcDetails.DeathSumInsured === undefined || this.calcDetails.DeathSumInsured === null)
    return;
  if(this.calcDetails.Name === undefined || this.calcDetails.Name === "" )
    return;
   var dob = this.calcDetails.DOB;
   this.calcDetails.DOB = new Date(this.calcDetails.DOB).toISOString()
   this.apiService.calculateInsurace(this.calcDetails).subscribe(res=>{
     console.log(res);
     this.calculatedSum = res.toString();
    }, err=>{console.log(err)}
  );
  this.calcDetails.DOB = dob;

 }
}
