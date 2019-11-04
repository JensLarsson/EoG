using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface IAbility
{
    void IExecute(); //This is unnecessary since the implementation could easily be implemented in IUpdate
    void IStart();
    void IUpdate();
    void IDisable();
}

