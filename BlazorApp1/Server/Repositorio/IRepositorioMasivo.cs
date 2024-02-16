using BlazorApp1.Shared;

namespace BlazorApp1.Server.Repositorio
{
    public interface IRepositorioMasivo
    {
        public Task<IEnumerable<Cursos>> PrimerVolcadoDatos();
        public Task<IEnumerable<Cursos>> DameCursos();
    }
}
