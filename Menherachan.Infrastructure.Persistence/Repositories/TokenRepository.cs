using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Menherachan.Application.Interfaces.Repositories;
using Menherachan.Domain.Database;
using Menherachan.Domain.Entities.DBOs;
using Microsoft.EntityFrameworkCore;

namespace Menherachan.Infrastructure.Persistence.Repositories
{
    public class TokenRepository : BaseRepository, ITokenRepository
    {
        private DbSet<Token> _tokens;
        
        public TokenRepository(ApplicationDbContext context) : base(context)
        {
            _tokens = context.Tokens;
        }
        
        public async Task<IEnumerable<Token>> GetDataAsync()
        {
            return await _tokens.ToListAsync();
        }

        public async Task<IEnumerable<Token>> GetDataWithConditionAsync(Expression<Func<Token, bool>> condition)
        {
            return await _tokens.Where(condition).ToListAsync();
        }

        public async Task<IEnumerable<Token>> GetDataWithIncludedAsync()
        {
            return await _tokens.ToListAsync();
        }

        public async Task<IEnumerable<Token>> GetDataWithConditionAndIncludedAsync(Expression<Func<Token, bool>> condition)
        {
            return await _tokens.Where(condition).ToListAsync();
        }

        public async Task DeleteAsync(Token entity)
        {
            _tokens.Remove(entity);
            await this.Context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<Token> entities)
        {
            _tokens.RemoveRange(entities);
            await this.Context.SaveChangesAsync();
        }

        public async Task AddAsync(Token entity)
        {
            await _tokens.AddAsync(entity);
            await this.Context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Token> entities)
        {
            await _tokens.AddRangeAsync(entities);
            await this.Context.SaveChangesAsync();
        }

        public async Task<Token> GetToken(string token)
        {
            return await _tokens.FirstAsync(t => t.TokenString == token);
        }
    }
}