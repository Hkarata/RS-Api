using Carter;
using Hangfire;
using Microsoft.Extensions.Compliance.Classification;
using Microsoft.Extensions.Compliance.Redaction;
using RSAllies.Jobs.Extensions;
using RSAllies.Mail.Extensions;
using RSAllies.Shared.Extensions;
using RSAllies.SMS.Extensions;
using RSAllies.Test.Extensions;
using RSAllies.Users.Extensions;
using RSAllies.Venues.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Logging.ClearProviders();

//builder.Logging.AddJsonConsole(options =>
//    options.JsonWriterOptions = new JsonWriterOptions
//    {
//        Indented = true
//    });

List<Assembly> mediatRAssemblies = [typeof(Program).Assembly]; // List of mediatR assemblies from modules

builder.Services.AddUsersModuleServices(builder.Configuration, mediatRAssemblies); // User Module Registration

builder.Services.AddVenuesModuleServices(builder.Configuration, mediatRAssemblies); // Venue Module Registration

builder.Services.AddTestModuleServices(builder.Configuration, mediatRAssemblies); // Test Module Registration

builder.Services.AddMailModuleServices(builder.Configuration, mediatRAssemblies); // Email Module Registration

builder.Services.AddSmsModuleServices(builder.Configuration, mediatRAssemblies); // Sms Module Registration

builder.Services.AddJobsModuleServices(builder.Configuration, mediatRAssemblies); // Jobs Module Registration

builder.Services.AddMediatR(configuration =>
    configuration.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()));

builder.Services.AddCarter();

builder.Logging.EnableRedaction();

builder.Services.AddRedaction(redact =>
{
    redact.SetRedactor<ErasingRedactor>(new DataClassificationSet(DataTaxonomy.SystemData));

    redact.SetRedactor<StarRedactor>(new DataClassificationSet(DataTaxonomy.SensitiveData));

    redact.SetHmacRedactor(options =>
    {
        options.Key = Convert.ToBase64String("Dontwriteitlikethisuseasecretsmanager"u8);
        options.KeyId = 69;
    }, new DataClassificationSet(DataTaxonomy.PiiData));

});

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

app.UseHttpsRedirection();

app.MapCarter();

app.UseHangfireDashboard();

app.Run();