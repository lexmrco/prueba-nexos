import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PacienteComponent } from './components/pacientes/paciente.component';
import { FormularioPacienteComponent } from './components/pacientes/formulario/formulario.component';
import { DoctorComponent } from './components/doctores/doctor.component';
import { FormularioDoctorComponent } from './components/doctores/formulario/formulario.component';
import { PacientesService } from './services/pacientes.service'
import { DoctoresService } from './services/doctores.service'


@NgModule({
  declarations: [
    AppComponent,
    PacienteComponent,
    FormularioPacienteComponent,
    DoctorComponent,
    FormularioDoctorComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule
  ],
  providers: [ PacientesService, DoctoresService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
