namespace BlazorApp1.Server.Datos
{
    public class AccesoDatos
    {
        private string? cadenaConexionSql;
        public string CadenaConexionSql { get=> cadenaConexionSql!; }
        public AccesoDatos(string ConexionSql)
        {
            cadenaConexionSql = ConexionSql;
        }


    }
}
