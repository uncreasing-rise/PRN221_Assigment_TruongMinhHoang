using Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//DI
builder.Services.AddScoped<ISystemAccountRepo, SystemAccountRepo>();
builder.Services.AddScoped<ISupplierCompanyRepo, SupplierCompanyRepo>();
builder.Services.AddScoped<IOilPaintingRepo, OilPaintingRepo>();


//Add Session
builder.Services.AddSession();  
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

//Use session
app.UseSession();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
