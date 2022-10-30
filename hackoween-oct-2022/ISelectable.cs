using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackoween_oct_2022
{
    public enum Type { Frame, Action};
    public interface ISelectable
    {
        Type GetType();
        string GetName();
    }
}
