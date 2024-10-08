﻿using AutoMapper;

using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// Mapping profile for NotifyMessage and NotifyMessageDto.
    /// </summary>
    public partial class NotifyMessageMappingProfile : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotifyMessageMappingProfile()
        {
            // AI: Create a automapper mapping for the NotifyMessage and NotifyMessageDto.
            CreateMap<NotifyMessageDto, NotifyMessage>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.PartitionKey, y => y.MapFrom<PartitionKeyResolver>())
                .ForMember(x => x.RowKey, y => y.MapFrom<RowKeyResolver>())
                .ForMember(x => x.ETag, y => y.Ignore())
                .ForMember(x => x.Timestamp, y => y.Ignore())
                .ForMember(x => x.Key, y => y.Ignore());

            CreateMap<NotifyMessage, NotifyMessageDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom<StorageKeyResolver>());
        }
    }
}