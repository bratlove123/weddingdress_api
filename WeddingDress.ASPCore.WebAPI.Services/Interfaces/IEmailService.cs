using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeddingDress.ASPCore.WebAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
