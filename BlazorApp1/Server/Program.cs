using BlazorApp1.Server.Datos;
using BlazorApp1.Server.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BlazorApp1.Server.Datos;
using BlazorApp1.Server.Repositorio;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration;
// Add services to the container.
var config = builder.Configuration;
var cadenaConexionSql = new AccesoDatos(config.GetConnectionString("SQL"));
builder.Services.AddSingleton(cadenaConexionSql);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["JWT:Issuer"],
        ValidAudience = config["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(config["JWT:ClaveSecreta"]))
    };
});

builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddScoped<IRepositorioMasivo, RepositorioMasivo>();
builder.Services.AddScoped<IRepositorioGeneral, RepositorioGeneral>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();