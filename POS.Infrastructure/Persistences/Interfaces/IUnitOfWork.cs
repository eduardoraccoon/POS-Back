namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Declaración de matricula de nuestra interface a nivel de repository
        ICategoryRepository Category { get; }
        IUserRepository User { get; }
        IClientRepository Client { get; }
        IProviderRepository Provider { get; }
        IProductRepository Product { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
