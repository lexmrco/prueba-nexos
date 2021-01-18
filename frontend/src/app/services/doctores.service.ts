import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Doctor } from '../components/doctores/doctor'
import { map } from 'rxjs/operators';


import { GLOBAL } from './global'

@Injectable()
export class DoctoresService{
    public url:string;
    constructor(private _http : Http)
    {
        this.url = GLOBAL.url;
    }

    getList()
    {
        return this._http.get(`${this.url}/doctores`).pipe(map(res => res.json()));
    }

    getDoctor(id: string)
    {
        return this._http.get(`${this.url}/doctores/${id}`).pipe(map(res => res.json()));
    }

    getPacientes(id: string)
    {
        return this._http.get(`${this.url}/doctores/${id}/getpacientes`).pipe(map(res => res.json()));
    }

    getSelectList()
    {
        return this._http.get(`${this.url}/doctores/selectlist`).pipe(map(res => res.json()));
    }

    post(doctor: Doctor){
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.post(`${this.url}/doctores`, doctor, options);
    }

    put(doctor: Doctor){

        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.put(`${this.url}/doctores/${doctor.id}`,doctor, options);
    }

    delete(doctor: Doctor){
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.delete(`${this.url}/doctores/${doctor.id}`, options);
    }
}