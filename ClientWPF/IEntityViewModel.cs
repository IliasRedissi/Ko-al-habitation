using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF
{
    public interface IEntityViewModel<T> where T : class
    {
        void SetCurrentEntity(T entity);
    }
}
