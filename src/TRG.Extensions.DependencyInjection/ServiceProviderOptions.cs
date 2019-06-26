namespace TRG.Extensions.DependencyInjection
{
    public class ServiceProviderOptions
    {
        internal static readonly ServiceProviderOptions Default = new ServiceProviderOptions();

        /// <summary>
        /// Whether the service provider should verify the container.
        /// <para>Default is set to <value>false</value>.</para>
        /// </summary>
        public bool VerifyOnBuild { get; set; } = false;

        /// <summary>
        /// Whether the service provider should register itself to the <see cref="IServiceProvider"/> interface as a singleton.
        /// <para>Default is set to <value>true</value>.</para>
        /// </summary>
        public bool SelfRegister { get; set; } = true;

        /// <summary>
        /// Whether the dependency descriptors will register by their <see cref="DependencyDescriptor.Priority"/> property.
        /// If disabled, descriptors will register by the order in the collection provided.
        /// <para>Default is set to <value>false</value>.</para>
        /// </summary>
        public bool SupportPriority { get; set; } = false;
    }
}