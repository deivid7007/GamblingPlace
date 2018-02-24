using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GP.LogService.Domain.Models
{
    public class CustomException : Exception
    {
            private string _id;

            public CustomException() { }
            public CustomException(Exception ex, string id = null)
            {
                this.CustomMessage = ex.Message;
                this.CustomStackTrace = ex.StackTrace;
                this.CustomInnerMessage = ex.InnerException != null ? ex.InnerException.Message : String.Empty;
                this.CustomInnerStackTrace = ex.InnerException != null ? ex.InnerException.StackTrace : String.Empty;
                this.DateCreated = DateTime.Now;

                this._id = id ?? new Guid().ToString();
            }

            [Key]
            public string Id { get; set; }

            public string CustomMessage { get; set; }

            public string CustomStackTrace { get; set; }

            public string CustomInnerMessage { get; set; }

            public string CustomInnerStackTrace { get; set; }

            public DateTime DateCreated { get; set; }
        }
}
