export class Doctor{
    constructor(
        public id:string,
        public nombre:string,
        public apellido:string,
        public especialidad:string,
        public hospital:string,
        public codigoProfesional:string
    ){}
}

export class Paciente{
    constructor(
        public id:string,
        public nombre:string,
        public apellido:string,
        public telefono:string
    ){}
}