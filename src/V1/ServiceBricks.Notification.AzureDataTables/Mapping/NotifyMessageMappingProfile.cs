using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// Mapping profile for NotifyMessage and NotifyMessageDto.
    /// </summary>
    public partial class NotifyMessageMappingProfile
    {
        /// <summary>
        /// Register the mapping
        /// </summary>
        public static void Register(IMapperRegistry registry)
        {
            registry.Register<NotifyMessage, NotifyMessageDto>(
                (s, d) =>
                {
                    d.BccAddress = s.BccAddress;
                    d.Body = s.Body;
                    d.BodyHtml = s.BodyHtml;
                    d.CcAddress = s.CcAddress;
                    d.CreateDate = s.CreateDate;
                    d.FromAddress = s.FromAddress;
                    d.FutureProcessDate = s.FutureProcessDate;
                    d.IsComplete = s.IsComplete;
                    d.IsError = s.IsError;
                    d.IsHtml = s.IsHtml;
                    d.IsProcessing = s.IsProcessing;
                    d.Priority = s.Priority;
                    d.ProcessDate = s.ProcessDate;
                    d.ProcessResponse = s.ProcessResponse;
                    d.RetryCount = s.RetryCount;
                    d.SenderType = s.SenderType;
                    d.StorageKey =
                        s.PartitionKey +
                        StorageAzureDataTablesConstants.STORAGEKEY_DELIMITER +
                        s.RowKey;
                    d.Subject = s.Subject;
                    d.ToAddress = s.ToAddress;
                    d.UpdateDate = s.UpdateDate;
                });

            registry.Register<NotifyMessageDto, NotifyMessage>(
                (s, d) =>
                {
                    d.BccAddress = s.BccAddress;
                    d.Body = s.Body;
                    d.BodyHtml = s.BodyHtml;
                    d.CcAddress = s.CcAddress;
                    //d.CreateDate ignore
                    //d.ETag ignore
                    d.FromAddress = s.FromAddress;
                    d.FutureProcessDate = s.FutureProcessDate;
                    d.IsComplete = s.IsComplete;
                    d.IsError = s.IsError;
                    d.IsHtml = s.IsHtml;
                    d.IsProcessing = s.IsProcessing;
                    d.Priority = s.Priority;
                    d.ProcessDate = s.ProcessDate;
                    d.ProcessResponse = s.ProcessResponse;
                    d.RetryCount = s.RetryCount;
                    d.SenderType = s.SenderType;
                    d.Subject = s.Subject;
                    //d.Timestamp ignore
                    d.ToAddress = s.ToAddress;
                    d.UpdateDate = s.UpdateDate;
                    if (!string.IsNullOrEmpty(s.StorageKey))
                    {
                        string[] tempStorageKey = s.StorageKey.Split(StorageAzureDataTablesConstants.STORAGEKEY_DELIMITER);
                        if (tempStorageKey.Length >= 1)
                            d.PartitionKey = tempStorageKey[0];
                        if (tempStorageKey.Length >= 2)
                            d.RowKey = tempStorageKey[1];
                    }
                });
        }
    }
}