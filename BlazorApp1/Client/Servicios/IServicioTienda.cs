using BlazorApp1.Shared;

namespace BlazorApp1.Client.Servicios
{
    public interface IServicioTienda
    {
        public Task<string> ObtenerToken();
        public Task<string> AltaUsuario(String tokenPeticion, Usuario nuevoUsuario);
        public Task<HttpResponseMessage> ConfirmarAlta(String tokenPeticion, String emailCifrado);
        public Task<HttpResponseMessage> CambiarPass(String tokenPeticion, UsuarioLogIn usuarioLogin);
        public Task<Usuario> DatosUsaurio(String tokenPeticion, String Email);
        public Task<List<Cursos>> DameCursosUsuario(String tokenPeticion, Usuario usuario);
        public Task<string> Cifrar(String tokenPeticion, String Email);
        public void comprobarError(Error error);
        public Task<HttpResponseMessage> ValidarUsuario(String tokenPeticion, UsuarioLogIn usuarioLogin);
        public Task<List<Cursos>> DameCursos(String tokenPeticion, String Email);
        public Task<List<Cursos>> DameCursos(String tokenPeticion);
        public Task<HttpResponseMessage> GuardarCursos(String tokenPeticion, Usuario u);
    }
}
