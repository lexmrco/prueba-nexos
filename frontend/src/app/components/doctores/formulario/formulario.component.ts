import { Component } from '@angular/core'
import { FormGroup, NgForm } from '@angular/forms'
import { Paciente, Doctor } from '../doctor'
import { PacientesService } from '../../../services/pacientes.service'
import { DoctoresService } from '../../../services/doctores.service'
import { HttpClientModule } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
    selector: 'doctores-formulario',
    templateUrl: './formulario.component.html',
    providers: [ PacientesService, DoctoresService, HttpClientModule]  
})

export class FormularioDoctorComponent {
  public crudAction : string;
  public doctor : Doctor;
  
  public selectPacienteId : string;
  public pacientes : Array<Paciente>;
  public selectedPaciente : Paciente;
  public pacientesSelectList : Array<Paciente>;
  
  public error : string;
  public modalError : string;
  public loading : boolean;
  display='none';
  displayModal2='none';
  constructor(private _pacientesService : PacientesService,
    private _doctoresService : DoctoresService,
    private route: ActivatedRoute,
    private router: Router){     
  }
  title = 'Formulario Doctores';

  ngOnInit(){
      const doctorId = this.route.snapshot.paramMap.get("id");
      this.initLoad();
      if(doctorId != null){
        // Obtener todos los campos
        this.getDoctor(doctorId);
        // Obtener los datos de los médicos asociados
        this.getPacientes(doctorId);
        // Obtener la lista completa de doctores por asociar
        this.getDoctoresSelectList();
        // Indicar que se está actualizando
        this.crudAction = "update";
      } else {
        this.crudAction = "create";
        this.doctor =  new Doctor("","","","","","");
      }
  }

  getDoctor(id: string){
    this._doctoresService.getDoctor(id).subscribe(
      result =>{ this.doctor = result },
      error => { this.error = 'Error obteniendo los datos del doctor seleccionado';
      ; }
    );
  }

  getPacientes(doctorId: string){
    this._doctoresService.getPacientes(doctorId).subscribe(
      result =>{ this.pacientes = result; },
      error => { console.log(error); }
    );
  }

  getDoctoresSelectList(){
    this._doctoresService.getSelectList().subscribe(
      result =>{ this.pacientesSelectList = result },
      error => { console.log(error); }
    );
  }

  private initLoad(){
    this.loading = true;
    this.error = "";
    this.selectedPaciente = new Paciente("","","","");
    this.doctor =  new Doctor("","","","","","");
  }

  private clearVar(){
    this.loading = true;
    this.error = "";
  }

  onSubmit(form: NgForm){
    if (form.valid) {
      if(this.crudAction == "create")
        this.create();
      else if(this.crudAction == "update")
        this.update();
    }
  }

  onSubmitPaciente(form: NgForm){
    if (form.valid) {
      this._pacientesService.addDoctor( this.selectPacienteId,this.doctor.id).subscribe(
        data =>{ 
          this.onCloseHandled();
          this.selectPacienteId = null;
          this.getPacientes(this.doctor.id);
        },
        error => { 
          console.log(error); 
          if(error.status == '400'){
            this.error = 'Error asociando el doctor al doctor, revise si están correctamente diligenciados';
          }
          else{
            this.error = 'Error en el servidor, por favor intente más tarde';
          }
        }
      );
    }
  }

  private create(){
    this._doctoresService.post(this.doctor).subscribe(
      data =>{ 
        this.goToIndex();
      },
      error => { 
        console.log(error); 
        if(error.status == '400'){
          this.error = 'Error guardando los datos, revise si están correctamente diligenciados';
        }
        else{
          this.error = 'Error en el servidor, por favor intente más tarde';
        }
      }
    );
  }

  private update(){
    this._doctoresService.put(this.doctor).subscribe(
      () =>{ 
        this.goToIndex();
      },
      error => { console.log(error); }
    );
  }

  openModal(){
    this.display='block';
  }

  openModalConfirm(){
    this.displayModal2='block';
  }

  onCloseHandled(){
    this.clearVar();
    this.display='none';
  }

  onCancel(){
    this.goToIndex();
  }

  goToIndex(){
    this.router.navigate(['/doctores']);
  }

  onCloseModal2(){
    this.clearVar();
    this.displayModal2='none';
  }

  editForm(doctor:Doctor)
  {
    this.clearVar();
    this.doctor = doctor;
  }

  confirmDeletePaciente(paciente:Paciente)
  {
    this.clearVar();
    this.selectedPaciente = paciente;
    this.openModalConfirm();
  }

  deletePaciente(){
    this.clearVar();
    this._pacientesService.removeDoctor(this.selectedPaciente.id, this.doctor.id).subscribe(
      () =>{ 
        this.onCloseModal2();
        this.getPacientes(this.doctor.id);
      },
      error => { console.log(error); }
    );
  }
}
