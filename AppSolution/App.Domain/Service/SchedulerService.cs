using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Service
{
    public class DataRefreshService : BackgroundService
    {
        //private readonly MessageQueueService _messageQueueService;

        //public DataRefreshService(MessageQueueService messageQueueService)
        //{
        //    _messageQueueService = messageQueueService;
        //}

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {


                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }
    }
}
