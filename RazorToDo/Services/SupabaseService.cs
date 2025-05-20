using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Runtime.CompilerServices;

namespace RazorToDo.Services
{
    // NOTE: доделал сервис
    public class SupabaseService
    {
        private readonly IConfiguration _configuration;
        private string _supabaseUrl;
        private string _supabaseKey;
        private Supabase.Client _supabaseClient;

        public SupabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
            _supabaseUrl = _configuration["Supabase:Url"];
            _supabaseKey = _configuration["Supabase:Key"];
        }
           
        public async Task<Supabase.Client> InitializeSupabase()
        {
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true,
            };

            _supabaseClient = new Supabase.Client(_supabaseUrl, _supabaseKey, options);
            await _supabaseClient.InitializeAsync();

            return _supabaseClient;
        }
    }
}
