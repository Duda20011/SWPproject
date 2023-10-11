using Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Project.SWP.Middlewares;
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
});
// add authen policy
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => builder.Configuration.Bind("JWTSection", options));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(IdentityData.Member, policy => policy.RequireRole("Admin", "Customer", "System"));
    options.AddPolicy(IdentityData.Admin, policy => policy.RequireRole("Admin", "System"));
    options.AddPolicy(IdentityData.Customer, policy => policy.RequireRole("Customer", "System"));
    options.AddPolicy(IdentityData.System, policy => policy.RequireRole("System"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Middleware
app.UseHttpsRedirection();

app.UseCors();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion
