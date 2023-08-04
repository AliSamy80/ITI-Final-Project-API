using APIFinalProject.Models;
using APIFinalProject.Repository.Base;
using System.ComponentModel.Design;

namespace APIFinalProject.Repository
{
    public class UnitOFWork : IUnitOfWork
    { 

        private readonly DataContext _context;
        public IRepository<UnitBuilding> unitBuildings { get; set; }

        public IRepository<MenuPrice> menuPrices { get; set; }
        public UnitOFWork(DataContext context )
        {
            _context = context;
            unitBuildings =new MainRepository<UnitBuilding>(_context);
            menuPrices = new MainRepository<MenuPrice>(_context); 
        }
        

        public int CommitChange()
        {
           return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
