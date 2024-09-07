
namespace Modification
{
    public interface IModification<T>
    {
        T CurrentModificationValue { get; }
    }
}