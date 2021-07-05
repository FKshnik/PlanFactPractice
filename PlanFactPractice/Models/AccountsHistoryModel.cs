using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanFactPractice.Utilities;
using Newtonsoft.Json;

namespace PlanFactPractice.Models
{
	public class AccountsHistoryModel
	{
		public Datum[] Data { get; set; }
		public bool IsSuccess { get; set; }
		public object ErrorMessage { get; set; }
		public object ErrorCode { get; set; }

		public class Datum
		{
			public int AccountId { get; set; }
			public List<Detail> Details { get; set; } = new List<Detail>();
		}

		public class Detail
		{
			public DateTime Date { get; set; }
			public double FactValue { get; set; }
			public double PlanValue { get; set; }
			public int Count { get; set; }
			public int NotComittedCount { get; set; }
			public double FactValueInUserCurrency { get; set; }
			public double PlanValueInUserCurrency { get; set; }
			public double AccountCurrencyUserCurrencyRate { get; set; }
		}

		public static AccountsHistoryModel GetAccountsHistory()
		{
			return JsonConvert.DeserializeObject<AccountsHistoryModel>(RequestSender.SendGetRequest("api/v1/bizinfos/accountshistory").Content.ReadAsStringAsync().Result);
		}
	}
}
