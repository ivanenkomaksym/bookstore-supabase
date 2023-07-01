using Microsoft.AspNetCore.Components.Authorization;
using WebUI.Providers;
using WebUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers().AddNewtonsoftJson();

var client = new Supabase.Client(builder.Configuration["Supabase:Url"], builder.Configuration["Supabase:Key"]);
await client.InitializeAsync();

builder.Services.AddSingleton<Supabase.Client>(x => client);

builder.Services.AddSingleton<AuthenticationStateProvider, CustomAuthStateProvider>(
    provider => new CustomAuthStateProvider(
        provider.GetRequiredService<Supabase.Client>(),
        provider.GetRequiredService<ILogger<CustomAuthStateProvider>>()
    )
);

builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
builder.Services.AddSingleton<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
