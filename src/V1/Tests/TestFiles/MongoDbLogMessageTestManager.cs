using Microsoft.Extensions.DependencyInjection;
using ServiceQuery;
using ServiceBricks.Logging;

namespace ServiceBricks.Xunit
{
    public class MongoDbLogMessageTestManager : LogMessageTestManager
    {
        public override LogMessageDto GetObjectNotFound()
        {
            return new LogMessageDto()
            {
                StorageKey = "000000000000000000000000"
            };
        }
    }
}