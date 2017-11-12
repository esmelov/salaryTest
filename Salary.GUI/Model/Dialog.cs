using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salary.Core.Interfaces;

namespace Salary.GUI.Model
{
    public class Dialog : IDialog
    {
        public void AlertMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void ErrortMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void InfoMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
