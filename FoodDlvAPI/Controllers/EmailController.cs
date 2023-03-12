using FoodDlvAPI.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace FoodDlvAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmailController : ControllerBase
	{
		private readonly AppDbContext _context;

		public EmailController(AppDbContext context)
		{
			_context = context;
		}
		[HttpPost]
		public IActionResult SendEmail(string body)
		{ 
			var email=new MimeMessage();
			email.From.Add(MailboxAddress.Parse("testfuen25@gmail.com"));
			email.To.Add(MailboxAddress.Parse("testfuen25@gmail.com"));
			email.Subject = "重設密碼";
			email.Body = new TextPart(TextFormat.Html) { Text = body };

			using var smtp = new SmtpClient();
			smtp.Connect("smtp.gmail.com",587,SecureSocketOptions.StartTls);
			smtp.Authenticate("testfuen25@gmail.com", "pymjkruxgrfjbisy");
			smtp.Send(email);
			smtp.Disconnect(true);
			return Ok();
		}
	}
}
