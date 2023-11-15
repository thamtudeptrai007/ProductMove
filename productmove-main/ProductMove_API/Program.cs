using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductMove_API.Data;
using ProductMove_API.Service.ChiTietKhoService;
using ProductMove_API.Service.KhoService;
using ProductMove_API.Service.LoaiSanPhamService;
using ProductMove_API.Service.NhapKhoSerice;
using ProductMove_API.Service.SanPhamService;
using ProductMove_API.Service.SeriService;
using ProductMove_API.Service.TaiKhoanService;
using ProductMove_API.Service.XuatKhoService;
using ProductMove_API.Service.BaoCaoService;
using System.Text.Json.Serialization;
using ProductMove_API.Service.KhachHangService;
using ProductMove_API.Service.HoaDonService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductMove_DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQL") ?? throw new InvalidOperationException("Connection string 'ProductMove_DBContext' not found.")));
// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add DI
builder.Services.AddScoped<ITaiKhoanService, TaiKhoanService>();
builder.Services.AddScoped<ISanPhamService, SanPhamService>();
builder.Services.AddScoped<IKhoService, KhoService>();
builder.Services.AddScoped<INhapKhoService, NhapKhoService>();
builder.Services.AddScoped<IXuatKhoService, XuatKhoService>();
builder.Services.AddScoped<ILoaiSanPhamService, LoaiSanPhamService>();
builder.Services.AddScoped<IChiTietKhoService, ChiTietKhoService>();
builder.Services.AddScoped<ISeriService, SeriService>();
builder.Services.AddScoped<IBaoCaoService, BaoCaoService>();
builder.Services.AddScoped<IKhachHangService, KhachHangService>();
builder.Services.AddScoped<IHoaDonService, HoaDonService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<ProductMove_DBContext>();
        db.Database.EnsureCreated();
        DbInitializer.Initialize(db);
    }
    catch (Exception ex)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
