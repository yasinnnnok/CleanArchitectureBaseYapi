
namespace CleanArchitecture.WebApi.Configurations
{
    public sealed class AuthorizeServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            //Authenticate işlemi
            services.AddAuthentication().AddJwtBearer();
            services.AddAuthorization();

        }
    }
}
