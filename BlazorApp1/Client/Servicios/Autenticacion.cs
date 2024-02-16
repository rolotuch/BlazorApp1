using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorApp1.Client
{
    public class Autenticacion : AuthenticationStateProvider
    {
        private ISessionStorageService almacenarSesionServicio;

        public Autenticacion(ISessionStorageService SessionStorageService)
        {
            almacenarSesionServicio = SessionStorageService;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var email = await almacenarSesionServicio.GetItemAsync<string>("email");

            ClaimsIdentity identity;
            if (email != null)
            {
                identity = new ClaimsIdentity(new[] {
              new Claim(ClaimTypes.Name,email),
              }, "login_Auth");
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var usuario = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(usuario));
        }

        public void CerrarSesion()
        {
            almacenarSesionServicio.RemoveItemAsync("email");
            var identity = new ClaimsIdentity();

            var usuario = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(usuario)));
        }
    }
}

//libreria para la sesion storage blazored para este caso usamos blazored.sessionstorage  en nuget lo instalamos.