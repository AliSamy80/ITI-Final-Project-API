using APIFinalProject.Models;
using APIFinalProject.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Linq.Expressions;

namespace APIFinalProject.Repository
{
    public class MainRepository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;

        public MainRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }

        public async void Delete(int id)
        {
            T x = await _context.Set<T>().FindAsync(id);
            if (x != null)
            {
                _context.Set<T>().Remove(x);
                _context.SaveChanges();
            }

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(params string[] args)
        {
           IQueryable<T> query =_context.Set<T>();
            if(args != null && args.Length > 0)
            {
                foreach(string property in args)
                {
                    query=query.Include(property);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetOne(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> SelectGroup(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().Where(match).ToListAsync();
        }

        public void Update(T item)
        {

            _context.Set<T>().Update(item);
            _context.SaveChanges();
        }

        //[HttpGet("{id}", Name = "GetOneDept")]
        //public IActionResult GetDeptWithEmpNames(int id)
        //{
        //    Cat DeptEmpDTO = new DeptWithListEmpInfo();
        //    Department Dept = context.Departments.Include(e => e.Employees).FirstOrDefault(d => d.ID == id);
        //    DeptEmpDTO.DeptName = Dept.Name;
        //    DeptEmpDTO.DeptId = Dept.ID;

        //    foreach (var item in Dept.Employees)
        //    {
        //        DeptEmpDTO.EmployeeNames.Add(item.Name);
        //    }

        //    return Ok(DeptEmpDTO);


        //}


        //public void Update(T item)
        //{
        //   _context.Set<T>().Update(item);
        //    _context.SaveChanges();
        //}
    }

}
