using System.Threading.Tasks;
using Amazon.Lambda.Core;

namespace TRG.Extensions.Lambda
{
    public abstract class AsyncHandler<TInput, TOutput> : Handler
    {
        protected AsyncHandler(Initializer initializer) : base(initializer)
        {
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public Task<TOutput> HandleAsync(TInput input, ILambdaContext context)
        {
            this.Context.Lambda = context;

            return this.ExecuteAsync(input);
        }

        protected abstract Task<TOutput> ExecuteAsync(TInput input);
    }
}