using System;

namespace Ukiyo.Infrastructure.WebApi.CQRS
{
    //Marker
    [AttributeUsage(AttributeTargets.Class)]
    public class PublicContractAttribute : Attribute
    {
    }
}