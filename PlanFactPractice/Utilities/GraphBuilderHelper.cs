using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanFactPractice.Models;
using Newtonsoft.Json;

namespace PlanFactPractice.Utilities
{
	public static class GraphBuilderHelper
	{
		private static readonly DateTime CurrentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
		private static Dictionary<string, List<double>> ValuesTable { get; set; } = new Dictionary<string, List<double>>();
		private static List<AccountsHistoryModel.Datum> AccountsOperations { get; set; } = new List<AccountsHistoryModel.Datum>();
		private static List<AccountsModel.Item> AccountsInfo { get; set; } = new List<AccountsModel.Item>();
		private static List<string> OrderInfo { get; set; } = new List<string>();

		public static object BuildValuesTable(DateRangeModel dateRange)
		{
			Setup();

			System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
			DateTime markerDate = (DateTime)AccountsInfo.Min(account => account.StartingRemainderDate);

			if (markerDate > dateRange.StartDate)
			{
				DateTime date = dateRange.StartDate;
				while (date < markerDate)
				{
					ValuesTable.Add(date.ToString("yyyy-MM-dd"), new List<double>(new[] { 0.0, 0.0, 0.0, 0.0 }));
					
					date = date.AddDays(1);
				}
			}

			while (markerDate <= dateRange.EndDate)
			{
				ValuesTable.Add(markerDate.ToString("yyyy-MM-dd"), new List<double>());
				FillTable(markerDate);
				markerDate = markerDate.AddDays(1);
			}
			
			if (ValuesTable.ContainsKey(dateRange.StartDate.AddDays(-1).ToString("yyyy-MM-dd")))
			{
				foreach (var keyValuePair in 
					ValuesTable.Where(kv => DateTime.ParseExact(kv.Key, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture) < dateRange.StartDate).ToList())
				{
					ValuesTable.Remove(keyValuePair.Key);
				}
			}
			stopwatch.Stop();

			return new { valuesTable = JsonConvert.SerializeObject(ValuesTable), orderInfo = JsonConvert.SerializeObject(OrderInfo), elapsedTime = stopwatch.ElapsedMilliseconds };
		}

		private static void Setup()
		{
			OrderInfo.Clear();
			AccountsInfo.Clear();
			AccountsOperations.Clear();
			ValuesTable.Clear();

			var accounts = AccountsModel.GetAccountsInfo().Data;
			foreach (var item in accounts.Items)
			{
				if (item.StartingRemainderDate != null && item.StartingRemainderValue != null)
				{ 
					OrderInfo.Add(item.Title);
					AccountsInfo.Add(item);
				}
			}
			OrderInfo.Add("Общий остаток");

			var accountsHistory = AccountsHistoryModel.GetAccountsHistory().Data;
			foreach (var accountInfo in AccountsInfo)
			{
				if (accountsHistory.Any(account => account.AccountId == accountInfo.AccountId))
					AccountsOperations.Add(Array.Find(accountsHistory, account => account.AccountId == accountInfo.AccountId));
			}
		}

		private static void FillTable(DateTime markerDate)
		{
			string dateString = markerDate.ToString("yyyy-MM-dd");
			foreach (var accountInfo in AccountsInfo)
			{
				if (markerDate < (DateTime)accountInfo.StartingRemainderDate)
				{
					ValuesTable[dateString].Add(0.0);
					continue;
				}

				if (markerDate == (DateTime)accountInfo.StartingRemainderDate)
				{
					ValuesTable[dateString].Add((double)accountInfo.StartingRemainderValue);
				}
				else
				{
					int index = AccountsOperations.FindIndex(account => account.AccountId == accountInfo.AccountId);
					if (index != -1)
					{
						while (AccountsOperations[index].Details.Any() && AccountsOperations[index].Details[0].Date < markerDate)
							AccountsOperations[index].Details.RemoveAt(0);

						double previousValue = ValuesTable[markerDate.AddDays(-1).ToString("yyyy-MM-dd")][AccountsInfo.IndexOf(accountInfo)];
						
						if (!AccountsOperations[index].Details.Any())
						{
							ValuesTable[dateString].Add(previousValue);
							continue;
						}

						if (AccountsOperations[index].Details[0].Date == markerDate)
						{
							if (markerDate < CurrentDate)
								ValuesTable[dateString].Add(previousValue + AccountsOperations[index].Details[0].FactValue);
							else
								ValuesTable[dateString].Add(previousValue + AccountsOperations[index].Details[0].PlanValue);
						}
						else
						{
							ValuesTable[dateString].Add(previousValue);
						}
					}
				}
			}

			var values = ValuesTable[dateString];
			ValuesTable[dateString].Add(values.Sum());
		}
	}
}
