import { Component } from '@angular/core'
import { FormGroup } from '@angular/forms'
import { Paciente } from './paciente'
import { PacientesService } from '../../services/pacientes.service'
import { HttpClientModule } from '@angular/common/http';

@Component({
    selector: 'pacientes',
    templateUrl: './paciente.component.html',
    providers: [ PacientesService, HttpClientModule]  
})

export class PacienteComponent {
    public pacienteForm: FormGroup;
    public paciente : Paciente;
    public pacientes : Array<Paciente>;
    public error : string;
    public loading : boolean;
    display='none';
    displayModal2='none';
    constructor(private _pacientesService : PacientesService){         
    }
    title = 'Pacientes';

    ngOnInit(){
        this.loadGrid();
        this.initLoad();
    }

    loadGrid(){
      this._pacientesService.getList().subscribe(
        result =>{ this.pacientes = result },
        error => { console.log(error); }
      );
    }

    private initLoad(){
      this.loading = true;
      this.error = "";
      this.paciente =  new Paciente("","","","","","");
    }

    private clearVar(){
      this.loading = true;
      this.error = "";
    }

    delete(){
      this.clearVar();
      this._pacientesService.delete(this.paciente).subscribe(
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

    deleteForm(paciente:Paciente)
    {
      this.clearVar();
      this.paciente = paciente;
      this.openModal();
    }
  }
