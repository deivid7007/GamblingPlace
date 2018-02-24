using GP.DB;
using GP.LogService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GP.LogService.Domain;

namespace GP.LogService
{
    public sealed class Logger : ILog
    {

        private GPDbContext _ctx = new GPDbContext();


        /*Lazy objects are thread safe*/
        private static readonly Lazy<Logger> instance =
            new Lazy<Logger>(() => new Logger());

        public static Logger GetInstance
        {
            get { return instance.Value; }
        }

        public async Task LogCustomExceptionAsync(Exception ex, string id = null)
        {
            CustomException exception = new CustomException(ex, id);
            _ctx.CustomExceptions.Add(exception);
            await _ctx.SaveChangesAsync();
        }

        public Task LogSendedEmailAsync()
        {
            throw new NotImplementedException();
        }

        public Task LogPerformerRequestAsync()
        {
            throw new NotImplementedException();
        }

        public Task LogVenueOwnerRequestAsync()
        {
            throw new NotImplementedException();
        }

    }
}
