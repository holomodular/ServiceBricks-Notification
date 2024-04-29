//using System;
//using System.Net;
//using System.Threading.Tasks;
//using ServiceQuery;

//namespace ServiceBricks.Notification.EntityFrameworkCore
//{
//    /// <summary>
//    /// This will allow only one server to process a task at a time
//    /// as the number of application instances are increased.
//    /// </summary>
//    public class SingleServerProcessService : ISingleServerProcessService
//    {
//        private readonly INotifyLockApiService _notifyLockApiService;

//        public SingleServerProcessService(INotifyLockApiService notifyLockApiService)
//        {
//            _notifyLockApiService = notifyLockApiService;
//            HeartbeatTimeout = TimeSpan.FromMinutes(5);
//        }

//        public TimeSpan HeartbeatTimeout { get; set; }

//        public async Task<bool> ShouldThisServerRunForProcess(string processName)
//        {
//            string hostname = Dns.GetHostName();

//            string name = nameof(SingleServerProcessService) + ":" + processName;
//            var request = ServiceQueryRequestBuilder.New().IsEqual(nameof(NotifyLockDto.Name), name).Build();

//            var respQuery = await _notifyLockApiService.QueryAsync(request);
//            if (respQuery.Error || respQuery.Item == null)
//                return false;

//            //No server defined yet
//            if (respQuery.Item.List == null && respQuery.Item.List.Count == 0)
//            {
//                var respCreate = await _notifyLockApiService.CreateAsync(new NotifyLockDto()
//                {
//                    Name = nameof(SingleServerProcessService) + ":" + processName,
//                    Value = hostname
//                });
//                return respCreate.Success;
//            }

//            var existingItem = respQuery.Item.List.OrderBy(x => x.CreateDate).First();

//            //This server is primary
//            if (string.Compare(existingItem.Value, hostname, true) == 0)
//            {
//                //Update record so updatedate is written
//                var respUpdate = await _notifyLockApiService.UpdateAsync(existingItem);
//                return respUpdate.Success;
//            }

//            //Check if server offline
//            DateTimeOffset timeout = DateTimeOffset.UtcNow.Subtract(HeartbeatTimeout);
//            if (existingItem.UpdateDate <= timeout)
//            {
//                //Take over
//                existingItem.Value = hostname;
//                var respUpdate = await _notifyLockApiService.UpdateAsync(existingItem);
//                return respUpdate.Success;
//            }
//            return false;
//        }
//    }
//}