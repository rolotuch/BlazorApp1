using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp1.Shared
{
    public class Precio
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo precio de venta es obligatorio")] //validar
        public double PrecioVenta { get; set; }
        [Required(ErrorMessage = "El campo fecha inicio es obligatorio")] //validar
        public DateTime FechaInicio { get; set; }
        [Required(ErrorMessage = "El campo fecha final es obligatorio")] //validar
        public DateTime FechaFin { get; set; }
    }
}
