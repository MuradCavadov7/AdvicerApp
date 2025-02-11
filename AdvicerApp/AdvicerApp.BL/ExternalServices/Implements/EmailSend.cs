using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using AdvicerApp.BL.Helper;
using Microsoft.EntityFrameworkCore;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.Core.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Identity;
using AdvicerApp.BL.ExternalServices.Interfaces;

namespace AdvicerApp.BL.ExternalServices.Implements;

public class EmailSend : IEmailSend
{
    readonly SmtpClient _smtpClient;
    readonly MailAddress _from;
    readonly HttpContext _context;
    readonly IMemoryCache _cache;
    private readonly UserManager<User> _userManager;
    public EmailSend(IOptions<SmtpOptions> option, IHttpContextAccessor acc, IMemoryCache cache, UserManager<User> userManager)
    {
        var opt = option.Value;
        _smtpClient = new(opt.Host, opt.Port);
        _smtpClient.Credentials = new NetworkCredential(opt.Sender, opt.Password);
        _smtpClient.EnableSsl = true;
        _cache = cache;
        _userManager = userManager;
        _from = new MailAddress(opt.Sender, "AdvicerApp");
        _context = acc.HttpContext;
    }
    public async Task SendEmailAsync(string reciever, string name, string code)
    {
        MailAddress to = new(reciever);
        MailMessage message = new MailMessage(_from, to);
        message.IsBodyHtml = true;
        message.Subject = "Your Verification CODE";
        message.Body = EmailTemplates.VerifyEmail.Replace("__$name", name).Replace("__$code",code);
        await _smtpClient.SendMailAsync(message);
        _smtpClient.Dispose();
    }

}
