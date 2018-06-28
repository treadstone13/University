using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.DI.Interfaces;
using University.Models;

namespace University.DI.Implementation
{
    public class RequestRepository : IRequestRepository
    {
        private UniversityContext ctx;
        public RequestRepository(UniversityContext universityContext)
        {
            this.ctx = universityContext;
        }
        public IEnumerable<Request> GetRequests()
        {
            var request = ctx.Requests.ToList();
            return request;
        }
        public Request GetRequest(int id)
        {
            var r = ctx.Requests.Find(id);
            return r;
        }
    }
}
