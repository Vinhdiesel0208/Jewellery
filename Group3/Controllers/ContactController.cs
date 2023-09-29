using System;
using System.Threading.Tasks;
using Group3.Reponsitory;
using Lib;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using Group3.SmtpConfig; // Thêm using cho không gian tên SmtpConfig
using Microsoft.Extensions.Logging;

namespace Group3.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactDetail _contactService;
        private readonly Group3.SmtpConfig.SmtpConfig _smtpConfigOptions;
        private readonly ILogger<ContactController> _logger; // Khai báo ILogger

        public ContactController(
            IContactDetail contactService,
            IOptions<Group3.SmtpConfig.SmtpConfig> smtpConfig,
            ILogger<ContactController> logger) // Inject ILogger vào constructor
        {
            _contactService = contactService;
            _smtpConfigOptions = smtpConfig.Value;
            _logger = logger; // Khởi tạo ILogger
        }

        // Hiển thị trang liên hệ
        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetContactList();
            return View(contacts);
        }

        // Hiển thị thông tin chi tiết liên hệ
        public async Task<IActionResult> Details(int id)
        {
            var contact = await _contactService.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // Hiển thị trang tạo liên hệ
        public IActionResult Create()
        {
            return View();
        }

        // Xử lý tạo liên hệ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
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

                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // Phương thức gửi email thông báo
        public async Task<bool> SendResponseEmail(Contact contact)
        {
            try
            {
                // Kiểm tra giá trị _smtpConfig.SmtpUsername và contact.Email
                if (string.IsNullOrEmpty(_smtpConfigOptions.SmtpUsername) || string.IsNullOrEmpty(contact.Email))
                {
                    // Sử dụng logger để in thông báo lỗi và giá trị
                    _logger.LogError("SmtpUsername or contact.Email is null or empty. SmtpUsername: {SmtpUsername}, Email: {Email}", _smtpConfigOptions.SmtpUsername, contact.Email);
                    return false;
                }

                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Your Name", _smtpConfigOptions.SmtpUsername));
                email.To.Add(new MailboxAddress("", contact.Email)); // Địa chỉ email người nhận
                email.Subject = "Your Contact Form Submission";

                var builder = new BodyBuilder();
                builder.TextBody = "Thank you for contacting us. We have received your message and will get back to you shortly.";
                email.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpConfigOptions.SmtpServer, _smtpConfigOptions.SmtpPort, false);
                    await client.AuthenticateAsync(_smtpConfigOptions.SmtpUsername, _smtpConfigOptions.SmtpPassword);
                    await client.SendAsync(email);
                    await client.DisconnectAsync(true);
                }

                return true; // Gửi email phản hồi thành công
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending response email."); // Ghi log lỗi
                return false; // Xử lý lỗi khi gửi email phản hồi không thành công
            }
        }

        // Phương thức gửi email thông báo
        public async Task<bool> SendEmail(Contact contact)
        {
            try
            {
                // Kiểm tra giá trị _smtpConfig.SmtpUsername và contact.Email
                if (string.IsNullOrEmpty(_smtpConfigOptions.SmtpUsername) || string.IsNullOrEmpty(contact.Email))
                {
                    // Sử dụng logger để in thông báo lỗi và giá trị
                    _logger.LogError("SmtpUsername or contact.Email is null or empty. SmtpUsername: {SmtpUsername}, Email: {Email}", _smtpConfigOptions.SmtpUsername, contact.Email);
                    return false;
                }

                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Your Name", _smtpConfigOptions.SmtpUsername));
                email.To.Add(new MailboxAddress("", "bellpham97@gmail.com")); // Thay đổi thành địa chỉ email của người nhận
                email.Subject = "New Contact Form Submission";

                var builder = new BodyBuilder();
                builder.TextBody = $"You have received a new contact form submission from {contact.Name}. Email: {contact.Email}. Message: {contact.Message}";
                email.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpConfigOptions.SmtpServer, _smtpConfigOptions.SmtpPort, false);
                    await client.AuthenticateAsync(_smtpConfigOptions.SmtpUsername, _smtpConfigOptions.SmtpPassword);
                    await client.SendAsync(email);
                    await client.DisconnectAsync(true);
                }

                return true; // Gửi email thành công
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email."); // Ghi log lỗi
                return false; // Xử lý lỗi khi gửi email không thành công
            }
        }


        // Trang hiển thị thành công khi gửi email phản hồi
        public IActionResult EmailSentSuccess()
        {
            return View();
        }

        // Trang hiển thị lỗi khi gửi email phản hồi không thành công
        public IActionResult EmailSentError()
        {
            return View();
        }
    }
}
