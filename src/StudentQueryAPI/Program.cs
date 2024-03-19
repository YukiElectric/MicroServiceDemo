using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SharedModel;
using StudentQueryAPI.Common;
using StudentQueryAPI.Data;
using StudentQueryAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


builder.Services.AddCors(options => options.AddDefaultPolicy(
    policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())
);

builder.Services.AddDbContext<StudentContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentDBQuery"));
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMassTransit(x =>
{
    x.AddRequestClient<MessageConsumer>(new Uri("exchange:authen-request"));
    x.AddConsumer<CreateEventConsumer>().Endpoint(e => e.Name = "create-student-request");
    x.AddConsumer<UpdateEventConsumer>().Endpoint(e => e.Name = "update-student-request");
    x.AddConsumer<DeleteEventConsumer>().Endpoint(e => e.Name = "delete-student-request");
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ReceiveEndpoint("create-student-request", e =>
        {
            e.ConfigureConsumer<CreateEventConsumer>(context);
        });
        cfg.ReceiveEndpoint("update-student-request", e =>
        {
            e.ConfigureConsumer<UpdateEventConsumer>(context);
        });
        cfg.ReceiveEndpoint("delete-student-request", e =>
        {
            e.ConfigureConsumer<DeleteEventConsumer>(context);
        });
    });
});

builder.Services.AddScoped<IStudentRepository, StudentRepository>();

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
