using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    interface IMailService
    {
        void Send(string subject, string message);

    }
}
