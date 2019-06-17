namespace TRG.Extensions.DependencyInjection
{
    public class ServiceProviderOptions
    {
        internal static readonly ServiceProviderOptions Default = new ServiceProviderOptions();

        public ServiceProviderOptions() : this(true)
        {
        }

        public ServiceProviderOptions(bool verifyOnBuild)
        {
            VerifyOnBuild = verifyOnBuild;
        }

        public bool VerifyOnBuild { get; set; }
    }
}