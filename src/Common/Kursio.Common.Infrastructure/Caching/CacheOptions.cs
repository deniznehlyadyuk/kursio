using Microsoft.Extensions.Caching.Distributed;

namespace Kursio.Common.Infrastructure.Caching;

public static class CacheOptions
{
    private static DistributedCacheEntryOptions DefaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
    };

    public static DistributedCacheEntryOptions Create(TimeSpan? expiration)
    {
        return expiration is not null ?
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration } :
            DefaultExpiration;
    }
}
