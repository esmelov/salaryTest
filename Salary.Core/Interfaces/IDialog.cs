using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salary.Core.Interfaces
{
    public interface IDialog
    {
        void AlertMessage(string message);
        void InfoMessage(string message);
        void ErrortMessage(string message);
    }
}
