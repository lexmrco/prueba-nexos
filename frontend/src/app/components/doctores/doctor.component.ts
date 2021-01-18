import { Component } from '@angular/core'
import { FormGroup, NgForm } from '@angular/forms'
import { Doctor, Paciente } from './doctor'
import { DoctoresService } from '../../services/doctores.service'
import { HttpClientModule } from '@angular/common/http';

@Component({
    selector: 'doctores',
    templateUrl: './doctor.component.html',
    providers: [ DoctoresService, HttpClientModule]  
})

export class DoctorComponent {
    public doctorForm: FormGroup;
    public doctor : Doctor;
    public doctores : Array<Doctor>;
    public doctorsSelectList : Array<Doctor>;
    public error : string;
    public loading : boolean;
    display='none';
    displayModal2='none';
    constructor(private _doctoresService : DoctoresService){         
    }
    title = 'Doctores';

    ngOnInit(){
        this.loadGrid();
        this.initLoad();
    }

    loadGrid(){
      this._doctoresService.getList().subscribe(
        result =>{ this.doctores = result },
        error => { console.log(error); }
      );
    }

    private initLoad(){
      this.loading = true;
      this.error = "";
      this.doctor =  new Doctor("","","","","","");
    }

    private clearVar(){
      this.loading = true;
      this.error = "";
    }

    delete(){
      this.clearVar();
      this._doctoresService.delete(this.doctor).subscribe(
        () =>{ 
          this.onCloseModal2();
          this.loadGrid();
        },
        error => { console.log(error); }
      );
    }

    openModal(){
      this.displayModal2 = 'block';
    }

    onCloseHandled(){
      this.clearVar();
      this.display='none';
    }

    onCloseModal2(){
      this.clearVar();
      this.displayModal2='none';
    }

    deleteForm(doctor:Doctor)
    {
      this.clearVar();
      this.doctor = doctor;
      this.openModal();
    }
  }
