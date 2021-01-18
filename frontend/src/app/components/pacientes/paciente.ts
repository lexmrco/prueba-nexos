export class Paciente{
    constructor(
        public id:string,
        public nombre:string,
        public apellido:string,
        public seguroSocial:string,
        public codigoPostal:string,
        public telefono:string
    ){}
}

export class Doctor{
    constructor(
        public id:string,
        public nombre:string,
        public apellido:string,
        public especialidad:string,
        public hospital:string
    ){}
}