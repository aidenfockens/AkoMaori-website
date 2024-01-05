using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using A2.Dtos;
using A2.Models;


namespace A2.Helper
{
        public class CalendarOutputFormatter : TextOutputFormatter
        {
            public CalendarOutputFormatter()
            {
                SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/calendar"));
                SupportedEncodings.Add(Encoding.UTF8);
            }

            public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
            {
                Event e = (Event)context.Object;
                StringBuilder builder = new StringBuilder();
                DateTime cur = DateTime.UtcNow;
                string formcur = cur.ToString("yyyyMMddTHHmmssZ");
                builder.AppendLine("BEGIN:VCALENDAR");
                builder.AppendLine("VERSION:2.0");
                builder.AppendLine("PRODID:afoc324");
                builder.AppendLine("BEGIN:VEVENT");
                builder.AppendLine("UID:" + e.Id);
                builder.AppendLine("DTSTAMP:" + formcur);
                builder.AppendLine("DTSTART:" + e.Start);
                builder.AppendLine("DTEND:" + e.End);
                builder.AppendLine("SUMMARY:" + e.Summary);
                builder.AppendLine("DESCRIPTION:" + e.Description);
                builder.AppendLine("LOCATION:" + e.Location);
                builder.AppendLine("END:VEVENT");
                builder.AppendLine("END:VCALENDAR");
                string outString = builder.ToString();
                byte[] outBytes = selectedEncoding.GetBytes(outString);
                var response = context.HttpContext.Response.Body;
                return response.WriteAsync(outBytes, 0, outBytes.Length);
            }
        }
}