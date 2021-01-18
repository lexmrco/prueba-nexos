import { Component } from '@angular/core'
import { FormGroup, NgForm } from '@angular/forms'
import { Paciente, Doctor } from '../paciente'
import { PacientesService } from '../../../services/pacientes.service'
import { DoctoresService } from '../../../services/doctores.service'
import { HttpClientModule } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
    selector: 'pacientes-formulario',
    templateUrl: './formulario.component.html',
    providers: [ PacientesService, DoctoresService, HttpClientModule]  
})

export class FormularioPacienteComponent {
  public crudAction : string;
  public paciente : Paciente;
  public selectDoctorId : string;
  public doctores : Array<Doctor>;
  public selectedDoctor : Doctor;
  public doctoresSelectList : Array<Doctor>;
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
  title = 'Formulario Pacientes';

  ngOnInit(){
      const pacienteId = this.route.snapshot.paramMap.get("id");
      this.initLoad();
      if(pacienteId != null){
        // Obtener todos los campos
        this.getPaciente(pacienteId);
        // Obtener los datos de los médicos asociados
        this.getDoctores(pacienteId);
        // Obtener la lista completa de doctores por asociar
        this.getDoctoresSelectList();
        // Indicar que se está actualizando
        this.crudAction = "update";
      } else {
        this.crudAction = "create";
        this.paciente =  new Paciente("","","","","","");
      }
  }

  getPaciente(id: string){
    this._pacientesService.getPaciente(id).subscribe(
      result =>{ this.paciente = result },
      error => { this.error = 'Error obteniendo los datos del paciente seleccionado';
      ; }
    );
  }

  getDoctores(pacienteId: string){
    this._pacientesService.getDoctores(pacienteId).subscribe(
      result =>{ this.doctores = result; console.log(this.doctores) },
      error => { console.log(error); }
    );
  }

  getDoctoresSelectList(){
    this._doctoresService.getSelectList().subscribe(
      result =>{ this.doctoresSelectList = result },
      error => { console.log(error); }
    );
  }

  private initLoad(){
    this.loading = true;
    this.error = "";
    this.selectedDoctor = new Doctor("","","","","");
    this.paciente =  new Paciente("","","","","","");
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

  onSubmitDoctor(form: NgForm){
    if (form.valid) {
      this._pacientesService.addDoctor(this.paciente.id, this.selectDoctorId).subscribe(
        data =>{ 
          this.onCloseHandled();
          this.selectDoctorId = null;
        this.getDoctores(this.paciente.id);
        },
        error => { 
          console.log(error); 
          if(error.status == '400'){
            this.error = 'Error asociando el doctor al paciente, revise si están correctamente diligenciados';
          }
          else{
            this.error = 'Error en el servidor, por favor intente más tarde';
          }
        }
      );
    }
  }

  private create(){
    this._pacientesService.post(this.paciente).subscribe(
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
    this._pacientesService.put(this.paciente).subscribe(
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
    this.router.navigate(['/pacientes']);
  }

  onCloseModal2(){
    this.clearVar();
    this.displayModal2='none';
  }

  editForm(paciente:Paciente)
  {
    this.clearVar();
    this.paciente = paciente;
  }

  confirmDeleteDoctor(doctor:Doctor)
  {
    this.clearVar();
    this.selectedDoctor = doctor;
    this.openModalConfirm();
  }

  deleteDoctor(){
    this.clearVar();
    this._pacientesService.removeDoctor(this.paciente.id, this.selectedDoctor.id).subscribe(
      () =>{ 
        this.onCloseModal2();
        this.getDoctores(this.paciente.id);
      },
      error => { console.log(error); }
    );
  }
}
