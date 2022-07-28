using Provider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Interface
{
     interface IBusinessProcess
    {
         Response Check<T>(T inData);

         Response Pay<T>(T inData);

         Response State<T>(T inData);

    }
}
