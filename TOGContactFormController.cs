/*The MIT License (MIT)

Copyright (c) The Other Guys Ltd http://whytheotherguys.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/

using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TOG.Controllers
{
    public class TOGContactFormController : Controller
    {
        [HttpPost]
        public ActionResult TOGContactFormSubmit(TOG.Models.TOGContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SmtpClient client = new SmtpClient())
                    {
                        client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["mailPort"]);
                        client.Host = ConfigurationManager.AppSettings["mailHost"];
                        client.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["mailEnableSsl"]);
                        client.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["mailTimeout"]);
                        client.UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["mailUseDefaultCredentials"]);
                        client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["mailUserName"], ConfigurationManager.AppSettings["mailPassword"]);

                        model.Name = HttpUtility.HtmlEncode(model.Name);
                        model.Email = HttpUtility.HtmlEncode(model.Email);
                        model.Message = HttpUtility.HtmlEncode(model.Message);

                        MailMessage message = new MailMessage(ConfigurationManager.AppSettings["mailFromAddress"], ConfigurationManager.AppSettings["mailToAddress"]);
                        message.IsBodyHtml = true;
                        message.SubjectEncoding = Encoding.UTF8;
                        message.BodyEncoding = Encoding.UTF8;
                        message.Subject = "Contact Request";
                        message.Body = "<table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 70%; color: #333333; font-family: Arial; border: 1px solid #6699CC; border-collapse: collapse\">" +
                                       "    <tr>" +
                                       "        <td colspan=\"2\" style=\"background-color: #6699CC; text-align: center; padding: 15px\">" +
                                       "            <span style=\"color: #ffffff; font-size: 30px;\"><strong>Contact Request</strong></span>" +
                                       "        </td>" +
                                       "    </tr>" +
                                       "    <tr>" +
                                       "        <td style=\"width: 0%; padding: 15px\"><strong>Name</strong></td>" +
                                       "        <td style=\"width: 100%; padding: 15px 15px 15px 0\">" + model.Name + "</td>" +
                                       "    </tr>" +
                                       "    <tr>" +
                                       "        <td style=\"width: 0%; padding: 0 15px 15px 15px\"><strong>Email</strong></td>" +
                                       "        <td style=\"width: 100%; padding: 0 15px 15px 0\">" + model.Email + "</td>" +
                                       "    </tr>" +
                                       "    <tr>" +
                                       "        <td style=\"width: 0%; padding: 0 15px 15px 15px; vertical-align: top\"><strong>Message</strong></td>" +
                                       "        <td style=\"width: 100%; padding: 0 15px 15px 0\">" + model.Message + "</td>" +
                                       "    </tr>" +
                                       "</table>";
                        client.Send(message);
                    }

                    return Json(new { type = "success" });
                }
                catch (Exception ex)
                {
                    return Json(new { error = ex.Message });
                }
            }
            else
            {
                return Json(new { error = "Model state not valid" });
            }
        }
    }
}