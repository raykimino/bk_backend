using bk_backend.EFCore;
using Microsoft.EntityFrameworkCore;

namespace bk_backend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // SQL Conn
        var mysqlConnectionStrings = builder.Configuration.GetConnectionString("mysql");
        if (!String.IsNullOrEmpty(mysqlConnectionStrings))
        {
            builder.Services.AddDbContext<AppDbContext>(o =>
                        o.UseMySQL(mysqlConnectionStrings));
        }
        

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseAuthorization();
        
        app.MapControllers();
        
        app.Run();
    }
}