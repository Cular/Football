using Models.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Registration
{
    public interface IRegisterNotifier
    {
        Task SendRegisterInfo(Player player);
    }
}
