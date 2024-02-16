using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Shared
{
    public class Cursos
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")] //validar
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo descripcion es obligatorio")] //validar
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "El campo ruta de imagen es obligatorio")] //validar
        public string? RutaImagen { get; set; }
        [Required(ErrorMessage = "El campo fecha de alta es obligatorio")] //validar
        public DateTime FechaAlta { get; set; }
        [Required(ErrorMessage = "El campo fecha de baja es obligatorio")] //validar
        public DateTime FechaBaja { get; set; }
        public Precio? Precio { get; set; }
        [Required(ErrorMessage = "El campo programa es obligatorio")] //validar
        public string? Programa { get; set; }
        public Error? error { get; set; }

    }
}
