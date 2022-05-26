using SqlInteractive.BLL.Services;
using SqlInteractive.BLL.ServicesImpls;
using SqlInteractive.BLL.ServicesInternal;
using SqlInteractive.SqlExecution.Configuration;
using SqlInteractive.SqlExecution.Db;
using SqlInteractive.SqlExecution.Db.DbContexts;
using SqlInteractive.SqlExecution.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:8888", "https://*:4444");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.Cookie.Name = ".SqlInteractive.Session";
	options.IdleTimeout = TimeSpan.FromHours(1);
	options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IDbContext, DbContextSqlServer>();
builder.Services.AddScoped<ISqlSessionExecutor, SqlExecutorWithUsers>();
builder.Services.AddScoped<ISqlService, SqlService>();
builder.Services.AddOptions<DbOptions>().BindConfiguration("Db");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.Run();
