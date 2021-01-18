import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Paciente } from '../components/pacientes/paciente'
import { map } from 'rxjs/operators';


import { GLOBAL } from './global'

@Injectable()
export class PacientesService{
    public url:string;
    constructor(private _http : Http)
    {
        this.url = GLOBAL.url;
    }

    getList()
    {
        return this._http.get(`${this.url}/pacientes`).pipe(map(res => res.json()));
    }

    getPaciente(id: string)
    {
        return this._http.get(`${this.url}/pacientes/${id}`).pipe(map(res => res.json()));
    }

    post(paciente: Paciente){
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.post(`${this.url}/pacientes`, paciente, options);
    }

    put(paciente: Paciente){

        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.put(`${this.url}/pacientes/${paciente.id}`,paciente, options);
    }

    delete(paciente: Paciente){
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.delete(`${this.url}/pacientes/${paciente.id}`, options);
    }

    getDoctores(id: string)
    {
        return this._http.get(`${this.url}/pacientes/${id}/getdoctores`).pipe(map(res => res.json()));
    }

    getSelectList()
    {
        return this._http.get(`${this.url}/pacientes/selectlist`).pipe(map(res => res.json()));
    }

    addDoctor(idPaciente: string, idDoctor:string){
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.put(`${this.url}/pacientes/adddoctor/${idPaciente}/${idDoctor}`,null, options);
    }

    removeDoctor(idPaciente: string, idDoctor:string){
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.put(`${this.url}/pacientes/removedoctor/${idPaciente}/${idDoctor}`,null, options);
    }
}