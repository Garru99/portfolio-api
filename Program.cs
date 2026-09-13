var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("https://sergiogarrues.vercel.app", 
                "http://localhost:5500", 
                "http://127.0.0.1:5500", "http://localhost:5500")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AllowVercel");

// --- ENDPOINTS ---

// 1. Estado y Health Check
app.MapGet("/api/health", () => Results.Ok(new { 
    status = "Online", 
    framework = ".NET 8.0", 
    environment = "Render Cloud",
    uptime = TimeSpan.FromHours(2.5).ToString(@"hh\:mm\:ss")
}));

// 2. Info
app.MapGet("/api/profile/stack", () => Results.Ok(new {
    developer = "Sergio Garrués",
    role = ".NET & Backend Developer",
    architecture = "Clean Architecture / CQRS",
    coreSkills = new[] { ".NET 8", "C#", "SQL Server", "EF Core", "Dapper", "Azure" },
    databaseOpt = "Index Tuning & Stored Procedures Expert"
}));

// 3. Simulación 
app.MapGet("/api/metrics/benchmark", () => {
    var sw = System.Diagnostics.Stopwatch.StartNew();
    
    // Simulación de iteración
    int sum = 0;
    for (int i = 0; i < 100_000; i++) sum += i;
    sw.Stop();

    return Results.Ok(new {
        operation = "Iteración de 100,000 elementos",
        executionTimeMs = sw.Elapsed.TotalMilliseconds,
        allocation = "0 KB (Zero Allocation)",
        status = "Optimizado"
    });
});

// 4. Nivel de Cafeína
app.MapGet("/api/fun/coffee-status", () => {
    var hour = DateTime.UtcNow.Hour;
    string status = hour switch {
        >= 6 and < 12 => "Cargando baterías (Café #1)",
        >= 12 and < 18 => "Rendimiento óptimo (.NET a full)",
        _ => "Modo Depuración Nocturna"
    };

    return Results.Json(new { 
        caffeineLevel = "85%", 
        message = status, 
        httpCode = 200 
    });
});

app.Run();