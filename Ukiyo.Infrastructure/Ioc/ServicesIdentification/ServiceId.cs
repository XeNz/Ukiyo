using System;

namespace Ukiyo.Infrastructure.Ioc
{
    public class ServiceId : IServiceId
    {
        public string Id { get; } = $"{Guid.NewGuid():N}";
    }
}