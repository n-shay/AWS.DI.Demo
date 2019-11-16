namespace TRG.Extensions.ObjectMapping
{
    using System.Collections.Generic;

    using AutoMapper;

    public abstract class MapperBase<TDest> : IMapper<TDest>
    {
        private readonly object syncRoot = new object();
        private IMapper mapper;
        
        protected abstract void Configure(MapperOptions options);

        public TDest Map<TSrc>(TSrc src)
        {
            return this.GetOrCreate().Map<TSrc, TDest>(src);
        }

        public IEnumerable<TDest> MapEach<TSrc>(IEnumerable<TSrc> src)
        {
            return this.GetOrCreate().Map<IEnumerable<TSrc>, IEnumerable<TDest>>(src);
        }

        private IMapper GetOrCreate()
        {
            if (this.mapper == null)
            {
                lock (this.syncRoot)
                {
                    if (this.mapper == null)
                    {
                        var options = new MapperOptions();
                        this.Configure(options);

                        this.mapper = options.CreateMapper();
                    }
                }
            }

            return this.mapper;
        }
    }
}