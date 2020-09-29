using System.Threading.Tasks;

namespace IdentityAPIWithoutEF.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}