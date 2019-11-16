namespace TRG.Extensions.ObjectMapping
{
    using System.Collections.Generic;

    public interface IMapper<out TDest>
    {
        TDest Map<TSrc>(TSrc src);

        IEnumerable<TDest> MapEach<TSrc>(IEnumerable<TSrc> src);
    }
}