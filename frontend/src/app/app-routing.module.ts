import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PacienteComponent } from './components/pacientes/paciente.component'
import { FormularioPacienteComponent } from './components/pacientes/formulario/formulario.component'
import { DoctorComponent } from './components/doctores/doctor.component'
import { FormularioDoctorComponent } from './components/doctores/formulario/formulario.component'

const routes: Routes = [
  { path: '', component:PacienteComponent },
  { path: 'pacientes', component:PacienteComponent },
  { path: 'pacientes/crear', component:FormularioPacienteComponent },
  { path: 'pacientes/actualizar/:id', component:FormularioPacienteComponent },
  { path: 'crear', component:FormularioPacienteComponent },
  { path: 'actualizar/:id', component:FormularioPacienteComponent },
  { path: 'doctores', component:DoctorComponent },
  { path: 'doctores/crear', component:FormularioDoctorComponent },
  { path: 'doctores/actualizar/:id', component:FormularioDoctorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
