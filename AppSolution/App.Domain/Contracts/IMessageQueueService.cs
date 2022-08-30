using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Contracts
{
    public interface IMessageQueueService
    {
        Task<bool> PostMessageQueue<T>(T Object, int fila);
        Task<bool> ProcessMessageQueue<T>(int fila);
    }
}
