﻿using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using Glimpse.Net.Configuration;

namespace Glimpse.Net.Responder
{
    [GlimpseResponder]
    public class Javascript : GlimpseResponder
    {
        [ImportingConstructor]
        public Javascript(JavaScriptSerializer jsSerializer) : base(jsSerializer)
        {
        }

        public override string ResourceName
        {
            get { return "glimpseClient.js"; }
        }

        public override void Respond(HttpApplication application, GlimpseConfiguration config)
        {
            var response = application.Response;
            var assembly = Assembly.GetExecutingAssembly();

            using (var resourceStream = assembly.GetManifestResourceStream("Glimpse.Net.glimpseClient.js"))
            {
                if (resourceStream != null)
                {
                    using (var reader = new StreamReader(resourceStream))
                    {
                        response.Write(reader.ReadToEnd());
                    }
                }
            }
            response.AddHeader("Content-Type", "application/x-javascript");
            application.CompleteRequest();
        }
    }
}