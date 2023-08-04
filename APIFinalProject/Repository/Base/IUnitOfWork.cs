using APIFinalProject.Models;

namespace APIFinalProject.Repository.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<UnitBuilding> unitBuildings { get; }
        IRepository<MenuPrice> menuPrices { get; }

        int CommitChange();
    }
}
