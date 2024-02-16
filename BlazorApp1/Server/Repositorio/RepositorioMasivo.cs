using BlazorApp1.Server.Datos;
using BlazorApp1.Shared;
using System.Data;
using System.Data.SqlClient;

namespace BlazorApp1.Server.Repositorio
{
    public class RepositorioMasivo : IRepositorioMasivo
    {
        private string CadenaConexion;
        public RepositorioMasivo(AccesoDatos cadenaConexion)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSql;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }
        public async Task<IEnumerable<Cursos>> PrimerVolcadoDatos()
        {
            List<Cursos> listaCursos = new List<Cursos>();
            Cursos curso = null!;
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null!;
            SqlDataReader reader = null!;

            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.CargaIncialDatos";
                Comm.CommandType = CommandType.StoredProcedure;
                reader = await Comm.ExecuteReaderAsync();
                while (reader.Read())
                {
                    curso = new Cursos();
                    curso.Id = Convert.ToInt32(reader["id"]);
                    curso.Nombre = reader["Nombre"].ToString();
                    listaCursos.Add(curso);

                }
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
            return listaCursos;
        }

        public async Task<IEnumerable<Cursos>> DameCursos()
        {
            List<Cursos> listaCursos = new List<Cursos>();
            Cursos curso = null;
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            SqlDataReader reader = null;

            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.DameCursos";
                Comm.CommandType = CommandType.StoredProcedure;
                reader = await Comm.ExecuteReaderAsync();
                while (reader.Read())
                {
                    curso = new Cursos();
                    curso.Id = Convert.ToInt32(reader["idCurso"]);
                    curso.Nombre = reader["Nombre"].ToString();
                    curso.Descripcion = reader["Descripcion"].ToString();
                    curso.RutaImagen = reader["RutaImagen"].ToString();
                    curso.Programa = reader["Programa"].ToString();

                    if (reader["Programa"] != null)
                        curso.Programa = reader["Programa"].ToString();

                    if (reader["PrecioVenta"] != null && Convert.ToDouble(reader["PrecioVenta"]) > 0)
                    {
                        curso.Precio = new Precio();

                        curso.Precio.PrecioVenta = Convert.ToDouble(reader["PrecioVenta"]);
                        curso.Precio.Id = Convert.ToInt32(reader["IdPrecio"]);
                    }
                    listaCursos.Add(curso);

                }
            }
            finally
            {
                Comm!.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
            return listaCursos;

        }
    }
}
