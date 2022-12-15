using Quartz;
using Quartz.Impl.AdoJobStore.Common;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Models;

namespace WorkerService.Scheduler
{
    internal class MyScheduler : IHostedService, IDisposable
    {
        public IScheduler Scheduler { get; set; }

        private readonly IJobFactory _jobFactory;
        private readonly JobMetadata _jobMetadata;
        private readonly ISchedulerFactory _schedulerFactory;

        public MyScheduler(IJobFactory jobFactory, ISchedulerFactory schedulerFactory, JobMetadata jobMetadata)
        {
            _jobFactory = jobFactory;
            _jobMetadata = jobMetadata;
            _schedulerFactory = schedulerFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Create scheduler
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            // Create job
            IJobDetail jobDetail = CreateJob(_jobMetadata);

            // Create trigger
            ITrigger trigger = CreateTrigger(_jobMetadata);

            // Schedule job
            await Scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
            await Scheduler.Start(cancellationToken);

        }

        private ITrigger CreateTrigger(JobMetadata jobMetadata)
        {
            return TriggerBuilder.Create()
                .WithIdentity(jobMetadata.JobId.ToString())
                .WithCronSchedule(jobMetadata.CronExpression)
                .WithDescription(jobMetadata.JobName)
                .Build();
        }

        private IJobDetail CreateJob(JobMetadata jobMetadata)
        {
            return JobBuilder.Create(jobMetadata.JobType)
                .WithIdentity(jobMetadata.JobId.ToString())
                .WithDescription(jobMetadata.JobName)
                .Build();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
