using Amazon.Lambda.Core;

namespace TRG.Extensions.Lambda
{
    public abstract class Handler<TInput, TOutput> : Handler
    {
        protected Handler(Initializer initializer) : base(initializer)
        {
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public TOutput Handle(TInput input, ILambdaContext context)
        {
            this.Context.Lambda = context;

            return this.Execute(input);
        }

        protected abstract TOutput Execute(TInput input);
    }
}