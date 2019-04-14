using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace EFCoreOptimizationHints
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TestContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));

                //This is the configuration change, we override SQL Query Generator with our custom generator
                options.ReplaceService<IQuerySqlGeneratorFactory, CustomQuerySqlGeneratorFactory>();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var dbContext = context.RequestServices.GetRequiredService<TestContext>();

                //Here we just mark necessary query with recompile tag
                var query = dbContext.Tests.Where(i => i.Name == "test").WithRecompile();

                //This query will contaion 'option (recompile)' in generated SQL
                await query.ToListAsync();

                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
