using PhotoStudiy.Services.Contracts.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


//builder.Services.AddScoped<IClientService, ClientService>();
//builder.Services.AddScoped<IClientServiceRead, DisciplineReadRepository>();

//builder.Services.AddScoped<IDocumentService, DocumentService>();
//builder.Services.AddScoped<IDocumentReadRepository, DocumentReadRepository>();

//builder.Services.AddScoped<IEmployeeService, EmployeeService>();
//builder.Services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();

//builder.Services.AddScoped<IGroupService, GroupService>();
//builder.Services.AddScoped<IGroupReadRepository, GroupReadRepository>();

//builder.Services.AddScoped<IPersonService, PersonService>();
//builder.Services.AddScoped<IPersonReadRepository, PersonReadRepository>();

//builder.Services.AddScoped<ITimeTableItemService, TimeTableItemService>();
//builder.Services.AddScoped<ITimeTableItemReadRepository, TimeTableItemReadRepository>();

//builder.Services.AddSingleton<ITimeTableContext, TimeTableContext>();
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
