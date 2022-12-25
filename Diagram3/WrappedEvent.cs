using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram3
{
    internal class WrappedEvent
    {
        public event Action<object> _event;
        public void Add(Action<object> func)
        {
            _event += func;
        }
        public void Invoke(object obj)
        {
            _event?.Invoke(obj);
        }
    }
}
