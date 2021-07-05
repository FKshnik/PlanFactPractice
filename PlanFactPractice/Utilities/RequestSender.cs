using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFactPractice.Utilities
{
	public static class RequestSender
	{
		private static readonly string token = "7XxrhcYaqYUoMll_apttEjxfuU0AFTBK-l9LlS0H4eN-vLRCo0V8YHu1GhCJkSuFI4bhyJAmF_Qxx9RdBDvrFiiu8xlBGmNABSpL3qsFBNpNv6-uUKmzw0E2PTPt7nS3iDfrJ0hBm_SFgOiK30qFTjZ_5eGi47gqBj0YQ2JwCToUktSDJZoYH5T5AoojNug190GLKo7hiJnzgCOcr_kebysTGw_jV730N7xX8y3gMLHZYKfIXwFrxNO3E4uP71inx-AfrOAnUkFeDzYh1JF8_dlOheDgcYXy7I1aamjoncHvW-Q9CcjTtSIQ2iwejv86F-lMB2RG6UDP1EwDeNIIFM6OSr2OMb-T6tJFYztc6OIxFtaZBxEpC9AXj2RvEkMZOUaIp0nN5DYEyFLDA0hpw6-e-BJfnjbYYCKcJThMwECigNxEx7lFhDL8NKqGXcDtD74LKaiPP3jI7B76DlLC54EIr1ASKDFXiWkKkoOJRPQz87FnI9dXdubWuGVDhxVk4Z2ZuO_4sCFgVUH3rAABuw7XHJuxH2Bq5YJK6FR9FYLOICy9U5WGkyJt1GNTueA96td8hYB-sPi96tKiFdZYAu9jKf2LlJAHYAMPMFlpM_4-yDWt";
		private static HttpClient client = new HttpClient()
		{
			BaseAddress = new Uri("https://api.planfact.io/")
		};

		static RequestSender()
		{
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.Add("X-ApiKey", token);
		}

		public static HttpResponseMessage SendGetRequest(string relativeUrl)
		{
			return client.GetAsync(relativeUrl).GetAwaiter().GetResult();
		}
	}
}
