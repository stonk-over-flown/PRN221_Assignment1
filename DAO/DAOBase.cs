using BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAOBase<T> where T : class
    {
        WPFShopManagementDBContext _context;
        DbSet<T> _dbSet;

        private static DAOBase<T> _instance;

        public DAOBase()
        {
            _context = new WPFShopManagementDBContext();
            _dbSet = _context.Set<T>();
        }

        public static DAOBase<T> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DAOBase<T>();
                }
                return _instance;
            }
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public bool Add(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(T entity, T baseValue)
        {
            try
            {
                _context.Entry(baseValue).CurrentValues.SetValues(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool Delete(T entity)
        {
            try
            {
                _context.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
