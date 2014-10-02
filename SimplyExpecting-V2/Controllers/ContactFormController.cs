using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using SimplyExpecting_V2.Models;

namespace SimplyExpecting_V2.Controllers
{
	public class ContactFormController : ApiController
	{
		// GET api/<controller>
		public Task<HttpResponseMessage> PostQuestion(ContactForm form)
		{
			// Verify that this is an HTML Form file upload request
			if (!Request.Content.IsMimeMultipartContent("form-data"))
			{
				throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
			}

			// Create a stream provider for setting up output streams that saves the output under c:\tmp\uploads
			// If you want full control over how the stream is saved then derive from MultipartFormDataStreamProvider
			// and override what you need.
			MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider("~/App_Data");

			// Read the MIME multipart content using the stream provider we just created.
			return Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith(t =>
			{
				if (t.IsFaulted || t.IsCanceled)
				{
					Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
				}

				// This illustrates how to get the file names.
				foreach (MultipartFileData file in streamProvider.FileData)
				{
					Trace.WriteLine(file.Headers.ContentDisposition.FileName);
					Trace.WriteLine("Server file path: " + file.LocalFileName);
				}
				return Request.CreateResponse(HttpStatusCode.OK);
			});
		}

	    

		// GET api/<controller>/5
		public string Get(int id)
		{
			return "value";
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		public void Delete(int id)
		{
		}
	}

	public static class IEnumerableExtensions
	{
		public static bool TryGetFormFieldValue(this IEnumerable<HttpContent> contents, string dispositionName, out string formFieldValue)
		{
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}

			HttpContent content = null; ;//= contents.FirstDispositionNameOrDefault(dispositionName);
			if (content != null)
			{
				formFieldValue = content.ReadAsStringAsync().Result;
				return true;
			}

			formFieldValue = null;
			return false;
		}
	}
}