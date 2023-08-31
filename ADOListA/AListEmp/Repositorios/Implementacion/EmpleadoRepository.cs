using AListEmp.Models;
using AListEmp.Repositorios.Contrato;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace AListEmp.Repositorios.Implementacion
{
    public class EmpleadoRepository : IGenericRepository<Empleado>
    {
        private readonly string _cadenaSQL = "";
        public EmpleadoRepository(IConfiguration configuracion)
        {
            _cadenaSQL = configuracion.GetConnectionString("cadenaSQL");

        }

        public async Task<List<Empleado>> Lista()
        {
            List<Empleado> _lista = new List<Empleado>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(" SPListaEmpleados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {

                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Empleado
                        {
                            IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]),
                            NombreCompleto = dr["Nombre"].ToString(),
                            refDepartamento = new Departamento()
                            {
                                IdDepartamento = Convert.ToInt32(dr["IdDepartamento"])
                                Nombre = dr["Nombre"].ToString(),
                            },
                            Sueldo = Convert.ToInt32(dr["Sueldo"]),
                            FechaContratado = dr["FechaContratado"].ToString(),

                        }); ;

                    }

                }

            }

            return _lista;



        }

        public async Task<bool> Guardar(Empleado modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SPGuardarEmpleado", conexion);
                cmd.Parameters.AddWithValue("NombreCompleto", modelo.NombreCompleto);
                cmd.Parameters.AddWithValue("IdDepartamento", modelo.refDepartamento.IdDepartamento);
                cmd.Parameters.AddWithValue("Sueldo", modelo.Sueldo);
                cmd.Parameters.AddWithValue("Fechacontratado", modelo.FechaContratado);
                cmd.CommandType = CommandType.StoredProcedure;

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0)
                    return true;
                else
                    return false;
                    


            }
        }
        public async Task<bool> Editar(Empleado modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SPEditarEmpleado", conexion);
                cmd.Parameters.AddWithValue("IdEmpleado", modelo.IdEmpleado);
                cmd.Parameters.AddWithValue("NombreCompleto", modelo.NombreCompleto);
                cmd.Parameters.AddWithValue("IdDepartamento", modelo.refDepartamento.IdDepartamento);
                cmd.Parameters.AddWithValue("Sueldo", modelo.Sueldo);
                cmd.Parameters.AddWithValue("Fechacontratado", modelo.FechaContratado);
                cmd.CommandType = CommandType.StoredProcedure;

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0)
                    return true;
                else
                    return false;



            }
        }

        public async Task<bool> Eliminar(int id)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SPEliminarEmpleado", conexion);
                cmd.Parameters.AddWithValue("IdEmpleado",id);
                cmd.CommandType = CommandType.StoredProcedure;

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0)
                    return true;
                else
                    return false;



            }
        }




    }
}
