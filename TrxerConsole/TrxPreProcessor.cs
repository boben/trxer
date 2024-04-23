using System;
using System.Text.RegularExpressions;

namespace TrxerConsole
{
    internal class TrxPreProcessor
    {
        public string RemoveAssemblyName(string asm)
        {
            int idx = asm.IndexOf(',');
            if (idx == -1)
                return asm;
            return asm.Substring(0, idx);
        }
        public string RemoveNamespace(string asm)
        {
            int coma = asm.IndexOf(',');
            return asm.Substring(coma + 2, asm.Length - coma - 2);
        }
        public string GetShortDateTime(string time)
        {
            if (string.IsNullOrEmpty(time))
            {
                return string.Empty;
            }

            return DateTime.Parse(time).ToString();
        }

        private string ToExtactTime(double ms)
        {
            if (ms < 1000)
                return ms + " ms";

            if (ms >= 1000 && ms < 60000)
                return string.Format("{0:0.00} seconds", TimeSpan.FromMilliseconds(ms).TotalSeconds);

            if (ms >= 60000 && ms < 3600000)
                return string.Format("{0:0.00} minutes", TimeSpan.FromMilliseconds(ms).TotalMinutes);

            return string.Format("{0:0.00} hours", TimeSpan.FromMilliseconds(ms).TotalHours);
        }

        public string ToExactTimeDefinition(string duration)
        {
            if (string.IsNullOrEmpty(duration))
            {
                return string.Empty;
            }

            return ToExtactTime(TimeSpan.Parse(duration).TotalMilliseconds);
        }

        public string ToExactTimeDefinition(string start, string finish)
        {
            TimeSpan datetime = DateTime.Parse(finish) - DateTime.Parse(start);
            return ToExtactTime(datetime.TotalMilliseconds);
        }

        public string CurrentDateTime()
        {
            return DateTime.Now.ToString();
        }

        public string ExtractImageUrl(string text)
        {
            Match match = Regex.Match(text, "('|\")([^\\s]+(\\.(?i)(jpg|png|gif|bmp)))('|\")",
               RegexOptions.IgnoreCase);


            if (match.Success)
            {
                return match.Value.Replace("\'", string.Empty).Replace("\"", string.Empty).Replace("\\", "\\\\");
            }
            return string.Empty;
        }

    }
}
