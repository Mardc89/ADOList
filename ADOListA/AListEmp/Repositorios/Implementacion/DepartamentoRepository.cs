using AListEmp.Models;
using AListEmp.Repositorios.Contrato;
using System.Data;
using System.Data.SqlClient;

namespace AListEmp.Repositorios.Implementacion
{
    public class DepartamentoRepository:IGenericRepository<Departamento>
    {
        private readonly string _cadenaSQL="";
        public DepartamentoRepository(IConfiguration configuracion)
        {
            _cadenaSQL = configuracion.GetConnectionString("cadenaSQL");
                
        }

        public async Task<List<Departamento>> Lista()
        {
            List<Departamento> _lista = new List<Departamento>();

            using (var conexion=new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SPListaDepartament", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using(var dr=await cmd.ExecuteReaderAsync()){

                    while(await dr.ReadAsync()){
                        _lista.Add(new Departamento
                        {
                            IdDepartamento = Convert.ToInt32(dr["IdDepartamento"]),
                            Nombre = dr["Nombre"].ToString()

                        }) ;

                    }

                }

            }

            return _lista;
        }

        public Task<bool> Editar(Departamento modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int d)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Guardar(Departamento modelo)
        {
            throw new NotImplementedException();
        }


    }
}
