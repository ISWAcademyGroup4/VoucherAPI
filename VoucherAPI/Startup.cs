using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VoucherAPILibrary.Services;
using Steeltoe.Discovery.Client;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using VoucherAPILibrary.Messaging;
using Swashbuckle.AspNetCore.Swagger;
using VoucherAPILibrary.Helpers;
using VoucherAPILibrary.Security;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography.X509Certificates;

namespace VoucherAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {         
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
            services.AddDiscoveryClient(Configuration);
            var _jwtAuthSettings = Configuration.GetSection("JwtAuthentication");
            services.Configure<JWTAuthentication>(_jwtAuthSettings);
            var _jwtAuthentication = _jwtAuthSettings.Get<JWTAuthentication>();
            var secretKey = Encoding.UTF8.GetBytes(_jwtAuthentication.SecurityKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {              
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "http://localhost:5000",
                    ValidAudience = "http://localhost:5000",
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                };
            });
            services.AddTransient<IVoucherService<object>, VoucherService>();
            services.AddTransient<MessageBroker, MessageBroker>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Voucher API",
                    Description = "Voucherz.NG Api for handling vouchers",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Enunwah",
                        Email = "senunwah@yahoo.com",
                        Url = "https://twitter.com/temirio"
                    },
                    License = new License
                    {
                        Name = "InterswitchGroup",
                        Url = "https://interswitchgroup.com/license"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
            app.UseDiscoveryClient();
            //app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "VoucherAPI Documentation");
            });          
            //app.UseHttpsRedirection();
            
            app.UseMvc();           
        }
    }
}
