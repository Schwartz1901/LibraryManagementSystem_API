

using System;
using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.Business.Mapper;
using LibraryManagementSystem_API.Business.Services;
using LibraryManagementSystem_API.DataAccess;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;
using LibraryManagementSystem_API.DataAccess.Repositories.Concrete;
using LibraryManagementSystem_API.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.ComponentModel;
using System.Text;

using Nest; // For ElasticSearch


using Hangfire;
using Hangfire.SqlServer;


var builder = WebApplication.CreateBuilder(args);


// Configure Elastic Search client

var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("library");

var client = new ElasticClient(settings);

var response = client.Ping();

if (!response.IsValid)
{
    Console.WriteLine($"Error: {response.OriginalException.Message}");
}
else
{
    Console.WriteLine("Connection Successfull");
}



builder.Services.AddSingleton<IElasticClient>(client);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTION_STRING")));

builder.Services.AddHangfire(configuration => configuration
                                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                                .UseSimpleAssemblyNameTypeSerializer()
                                .UseDefaultTypeSerializer()
                                .UseSqlServerStorage(
                           builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTION_STRING"), new SqlServerStorageOptions
                           {
                               CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                               SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                               QueuePollInterval = TimeSpan.Zero,
                               UseRecommendedIsolationLevel = true,
                               DisableGlobalLocks = true
                           }

                           ));

builder.Services.AddTransient<ElasticSearchServices>();

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; ;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
/*            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],*/
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:5173", "http://localhost:9200")
                                                        .AllowAnyMethod()
                                                        .AllowAnyHeader()
                                                        .AllowCredentials()
                                                        );

    // https://library-system-9d5s.vercel.app

    /*    options.AddPolicy(name: "AllowAllCorsPolicy", builder =>
        {
            builder
          .AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
    *//*      .AllowCredentials();*//*
        });*/

});


// add Repository
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IBorrowRepository, BorrowRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

// add Services
builder.Services.AddScoped<IBookServices, BookServices>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IRequestServices, RequestServices>();
builder.Services.AddScoped<ICommentServices, CommentServices>();
builder.Services.AddScoped<IBorrowServices, BorrowServices>();
builder.Services.AddScoped<IImageServices, ImageServices>();
builder.Services.AddScoped<INotificationServices, NotificationServices>();

builder.Services.AddScoped<IELasticSearchServices, ElasticSearchServices>();



// Services for Hangfire and SignalR - Realtime communication
builder.Services.AddHangfireServer();
builder.Services.AddSignalR();
builder.Services.AddScoped<INotificationServices, NotificationServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
 /*   app.UseSwaggerUI();*/
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Library API V1");
        c.RoutePrefix = string.Empty;
    });
}

/*app.MapGet("/", () => "Hello World!");

app.MapGet("/search", async (ElasticSearchServices elasticSearchServices, string query) =>
    {
        var results = await elasticSearchServices.SearchAsync(query);
        return Results.Ok(results.Documents);
    }
);

app.MapPost("/index", async (ElasticSearchServices elasticSearchServices) =>
    {
        await elasticSearchServices.IndexDataAsync();
        return Results.Ok();
    }
);*/

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.UseCors("CorsPolicy");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<NotificationHub>("/notificationHub");
});
/*app.MapControllers();*/

app.UseHangfireDashboard();

app.Run();
