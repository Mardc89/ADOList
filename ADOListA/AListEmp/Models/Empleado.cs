namespace AListEmp.Models
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string NombreCompleto { get; set;}
        public Departamento refDepartamento { get; set; }
        public int Sueldo{ get; set; }
        public string FechaContratado { get; set; }

    }
}
