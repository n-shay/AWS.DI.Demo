using System.Threading.Tasks;
using HelloWorld.DynamoDB.Lambda.Business;
using HelloWorld.DynamoDB.Lambda.Models;
using TRG.Extensions.DataAccess.DynamoDB.Lambda;
using TRG.Extensions.DependencyInjection;
using TRG.Extensions.Lambda;
using TRG.Extensions.Logging.Serilog.Lambda;
using TRG.Extensions.Settings;

namespace HelloWorld.DynamoDB.Lambda
{
    public class SearchFunction : AsyncHandler<SearchInput, MultiItemOutput>
    {
        public SearchFunction() : base(new StartupInitializer())
        {
        }

        protected override async Task<MultiItemOutput> ExecuteAsync(SearchInput input)
        {
            var fooService = Context.ServiceProvider.Resolve<IFooService>();

            var result = await fooService.SearchAsync(null, input.Num, input.Text);

            return new MultiItemOutput(result);
        }

        public class StartupInitializer : Initializer
        {
            protected override void Configure(IDependencyCollection dependencyCollection, IConfigurationProvider configurationProvider)
            {
                dependencyCollection.UseCurrentDomain()
                    .UseSerilog(configurationProvider)
                    .UseDynamoDB();
            }
        }
    }
}