using BlazorApp1.Shared;


namespace BlazorApp1.Server.Repositorio
{
    public interface IRepositorioGeneral
    {
        public Task<Usuario> GuardarCursos(Usuario u);
        public Task<Usuario> AltaUsuarios(Usuario u);
        public Task<UsuarioLogIn> ValidarUsuario(string email);
        public Task<Usuario> DameUsuario(string email);
        public Task<IEnumerable<Cursos>> DameCursos(string email);
        public Task<IEnumerable<Cursos>> DameCursos(int idUsuario);
    }
}
