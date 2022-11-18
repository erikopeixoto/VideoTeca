﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VideoTeca.AcessoDados.Interfaces;

namespace VideoTeca.AcessoDados.Repositorios
{
    public abstract class GenericoRepositorio<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> dbSet;
        public GenericoRepositorio(DbContext contexto)
        {
            _context = contexto;
            this.dbSet = _context.Set<T>();
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
        // GET: api/Clientes
        public virtual async Task<List<T>> Listar()
        {
            var entity = await dbSet.AsNoTracking<T>().ToListAsync();
            return entity;
        }
        public virtual async Task<T> BuscarId(int id)
        {
            var entity = await dbSet.FindAsync(id);
            return entity;
        }
        public virtual async Task<List<T>> Listar(Expression<Func<T, bool>> where)
        {
            var entity = await dbSet.AsNoTracking<T>().Where<T>(where).ToListAsync<T>();
            return entity;
        }
        public virtual async Task<T> Alterar(T entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(TrataErro(ex));
            }
            return entity;
        }
        public virtual async Task<T> Incluir(T entity)
        {
            try
            {
                entity = _context.Set<T>().Add(entity).Entity;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(TrataErro(ex));
            }

        }
        public virtual async Task<T> Excluir(int id)
        {
            try
            {
                var entity = _context.Set<T>().Find(id);

                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(TrataErro(ex));
            }
        }
        private string TrataErro(Exception ex)
        {
            string sqlError = ex?.InnerException?.ToString();

            if (sqlError == null)
            {
                sqlError = ex.Message.ToString();
            }

            int LcolFim = sqlError.IndexOf((char)13);

            if (LcolFim != -1)
            {
                sqlError = sqlError.Substring(0, LcolFim);
            }
            return sqlError;
        }
    }
}
