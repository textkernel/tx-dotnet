using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textkernel.Tx.SDK.Examples
{
    internal interface IExample
    {
        Task Run(TxClient client);
    }
}
