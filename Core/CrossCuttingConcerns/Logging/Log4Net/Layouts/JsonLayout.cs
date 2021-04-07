using log4net.Core;
using log4net.Layout;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Log4Net.Layouts
{
    public class JsonLayout : LayoutSkeleton
    {
        public override void ActivateOptions()
        {
            
        }

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            var logEvent = new SerializableLogEvent(loggingEvent);
            var json = JsonConvert.SerializeObject(logEvent, Formatting.Indented);

            writer.WriteLine(MaskPassword(json));
        }

        private static string MaskPassword(string json)
        {
            JObject jObject = JObject.Parse(json);
            var tokens = jObject.Descendants().ToList();
            foreach (var token in tokens)
            {
                if (token is JProperty property)
                {
                    string propertyName = property.Name;
                    if (propertyName == "Password")
                    {
                        property.Value = "************";
                    }
                }
            }
            return JsonConvert.SerializeObject(jObject, Formatting.Indented);
        }
    }
}
