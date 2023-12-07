using AzCosmos;
using AzCosmos.WebApp;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTodoCollection(builder.Configuration, builder.Environment);

WebApplication app = builder.Build();

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

var cosmosCollection = app.Services.GetRequiredService<TodosCollection>();

try
{
    cosmosCollection.Initialize();
    await app.RunAsync();
}
catch (Exception e)
    when (e is TaskCanceledException or OperationCanceledException)
{
    Console.Error.WriteLine("Failed to initialize the Cosmos collection in time");
    await app.StopAsync();
}