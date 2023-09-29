using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Group3.Reponsitory;
using Lib;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Group3.Controllers
{
    public class FrontEndController : Controller
    {
        private readonly IContactDetail _contactService;

        public FrontEndController(IContactDetail contactService)
        {
            _contactService = contactService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        // Hiển thị trang liên hệ ở phía frontend
        public IActionResult Contact()
        {
            var contact = new Lib.Contact(); // Tạo một đối tượng Contact mới
                                             // Các thao tác khác bạn muốn thực hiện trên đối tượng contact ở đây (nếu cần)
            return View(contact); // Truyền đối tượng contact vào view
        }


        // Xử lý gửi thông tin liên hệ từ trang frontend tới backend
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                // Gửi thông tin liên hệ tới backend
                await _contactService.addContacts(contact);

                // Gửi email khi tạo liên hệ
                bool emailSent = await SendEmail(contact);

                if (emailSent)
                {
                    // Gửi email phản hồi sau khi lưu thông tin liên hệ thành công
                    bool responseEmailSent = await SendResponseEmail(contact);

                    if (responseEmailSent)
                    {
                        // Nếu email phản hồi được gửi thành công, bạn có thể thực hiện các hành động khác (nếu cần)
                    }
                    else
                    {
                        // Xử lý lỗi khi gửi email phản hồi không thành công
                    }
                }
                else
                {
                    // Xử lý lỗi khi gửi email không thành công
                }

                return RedirectToAction("Index", "Home"); // Chuyển hướng tới trang chính hoặc trang khác sau khi hoàn thành
            }
            return View(contact);
        }

        // Phương thức gửi email thông báo
        public async Task<bool> SendResponseEmail(Contact contact)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("dn169240@gmail.com", "Tincute1"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("dn169240@gmail.com"),
                    Subject = "Your Contact Form Submission",
                    Body = "Thank you for contacting us. We have received your message and will get back to you shortly.",
                };

                mailMessage.To.Add(contact.Email); // Sử dụng địa chỉ email của khách hàng làm địa chỉ người nhận

                await smtpClient.SendMailAsync(mailMessage);

                return true; // Gửi email phản hồi thành công
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi gửi email phản hồi không thành công
                return false;
            }
        }

        // Phương thức gửi email thông báo
        public async Task<bool> SendEmail(Contact contact)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("dn169240@gmail.com", "Tincute1"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("dn169240@gmail.com"),
                    Subject = "New Contact Form Submission",
                    Body = $"You have received a new contact form submission from {contact.Name}. Email: {contact.Email}. Message: {contact.Message}",
                };

                mailMessage.To.Add("dn169241@gmail.com"); // Thay đổi thành địa chỉ email của người nhận

                await smtpClient.SendMailAsync(mailMessage);

                return true; // Gửi email thành công
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi gửi email không thành công
                return false;
            }
        }

        // Các action khác
    }

}

