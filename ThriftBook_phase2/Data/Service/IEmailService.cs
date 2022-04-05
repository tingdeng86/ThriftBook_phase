using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Models;

namespace ThriftBook_phase2.Data.Service
{
    public interface IEmailService
    {
        Task<Response> SendSingleEmail(ComposeEmailModel payload);
    }
}
