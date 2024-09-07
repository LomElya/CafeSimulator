using System;

namespace Modification
{
    public interface IModificationPlacement
    {
        event Action<Type, int> Upgrading;
    }
}

