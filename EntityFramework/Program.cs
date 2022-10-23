using EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<EfContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("ContextConnectionString")));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.Run();
        }
    }
}