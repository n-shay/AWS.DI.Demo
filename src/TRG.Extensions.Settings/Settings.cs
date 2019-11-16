namespace TRG.Extensions.Settings
{
    using System;

    public class Settings<T> : ISettings<T>
    {
        private readonly object syncRoot = new object();
        private readonly Func<T> valueFactory;
        private T value;

        public Settings(Func<T> valueFactory)
        {
            this.valueFactory = valueFactory;
        }

        public T Value => this.GetOrCreate();

        private T GetOrCreate()
        {
            if(this.value == null)
            {
                lock (this.syncRoot)
                {
                    if (this.value == null)
                    {
                        this.value = this.valueFactory();
                    }
                }
            }

            return this.value;
        }
    }
}