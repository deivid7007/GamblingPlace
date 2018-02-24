using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GP.LogService.Domain
{
    public interface ILog
    {
        Task LogCustomExceptionAsync(Exception ex, string id);
        Task LogSendedEmailAsync();
        Task LogPerformerRequestAsync();
        Task LogVenueOwnerRequestAsync();
    }
}
