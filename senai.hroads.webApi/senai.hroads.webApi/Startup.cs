using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace senai.hroads.webApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()

            .AddNewtonsoftJson(options =>
                {
                    // Ignora os loopings nas consultas
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    // Ignora valores nulos nas consultas
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HROADS.webApi", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services
            // Define a forma de autentica��o
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })

            // Define os par�metros de valida��o do token
            .AddJwtBearer("JwtBearer", options =>
            {
                // quem est� emitindo
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // quem est� emitindo
                    ValidateIssuer = true,

                    // quem est� recebendo
                    ValidateAudience = true,

                    // o tempo de expira��o
                    ValidateLifetime = true,

                    // forma de criptografia e a chave de autentica��o
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("HROADS-key-autenticacao")),

                    // tempo de expira��o do token
                    ClockSkew = TimeSpan.FromMinutes(30),

                    // nome do issuer, de onde est� vindo o token
                    ValidIssuer = "HROADS.webApi",

                    // nome do audience, para onde est� indo
                    ValidAudience = "HROADS.webApi"
                };

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            // Habilita a autentica��o
            app.UseAuthentication();

            // Habilita a autoriza��o
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
