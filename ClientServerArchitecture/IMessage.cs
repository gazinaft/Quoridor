using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerArchitecture
{
    public interface IMessage
    {
        string Title { get; set; }

        int X { get; set; }

        int Y { get; set; }

        string Message { get; set; }

        bool IsHorisontal { get; set; }
    }
}
