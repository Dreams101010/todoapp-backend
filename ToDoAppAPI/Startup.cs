using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Npgsql;
using ToDoAppDomainLayer.Decorators.Command;
using ToDoAppDomainLayer.Decorators.Query;
using ToDoAppDomainLayer.Interfaces;
using ToDoAppDataAccessLayer.Commands;
using ToDoAppDataAccessLayer.Queries;
using ToDoAppDomainLayer.Parameters.Commands;
using ToDoAppDomainLayer.Parameters.Queries;
using ToDoAppDomainLayer.DataObjects;
using ToDoAppDomainLayer.Interfaces.Facades;
using ToDoAppDomainLayer.Facades;

namespace ToDoAppAPI
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        private ILifetimeScope AutofacContainer { get; set; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // register command implementations
            builder.RegisterType<AddTaskCommand>()
                .As(typeof(ICommand<AddTaskCommandParameter, int>));
            builder.RegisterType<EditTaskCommand>()
                .As(typeof(ICommand<EditTaskCommandParameter, int>));
            builder.RegisterType<RemoveTaskCommand>()
                .As(typeof(ICommand<RemoveTaskCommandParameter, int>));
            builder.RegisterType<AddCategoryCommand>()
                .As(typeof(ICommand<AddCategoryCommandParameter, int>));
            builder.RegisterType<EditCategoryCommand>()
                .As(typeof(ICommand<EditCategoryCommandParameter, int>));
            builder.RegisterType<RemoveCategoryCommand>()
                .As(typeof(ICommand<RemoveCategoryCommandParameter, int>));
            // register query implementations
            builder.RegisterType<GetActiveTasksByCategoryIdQuery>()
                .As(typeof(IQuery<GetActiveTasksByCategoryIdQueryParameter, IEnumerable<ToDoTaskOutputModel>>));
            builder.RegisterType<GetActiveTasksQuery>()
                .As(typeof(IQuery<GetActiveTasksQueryParameter, IEnumerable<ToDoTaskOutputModel>>));
            builder.RegisterType<GetCategoriesQuery>()
                .As(typeof(IQuery<GetCategoriesQueryParameter, IEnumerable<Category>>));
            builder.RegisterType<GetCompleteTasksByCategoryIdQuery>()
                .As(typeof(IQuery<GetCompleteTasksByCategoryIdQueryParameter, IEnumerable<ToDoTaskOutputModel>>));
            builder.RegisterType<GetCompleteTasksQuery>()
                .As(typeof(IQuery<GetCompleteTasksQueryParameter, IEnumerable<ToDoTaskOutputModel>>));
            builder.RegisterType<GetTasksByCategoryIdQuery>()
                .As(typeof(IQuery<GetTasksByCategoryIdQueryParameter, IEnumerable<ToDoTaskOutputModel>>));
            builder.RegisterType<GetCategoryByIdQuery>()
                .As(typeof(IQuery<GetCategoryByIdQueryParameter, Category>));
            // register command decorators
            builder.RegisterGenericDecorator(typeof(RetryCommandDecorator<,>),
                typeof(ICommand<,>));
            builder.RegisterGenericDecorator(typeof(LoggingCommandDecorator<,>),
                typeof(ICommand<,>));
            builder.RegisterGenericDecorator(typeof(ErrorHandlingCommandDecorator<,>),
                typeof(ICommand<,>));
            // register query decorators
            builder.RegisterGenericDecorator(typeof(RetryQueryDecorator<,>),
                typeof(IQuery<,>));
            builder.RegisterGenericDecorator(typeof(LoggingQueryDecorator<,>),
                typeof(IQuery<,>));
            builder.RegisterGenericDecorator(typeof(ErrorHandlingQueryDecorator<,>),
                typeof(IQuery<,>));
            // register facades
            builder.RegisterType<TaskFacade>().As<ITaskFacade>();
            builder.RegisterType<CategoryFacade>().As<ICategoryFacade>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(
                (x) => new NpgsqlConnection(Configuration.GetConnectionString("PostgresDB")));
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddHttpsRedirection((opts) => opts.HttpsPort = 443);
            services.AddCors((arg) => arg.AddPolicy("AllowAnyOrigin", options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");

            app.UseCors("AllowAnyOrigin");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
