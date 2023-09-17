using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService
{
    public interface IProducer
    {
        public void Initialize();

        public void Produce(string msg);
    }
}
