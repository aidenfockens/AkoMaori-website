
using A2.Data;
using A2.Handler;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using A2.Models;
using A2.Controllers;
using A2.Helper;

public class Program
{
    public static void Main(string[] args)
    {



       
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<A2DbContext>(options => options.UseSqlite(builder.Configuration["WebAPIConnection"]));
        builder.Services.AddScoped<IA2Repo, A2Repo>();
        builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, A2AuthHandler>("A2Authentication", null);
        builder.Services.AddMvc(options => options.OutputFormatters.Add(new CalendarOutputFormatter()));
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("UserOnly", policy => policy.RequireClaim("username"));
            options.AddPolicy("OrganizerOnly", policy => policy.RequireClaim("Organizer"));
            options.AddPolicy("OrganizerOrUser", policy => policy.RequireClaim("UserorOrg"));
        });



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }

}