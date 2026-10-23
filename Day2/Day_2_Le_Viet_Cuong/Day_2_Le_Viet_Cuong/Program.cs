var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// ==========================================
// 1. [ATTRIBUTE ROUTING]
// Cho phép các Controller sử dụng [Route], [HttpGet]...
// ==========================================
app.MapControllers();

// ==========================================
// 2. [CONVENTIONAL ROUTING] - Định tuyến theo quy ước
// ==========================================

// Route quy ước tùy biến 1: Chào mừng (chao-mung/{name}/{id?})
app.MapControllerRoute(
    name: "chaoMung",
    pattern: "chao-mung/{name}/{id:int?}",
    defaults: new { controller = "Hello", action = "Welcome" });

// Route quy ước tùy biến 2: Giới thiệu (gioi-thieu -> Home/Privacy)
app.MapControllerRoute(
    name: "gioiThieu",
    pattern: "gioi-thieu",
    defaults: new { controller = "Home", action = "Privacy" });

// Route quy ước mặc định (Default Conventional Route)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
