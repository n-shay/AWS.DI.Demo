namespace TRG.Extensions.Settings
{
    public interface ISettings<out T>
    {
        T Value { get; }
    }
}