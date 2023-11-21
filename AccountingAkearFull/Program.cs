
using AccountsAkear.Servecs;
using Accouting.Data.Interfaces;
using Accouting.Data.UnitOfWork;
using Accouting.Data;
using Accouting.Servecs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddDbContext<AccountingDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));
builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
builder.Services.AddScoped(typeof(AccountSereves));
builder.Services.AddScoped(typeof(MakeJournalBodyServecs));
builder.Services.AddScoped(typeof(CostCenterServecs));
builder.Services.AddScoped(typeof(GetSelectAccountTypesServecs));
builder.Services.AddScoped(typeof(TreeOfAccountsServecs));
builder.Services.AddScoped(typeof(MakeJournalHeadServecs));
builder.Services.AddScoped<MudThemeProvider>();


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();