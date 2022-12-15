using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Jobs
{
    internal class Notification : IJob
    {
        private readonly ILogger<Notification> _logger;

        public Notification(ILogger<Notification> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Notify user at {DateTime.Now} and JobTYpe: {context.JobDetail.JobType}");
            return Task.CompletedTask;
        }
    }
}
