﻿using Swastika.Messenger.Lib.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Sockets;
using Microsoft.Extensions.DependencyInjection;
using System;
using Messenger.Lib.SignalR.Hubs;

namespace Swastika.Messenger.Web
{
    public partial class Startup
    {
        public static void ConfigureSignalRServices(IServiceCollection services)
        {
            services.BuildServiceProvider();
            services.AddSignalR();
            services.AddCors(opt =>
                opt.AddPolicy("MessengerPolicy",
                    builder =>
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                    ));
        }

        public static void ConfigurationSignalR(IApplicationBuilder app)
        {
            app.UseSignalR(routes =>
            {
                routes.MapHub<MessengerHub>("Messenger");
            });
            app.UseCors("MessengerPolicy");
        }

    }
}
