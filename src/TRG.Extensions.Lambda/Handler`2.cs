﻿using Amazon.Lambda.Core;

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
            LambdaLifestyle.Invoked();
            Context.Lambda = context;

            return Handle(input);
        }

        protected abstract TOutput Handle(TInput input);
    }
}