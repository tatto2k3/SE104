using Scriban;
using Syncfusion.Drawing;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Security;
using System.Net.Mail;
namespace BlueStarMVC.Pages.Server
{
    public class UtilsEmail
    {
        public static PdfDocument CreatePdfDocument(long amount, string CustomerName,
            string CustomerIdentify, string SeatID, string FlightID, string DepeartureDay, string ArriveDay, string DepeartureTime
        , string ArriveTime, string CustomerPhone, string TimeDuration, string TripType)
        {
            string TemplatePath;
            string templateRendered = "";
            if (TripType == "oneWay")
            {
                TemplatePath = @"Pages/Server/Templates/Ticket.html";
            }
            else
            {
                TemplatePath = @"Pages/Server/Templates/Ticket2.html";
            }
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();
            BlinkConverterSettings blinkConverterSettings = new BlinkConverterSettings();
            blinkConverterSettings.ViewPortSize = new Size(800, 200);
            htmlConverter.ConverterSettings = blinkConverterSettings;
            var data = File.ReadAllText(TemplatePath);
            var template = Template.Parse(data);
            templateRendered = template.Render(new
            {
                departuretime = DepeartureTime,
                departureday = DepeartureDay,
                duration = TimeDuration,
                arrivaltime = ArriveTime,
                arrivalday = ArriveDay,
                originalprice = amount,
                customername = CustomerName,
                customeridentify = CustomerIdentify,
                seatid = SeatID,
                flightid = FlightID,
                customerphone = CustomerPhone
            });

            PdfDocument document = htmlConverter.Convert(templateRendered, "");
            PdfPage page = document.Pages.Add();

            PdfGraphics graphics = page.Graphics;
            PdfSecurity security = document.Security;
            PdfBrush brush = PdfBrushes.Black;
            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 20f, PdfFontStyle.Bold);

            security.KeySize = PdfEncryptionKeySize.Key128Bit;
            security.Algorithm = PdfEncryptionAlgorithm.RC4;
            security.UserPassword = CustomerIdentify;
            //Draw the text.
            graphics.DrawString("", font, brush, new PointF(0, 40));
            return document;
        }
        public static void SendEMail(string from, string recipients, string subject, string body, Attachment attachment, string username, string password)
        {
            try
            {


                MailMessage emailMessage = new MailMessage(from, recipients);

                emailMessage.Subject = subject;

                emailMessage.IsBodyHtml = true;
                emailMessage.Body = body;

                emailMessage.Attachments.Add(attachment);

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Credentials = new System.Net.NetworkCredential(username, password);
                    smtp.Send(emailMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static string FormatTimeDuration(string departureTime, string arrivalTime)
        {
            DateTime departureDate = DateTime.ParseExact(departureTime, "HH:mm", null);
            DateTime arrivalDate = DateTime.ParseExact(arrivalTime, "HH:mm", null);

            TimeSpan duration = departureDate - arrivalDate;

            int hours = Math.Abs(duration.Hours);
            int minutes = Math.Abs(duration.Minutes);
            int seconds = Math.Abs(duration.Seconds);

            string formattedDuration = $"{hours} hr";

            if (minutes > 0)
            {
                formattedDuration += $" {minutes} min";
            }

            if (seconds > 0)
            {
                formattedDuration += $" {seconds} sec";
            }

            return formattedDuration;
        }

    }
}
