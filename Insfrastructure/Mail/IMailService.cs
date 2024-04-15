using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insfrastructure.Mail
{
    public interface IMailService
    {
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml);
        Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml);
    }
}
