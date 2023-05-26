using Microsoft.AspNetCore.Authentication;
using QuestionnaireManagerPOC.Controllers;
using QuestionnaireManagerPOC.Interfaces.DataInterfaces;
using QuestionnaireManagerPOC.Interfaces.ServicesInterfaces;
using QuestionnaireManagerPOC.Repositories;
using QuestionnaireManagerPOC.Services;
using AutoMapper;
using QuestionnaireManagerPOC.DTOs.ServiceDTOs;
using QuestionnaireManagerPOC.DTOs.RepositoryDTOs;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IQuestionnaireService, QuestionnaireService>();
builder.Services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();

builder.Services.AddScoped<IQuestionnaireQuestionService, QuestionnaireQuestionService>();
builder.Services.AddScoped<IQuestionnaireQuestionRepository, QuestionnaireQuestionRepository>();


builder.Services.AddScoped<IUserQuestionnaireService, UserQuestionnaireService>();
builder.Services.AddScoped<IUserQuestionnaireRepository, UserQuestionnaireRepository>();

builder.Services.AddScoped<IUserResponseService, UserResponseService>();


builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<BaseQuestionnaireModel, QuestionnaireDto>();
});

var mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        //var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        //var exception = exceptionHandlerFeature.Error;

        //// Handle the exception here, log it, or display a message to the user.
        //Console.WriteLine("Unhandled exception: " + exception.Message);

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An error occurred.");
    });
});
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
