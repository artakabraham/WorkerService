using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using WorkerService;
using WorkerService.JobFactory;
using WorkerService.Jobs;
using WorkerService.Models;
using WorkerService.Scheduler;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        //services.AddHostedService<MyService>();
        services.AddSingleton<IJobFactory, JobFactory>();
        services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
        services.AddSingleton<Notification>();

        services.AddSingleton(new JobMetadata(Guid.NewGuid(), typeof(Notification), "Notify job", "0/10 * * * * ?"));

        services.AddHostedService<MyScheduler>();
    })
    .Build();

await host.RunAsync();
