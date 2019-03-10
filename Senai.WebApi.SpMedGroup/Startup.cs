using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Senai.WebApi.SpMedGroup {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().AddJsonOptions(
                opt =>{
                    //define a data como dia mes e ano 
                    opt.SerializerSettings.DateFormatString = "dd'-'MM'-'yyyy' 'hh':'mm";
                    //ignorando valores nulos
                    opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    //ignora loops de referencia
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                }
             ).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            services.AddAuthentication(
                c => {
                    c.DefaultAuthenticateScheme = "JwtBearer";
                    c.DefaultChallengeScheme = "JwtBearer";
                }
            ).AddJwtBearer(
                "JwtBearer",
                i => {
                    i.TokenValidationParameters = new TokenValidationParameters(){
                        //Valida o Emissor do token
                        ValidateIssuer = true,
                        ValidIssuer = "SPMedicalGroup",// Unico emissor valido será o que contem esse valor no token

                        // Valida o destinatário do token 
                        ValidateAudience= true,
                        ValidAudience = "SPMedicalGroup",//Unico destinatario valido será o que contem este valor

                        // Valida o tempo de duração do token
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromHours(1),//define o mesmo para 1 hora

                        //Define a chave de assinatura para o destinatario
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Autenticação-SPMedGroup"))
                    };
                }
            );

            services.AddSwaggerGen(
                i => i.SwaggerDoc("v1",new Info(){
                        Title = "SP Medical Group", Version = "v1"
                    }
                )
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();// usa o swagger (cria o arquivo swagger.json que é usado no comando a seguir)

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SP Medical Group");
                }
            );//usa o arquivo swagger.json e gera uma pagina de documentação

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
