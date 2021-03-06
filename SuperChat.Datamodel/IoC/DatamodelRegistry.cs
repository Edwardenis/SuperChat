﻿using Microsoft.Extensions.DependencyInjection;
using SuperChat.Datamodel.Contexts;
using SuperChat.Datamodel.Repositories;
using SuperChat.Datamodel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.Datamodel.IoC
{
    public static class DatamodelRegistry
    {
        public static void AddDatamodelRegistry(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork<SuperChatDbContext>, SuperChatUnitOfWork>();
        }
    }
}
