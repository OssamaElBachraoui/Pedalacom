
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pedalacom.BLogic.Authentication;
using Pedalacom.Models;
using System.Text.Json.Serialization;

namespace Pedalacom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Add services to the container.
            builder.Services.AddDbContext<AdventureWorksLt2019Context>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("AdventureDB")));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //login autorizzato
            builder.Services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("BasicAuthentication", new AuthorizationPolicyBuilder("BasicAuthentication").RequireAuthenticatedUser().Build());
            });

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", builder => builder
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowAnyHeader()
                );
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //autenticazione
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("CorsPolicy");
            app.MapControllers();

            app.Run();
        }
    }
}