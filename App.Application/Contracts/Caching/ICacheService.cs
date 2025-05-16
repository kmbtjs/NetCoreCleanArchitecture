namespace App.Application.Contracts.Caching
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);

        Task<string?> GetAsync(string key);

        Task<byte[]?> GetByteAsync(string key);

        Task AddAsync<T>(string key, T value, TimeSpan? expirationTime = null);

        Task AddAsync(string key, string value, TimeSpan? expirationTime = null);

        Task RemoveAsync(string key);

        void Remove(string key);

        void FlushAll();
    }
}
