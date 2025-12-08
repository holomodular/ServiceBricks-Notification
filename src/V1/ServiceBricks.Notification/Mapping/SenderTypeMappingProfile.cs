namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a mapping profile for the SenderType.
    /// </summary>
    public partial class SenderTypeMappingProfile
    {
        /// <summary>
        /// Register the mapping
        /// </summary>
        public static void Register(IMapperRegistry registry)
        {
            registry.Register<SenderType, DomainTypeDto>(
                (s, d) =>
                {
                    d.Name = s.Name;
                    d.StorageKey = s.Key;
                });

            registry.Register<DomainTypeDto, SenderType>(
                (s, d) =>
                {
                    d.Name = s.Name;
                    d.Key = s.StorageKey;
                });
        }
    }
}