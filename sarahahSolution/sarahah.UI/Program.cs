using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sarahah.Core.Domain.IdentityEntities;
using sarahah.Core.Domain.RepositoryContracts;
using sarahah.Core.ServiceContracts;
using sarahah.Core.Services;
using sarahah.Infrastructure.Data.DBContext;
using sarahah.Infrastructure.Repositories;
// test deploy

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var ConnectionString = builder.Configuration.GetConnectionString("constr") ?? throw new InvalidOperationException("Connection string 'constr' not found !");

builder.Services.AddDbContext<AppDbContext>(options => 
options.UseSqlServer(ConnectionString)); 

builder.Services.AddScoped<IMessageService , MessageService>();
builder.Services.AddScoped<IMessagesRepository , MessagesRepository>();



builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 3;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, AppDbContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole, AppDbContext, Guid>>();

builder.Services.AddAuthorization(Options =>
{
    Options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});
builder.Services.ConfigureApplicationCookie(Options =>
{
    // if the user not logged in then automatically it has to redirect Login 
    Options.LoginPath = "/Account/Login";
});



var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
