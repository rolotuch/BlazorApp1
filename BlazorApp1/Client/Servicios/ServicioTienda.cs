
using BlazorApp1.Shared;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Servicios
{
    public class ServicioTienda : IServicioTienda
    {
        IConfiguration Configuration;
        private readonly HttpClient ClienteHttp;
        public ServicioTienda(HttpClient httpClient, IConfiguration configuration)
        {
            ClienteHttp = httpClient;
            Configuration = configuration;
        }

        public async Task<string> ObtenerToken()
        {
            LoginApi loginApi = new LoginApi();

            string token = String.Empty;
            loginApi.usuarioAPI = Configuration.GetValue<string>("DatosVarios:UsuarioAPI");
            loginApi.passAPI = Configuration.GetValue<string>("DatosVarios:PassAPI");

            var respuesta = await ClienteHttp.PostAsJsonAsync("api/Login", loginApi);

            if (respuesta.IsSuccessStatusCode)
            {
                UsuarioApi usuarioAPI = await respuesta.Content.ReadFromJsonAsync<UsuarioApi>();
                comprobarError(usuarioAPI!.error!);
                token = usuarioAPI.Token!;

            }
            else
                throw new Exception("Se produjo un error");

            return token;
        }


        public async Task<string> AltaUsuario(string tokenPeticion, Usuario nuevoUsuario)
        {
            String urlResultado = String.Empty;

            ClienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenPeticion);
            var respuesta = await ClienteHttp.PostAsJsonAsync("api/AltaUsuario", nuevoUsuario);

            if (respuesta.IsSuccessStatusCode)
            {
                Usuario usuarioRespuesta = await respuesta.Content.ReadFromJsonAsync<Usuario>();

                if (usuarioRespuesta.error == null || usuarioRespuesta.error.mensaje == String.Empty)
                {
                    //Generamos el enlace que llegaría  por email para confirmar el alta
                    string parametro = await ClienteHttp.GetStringAsync("api/Cifrar/" + usuarioRespuesta.Email);
                    urlResultado = "https://localhost:7070/ConfirmarAlta/" + parametro;
                }
                else
                {
                    comprobarError(usuarioRespuesta.error);
                }
            }
            else
                throw new Exception("Intentelo de nuevo se produjo un error");

            return urlResultado;
        }

        public async Task<HttpResponseMessage> ConfirmarAlta(string tokenPeticion, string emailCifrado)
        {
            UsuarioLogIn usuarioLogin = new UsuarioLogIn();
            ClienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenPeticion);
            string email = await ClienteHttp.GetStringAsync("api/DesCifrar/" + emailCifrado);
            usuarioLogin.EmailLogin = email;
            usuarioLogin.Password = "NO SE UTILIZA";
            var respuesta = await ClienteHttp.PostAsJsonAsync("api/ConfirmarAlta", usuarioLogin);
            return respuesta;
        }

        public async Task<HttpResponseMessage> CambiarPass(string tokenPeticion, UsuarioLogIn usuarioLogin)
        {
            ClienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenPeticion);
            var respuesta = await ClienteHttp.PostAsJsonAsync("api/CambiarPass", usuarioLogin);
            return respuesta;
        }

        public async Task<Usuario> DatosUsaurio(string tokenPeticion, string Email)
        {
            ClienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenPeticion);
            return await ClienteHttp.GetFromJsonAsync<Usuario>("api/DatosUsaurio/" + Email);
        }

        public async Task<List<Cursos>> DameCursosUsuario(string tokenPeticion, Usuario usuario)
        {
            List<Cursos> resultado = new List<Cursos>();
            ClienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenPeticion);
            resultado = await ClienteHttp.GetFromJsonAsync<List<Cursos>>("api/DameCursosUsuario/" + usuario.Id);
            if (resultado.Count > 0)
                comprobarError(resultado[0].error);

            return resultado;
        }

        public async Task<string> Cifrar(string tokenPeticion, string Email)
        {
            string parametro = await ClienteHttp.GetStringAsync("api/Cifrar/" + Email);
            return "https://localhost:7070/CambioPass/" + parametro;
        }

        public async Task<HttpResponseMessage> ValidarUsuario(string tokenPeticion, UsuarioLogIn usuarioLogin)
        {
            ClienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenPeticion);
            return await ClienteHttp.PostAsJsonAsync("api/ValidarUsuario", usuarioLogin);
        }

        public async Task<List<Cursos>> DameCursos(string tokenPeticion, string Email)
        {
            ClienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenPeticion);
            return await ClienteHttp.GetFromJsonAsync<List<Cursos>>("api/DameCursos/" + Email);
        }

        public async Task<List<Cursos>> DameCursos(string tokenPeticion)
        {
            ClienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenPeticion);
            return await ClienteHttp.GetFromJsonAsync<List<Cursos>>("api/DameCursos");
        }

        public async Task<HttpResponseMessage> GuardarCursos(string tokenPeticion, Usuario u)
        {
            ClienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenPeticion);
            return await ClienteHttp.PostAsJsonAsync("api/GuardarCursos", u);
        }

        public void comprobarError(Error error)
        {
            if (error != null)
            {
                if (error.mostrarEnPantalla)
                {
                    throw new Exception(error.mensaje);
                }
                else
                {
                    throw new Exception("Intentelo de nuevo, se produjo un error");
                }
            }
        }
    }
}
