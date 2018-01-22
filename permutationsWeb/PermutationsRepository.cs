using permutationsWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace permutationsWeb
{
    public class PermutationsRepository
    {
        private readonly DatabaseContext _context;

        public PermutationsRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public PermutationsResult Get(String request)
        {
            var orderedString = this.GetOrderedString(request);
            return this._context.PermutationsResults
                .Where(r => r.Request.Equals(orderedString, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
        }

        public void Add(String request, String permutationsJson, Double seconds)
        {
            var orderedString = this.GetOrderedString(request);
            this._context.Add(new PermutationsResult
            {
                Request = orderedString,
                PermutationsJson = permutationsJson,
                Seconds = seconds
            });
            this._context.SaveChanges();
        }

        private String GetOrderedString(String request)
        {
            return String.Concat(request.OrderBy(c => c));
        }
    }
}
