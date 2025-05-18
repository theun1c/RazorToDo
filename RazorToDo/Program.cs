using RazorToDo.Models;
using Serilog;

namespace RazorToDo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {      
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            // - мб понадобится потом
            //builder.Logging.ClearProviders();
            //builder.Logging.AddSerilog();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            // подключение супабейза 
            var url = builder.Configuration["Supabase:Url"];
            var key = builder.Configuration["Supabase:Key"];

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true,
            };

            var supabase = new Supabase.Client(url, key, options);
            await supabase.InitializeAsync();

            // заполнение тестовыми данными
            var model = new TodoItem
            {
                Title = "Title 123",
                Description = "Description 123",
                Author = "Author 123",
                Status = "Status",
            };
            
            // заполнение + получение
            await supabase.From<TodoItem>().Insert(model);
            var data = await supabase.From<TodoItem>().Get();
            var result = data.Model;

            // использование серилога для логгирования
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}")
                .CreateLogger();

            // вывод лога в консольку 
            Log.Warning(result.ToString());

            app.Run();
        }
    }
}
