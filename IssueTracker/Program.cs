using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<AppDbContext>(opts => opts.UseSqlite(connString));
builder.Services.AddDefaultIdentity<User>()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IssueService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IssueTagService>();
builder.Services.AddScoped<ProjectService>();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
