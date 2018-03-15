using FluentValidation;
using HeadHunter.API.Infrastructure.Requests.Service;
using HeadHunter.API.Infrastructure.Requests.Service.Validation;
using HeadHunter.API.Repositories;
using HeadHunter.API.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AutoMapper;
using FluentValidation.AspNetCore;
using Swashbuckle.Swagger;


namespace HeadHunter.API.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<FeedbackContext>(options =>
                options.UseSqlServer(connectionString));
        }

        public static IServiceCollection AddSubjectStack(this IServiceCollection services)
        {
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<IValidator<CreateSubjectRequest>, CreateSubjectRequestValidator>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            //services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "ToggleSys -  HTTP API", Version = "v1" }); });

            //return services;
            return services;
        }

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, bool isDevelopment = false)
        {
            if (isDevelopment)
                services.AddMvc(opts => { opts.Filters.Add(new AllowAnonymousFilter()); })
                    .AddFluentValidation();
            else
                services.AddMvc()
                    .AddFluentValidation();

            return services;
        }


        public static IServiceCollection AddAutomapper(this IServiceCollection services)
        {
            services.RemoveAll<Mapper>();
            services.AddAutoMapper(typeof(Startup));
            return services;
        }

    }
}
