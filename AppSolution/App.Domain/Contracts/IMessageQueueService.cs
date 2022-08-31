using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Contracts
{
    public interface IMessageQueueService
    {
        bool PostMessageQueue<T>(T Object, int fila);
        //bool ProcessMessageQueue(int fila);
    }
}
