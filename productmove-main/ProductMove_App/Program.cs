using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductMove_App.Data;
using RestSharp;

class Program
{
    public static RestClient Client { get; set; } = null!;
    public static IConfiguration Config { get; private set; } = null!;
    public static string RootPath { get; set; } = null!;
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddDbContext<ProductMove_AppContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SQL") ?? throw new InvalidOperationException("Connection string 'ProductMove_AppContext' not found.")));
        builder.Services.AddHttpClient();

        builder.Services.AddDistributedMemoryCache();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(1000);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });


        builder.Services.AddAuthentication();


        var app = builder.Build();
        Config = app.Configuration;
        Client = new RestClient(Config["APIServer"]);
        RootPath = app.Environment.ContentRootPath;
        Console.WriteLine(RootPath);
        Console.WriteLine(Program.Client);
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
        app.UseSession();
        app.MapRazorPages();

        app.Run();

    }
}