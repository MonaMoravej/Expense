using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Expense.Data.Startup))]
namespace Expense.Data
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
