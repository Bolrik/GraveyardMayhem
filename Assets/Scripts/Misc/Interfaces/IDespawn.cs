using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc.Interfaces
{
    public interface IDespawn
    {
        void Despawn();
    }

    public interface IDespawn<T> : IDespawn
    {
        Action<T> OnDespawn { get; set; }
    }
}
