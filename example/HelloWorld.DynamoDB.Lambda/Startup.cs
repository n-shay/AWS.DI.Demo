using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorld.DynamoDB.Lambda.Domain;
using HelloWorld.DynamoDB.Lambda.Domain.Models;
using HelloWorld.DynamoDB.Lambda.Models;
using Newtonsoft.Json;
using TRG.Extensions.DataAccess;
using TRG.Extensions.DataAccess.DynamoDB.Lambda;
using TRG.Extensions.DependencyInjection;
using TRG.Extensions.Lambda;
using TRG.Extensions.Logging.Serilog.Lambda;
using TRG.Extensions.Settings;

namespace HelloWorld.DynamoDB.Lambda
{
    public class Startup : AsyncHandler<Input, Output>
    {
        public Startup() : base(new StartupInitializer())
        {
        }

        protected override async Task<Output> HandleAsync(Input input)
        {
            SomeFoo foo;

            // New concept:
            var unitOfWorkFactory = Context.ServiceProvider.Resolve<IUnitOfWorkFactory>();

            using (var unitOfWork = unitOfWorkFactory.Create<IFooUnitOfWork>())
            {
                if (input.Id != null)
                {
                    foo = await unitOfWork.Foos.GetByKeyAsync(input.Id.Value);

                    Context.Lambda.Logger.Log($"{JsonConvert.SerializeObject(foo)}{Environment.NewLine}");
                }
                else
                {
                    List<SomeFoo> result;
                    if (input.Num != null)
                    {
                        result = (await unitOfWork.Foos
                                .QueryByNumAndTextAsync(input.Num.Value, input.TextSearch))
                            .ToList();
                    }
                    else
                    {
                        result = (await unitOfWork.Foos
                                .ScanAsync(input.Id, input.Num, input.TextSearch))
                            .ToList();

                    }

                    result.ForEach(r =>
                        Context.Lambda.Logger.Log($"{JsonConvert.SerializeObject(r)}{Environment.NewLine}"));

                    foo = result.FirstOrDefault();
                }
            }

            return new Output(foo);
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