﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp1.Shared
{
    public class UsuarioApi
    {
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Email { get; set; }
        public Error? error { get; set; }
        public String? Token { get; set; }
    }
}
