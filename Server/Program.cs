

using BL;
using BL.Api;

namespace Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // builder.Services.AddSingleton<IblMeeting, BlMeetingService>();
        // builder.Services.AddSingleton<IBlPatient, BlPatientService>();

        builder.Services.AddSingleton<Ibl, BlManager>();
       builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy("AllowAllOrigins",
        //        builder => builder.AllowAnyOrigin());
        //});

        builder.Services.AddCors(c => c.AddPolicy("AllowAll",
            option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


        var app = builder.Build();


        app.UseCors("AllowAll");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}