using System;

namespace Ukiyo.Infrastructure.Ioc.ServicesIdentification
{
    public class ServiceId : IServiceId
    {
        public string Id { get; } = $"{Guid.NewGuid():N}";
    }
}