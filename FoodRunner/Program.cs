/// ----------------------------- IMPORTAZIONE DEI NAMESPACE -----------------------------
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FoodRunner.Services;
using Microsoft.OpenApi.Models;

// ------------------------- FASE 1: CREAZIONE E CONFIGURAZIONE DELL’APP -------------------------

var builder = WebApplication.CreateBuilder(args);

// Aggiunge i controller (necessari per gestire le API REST)
builder.Services.AddControllers();

// Registra i service come singleton 
builder.Services.AddSingleton<PiattoService>();
builder.Services.AddSingleton<OrdineService>();
builder.Services.AddSingleton<UtenteService>();

// Configura CORS per permettere al frontend (anche su domini diversi) di accedere all'API
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()    // Consenti tutti i domini (ok per sviluppo, non in produzione!)
            .AllowAnyHeader()    // Accetta tutte le intestazioni (es. Authorization, Content-Type)
            .AllowAnyMethod();   // Consente tutti i metodi HTTP (GET, POST, PUT, DELETE, ecc.)
    });
});

// Configura Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Frontend API",
        Version = "v1",
    });
});

// Costruisce l'app in base alla configurazione
var app = builder.Build();

// ------------------------- FASE 2: CONFIGURAZIONE DELLA PIPELINE DI MIDDLEWARE -------------------------

// Abilita CORS
app.UseCors();

// Mostra dettagli degli errori in ambiente di sviluppo
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Frontend API V1");
    });
}
else
{
    app.UseExceptionHandler("/error");
}

// Reindirizza le richieste HTTP a HTTPS (sicurezza)
app.UseHttpsRedirection();

// Mappa gli endpoint dei controller (es. /api/products)
app.MapControllers();

// Avvia l'applicazione e ascolta le richieste
app.Run();
