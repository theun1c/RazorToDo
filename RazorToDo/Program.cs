using RazorToDo.Models;
using RazorToDo.Services;
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

            var model = new TodoItem
            {
                Title = "sdfsdfsdf",
                Description = "sdfsdfsdf",
                Author = "Author",
            };

            // теперь можно использовать сервис с супой
            var supabase = new SupabaseService(builder.Configuration); // создаем сервис
            var supabaseClient = await supabase.InitializeSupabase(); // создаем клиент
            // тестовая вставка
            supabaseClient.From<TodoItem>().Insert(model);

            // использование серилога для логгирования
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}")
                .CreateLogger();

            app.Run();
        }
    }
}
