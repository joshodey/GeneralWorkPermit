namespace GeneralWorkPermit.EmailService
{
    public static class EmailExtention
    {
        public static void ConfigureMailService(this IServiceCollection services, IConfiguration Configuration)
        {
            var emailConfig = Configuration
               .GetSection("EmailConfiguration")
               .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
        }
    }
}
