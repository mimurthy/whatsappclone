using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseService
{
    public interface IDatabaseAdapter
    {
        Task<bool> InsertChatMessage(ChatMessage msg);

    }
}
