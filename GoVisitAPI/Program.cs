using GoVisitAPI.DataModel;
using GoVisitAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GoVisitDbConfig");

builder.Services.AddDbContext<GoVisitContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<GoVisitContext>();

//     db.Database.EnsureDeleted();
//     db.Database.EnsureCreated();

//     if (!db.PublicOrganizations.Any())
//     {
//         DbSeeder.Seed(db);
//     }
// }

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


