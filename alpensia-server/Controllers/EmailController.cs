using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using alpensia_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace alpensia_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Ok");
        }

        [HttpPost("emailSend")]
        public ActionResult EmailSend([FromBody] EmailModel model)
        {
            using (var client = new SmtpClient("localhost", 25))
            {
                //보내는 사람도 메일 변경
                var from = new MailAddress("cyk8762@alpensiaresort.co.kr", model.Name, Encoding.UTF8);

                //업로드전 메일로 교체(cyk8762@alpensiaresort.co.kr)
                var to = new MailAddress("cyk8762@alpensiaresort.co.kr");

                using (var message = new MailMessage(from, to))
                {
                    message.SubjectEncoding = Encoding.UTF8;
                    message.BodyEncoding = Encoding.UTF8;

                    message.Subject = $"[알펜시아 홈페이지] {model.Name} 님의 CONTACT";

                    var stringBuilder = new StringBuilder();

                    var now = DateTimeOffset.Now;
                    stringBuilder.AppendLine($"{now.ToString("yyyy/MM/dd HH:mm:ss")}에 보낸 문의메일 입니다.");
                    stringBuilder.AppendLine($"보낸 이 :{model.Name} ");
                    stringBuilder.AppendLine($"연락처 :{model.Phone}");

                    message.Body = stringBuilder.ToString();

                    client.Send(message);
                }
            }
            return Ok();
        }
    }
}