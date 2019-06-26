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
    public class ItemFunction : AsyncHandler<QueryByIdInput, SingleItemOutput>
    {
        public ItemFunction() : base(new StartupInitializer())
        {
        }

        protected override async Task<SingleItemOutput> ExecuteAsync(QueryByIdInput input)
        {
            var fooService = Context.ServiceProvider.Resolve<IFooService>();

            var result = await fooService.GetItemAsync(input.Id);

            return new SingleItemOutput(result);
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