using Services;
using Project.SWP;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.BuildServices(builder.Configuration);
builder.Services.CoreServices();

// add cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
              .AllowAnyMethod();
    });
});// add authen policy

builder.Services.AddAuthorization(options =>
{

    options.AddPolicy(IdentityData.Admin, policy => policy.RequireRole("Admin", "System"));
    options.AddPolicy(IdentityData.Customer, policy => policy.RequireRole("Customer", "Admin", "System"));
    options.AddPolicy(IdentityData.System, policy => policy.RequireRole("System"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

