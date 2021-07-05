using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanFactPractice.Utilities;
using Newtonsoft.Json;

namespace PlanFactPractice.Models
{

	//This is actually painful to watch.
	public class AccountsModel
	{
		public Datum Data { get; set; }
		public bool IsSuccess { get; set; }
		public object ErrorMessage { get; set; }
		public object ErrorCode { get; set; }

		public class Datum
		{
			public Item[] Items { get; set; }
			public int Total { get; set; }
			public object[] DeletedItems { get; set; }
			public int TotalDeleted { get; set; }
		}

		public class Item
		{
			public int CompanyId { get; set; }
			public string AccountType { get; set; }
			public object AccountTypeTitle { get; set; }
			public string CurrencyCode { get; set; }
			public string LongTitle { get; set; }
			public string Description { get; set; }
			public string AccountAcct { get; set; }
			public string AccountBik { get; set; }
			public string AccountBank { get; set; }
			public object CurrencyTitle { get; set; }
			public bool Active { get; set; }
			public float Remainder { get; set; }
			public object RemainderOperationId { get; set; }
			public object BankImportIntegrationTokenId { get; set; }
			public object BankImportIntegrationTokenBankId { get; set; }
			public DateTime ModifyDate { get; set; }
			public DateTime CreateDate { get; set; }
			public string ActionStatus { get; set; }
			public bool IsUndistributed { get; set; }
			public DateTime? StartingRemainderDate { get; set; }
			public double? StartingRemainderValue { get; set; }
			public double? StartingRemainderValueInUserCurrency { get; set; }
			public string CorrespondentAcct { get; set; }
			public object[] AccountAliases { get; set; }
			public object[] AcquiringIntegrations { get; set; }
			public object[] SettlementAcquiringIntegrations { get; set; }
			public AccountGroup AccountGroup { get; set; }
			public int HeaderPosition { get; set; }
			public int AccountId { get; set; }
			public string Title { get; set; }
			public object ExternalId { get; set; }
		}

		public class AccountGroup
		{
			public int AccountGroupId { get; set; }
			public string Title { get; set; }
		}


		//public Datum Data { get; set; }
		//public bool IsSuccess { get; set; }
		//public string ErrorMessage { get; set; }
		//public string ErrorCode { get; set; }

		//public class Datum
		//{
		//	public Item[] Items { get; set; }
		//	public int Total { get; set; }
		//	public object[] DeletedItems { get; set; }
		//	public int TotalDeleted { get; set; }
		//}

		//public class Item
		//{
		//	public int CompanyId { get; set; }
		//	public string AccountType { get; set; }
		//	public string AccountTypeTitle { get; set; }
		//	public string CurrencyCode { get; set; }
		//	public string LongTitle { get; set; }
		//	public string Description { get; set; }
		//	public string AccountAcct { get; set; }
		//	public string AccountBik { get; set; }
		//	public string AccountBank { get; set; }
		//	public string CurrencyTitle { get; set; }
		//	public bool Active { get; set; }
		//	public double Remainder { get; set; }
		//	public int RemainderOperationId { get; set; }
		//	public int BankImportIntegrationTokenId { get; set; }
		//	public int BankImportIntegrationTokenBankId { get; set; }
		//	public DateTime ModifyDate { get; set; }
		//	public DateTime CreateDate { get; set; }
		//	public string ActionStatus { get; set; }
		//	public bool IsUndistributed { get; set; }
		//	public DateTime StartingRemainderDate { get; set; }
		//	public double StartingRemainderValue { get; set; }
		//	public double StartingRemainderValueInUserCurrency { get; set; }
		//	public string CorrespondentAcct { get; set; }
		//	public AccountAlias[] AccountAliases { get; set; }
		//	public AcquiringIntegration[] AcquiringIntegrations { get; set; }
		//	public SettlementAcquiringIntegration[] SettlementAcquiringIntegrations { get; set; }
		//	public AccountGroup AccountGroup { get; set; }
		//	public int HeaderPosition { get; set; }
		//	public int AccountId { get; set; }
		//	public string Title { get; set; }
		//	public string ExternalId { get; set; }

		//	public class AccountAlias
		//	{
		//		public int AccountId { get; set; }
		//		public string Alias { get; set; }
		//		public string AccountAcct { get; set; }
		//		public string AccountBank { get; set; }
		//		public string AccountBik { get; set; }
		//	}

		//	public class AcquiringIntegration
		//	{
		//		public string AcquiringIntegrationSettingsId { get; set; }
		//		public AcquiringAccount AcquiringAccount { get; set; }
		//		public string AcquiringService { get; set; }
		//		public DateTime ConnectionDate { get; set; }
		//		public SettlementAccount SettlementAccount { get; set; }
		//		public string ShopId { get; set; }
		//		public string ShopTitle { get; set; }
		//		public string ContractNumber { get; set; }
		//	}

		//	public class SettlementAcquiringIntegration
		//	{
		//		public string AcquiringIntegrationSettingsId { get; set; }
		//		public AcquiringAccount AcquiringAccount { get; set; }
		//		public string AcquiringService { get; set; }
		//		public DateTime ConnectionDate { get; set; }
		//		public SettlementAccount SettlementAccount { get; set; }
		//		public string ShopId { get; set; }
		//		public string ShopTitle { get; set; }
		//		public string ContractNumber { get; set; }
		//	}

		//	public class AcquiringAccount
		//	{
		//		public int AccountId { get; set; }
		//		public string Title { get; set; }
		//		public string ExternalId { get; set; }
		//	}

		//	public class SettlementAccount
		//	{
		//		public int AccountId { get; set; }
		//		public string Title { get; set; }
		//		public string ExternalId { get; set; }
		//	}

		//}

		//public class AccountGroup
		//{
		//	public int AccountGroupId { get; set; }
		//	public string Title { get; set; }
		//}

		public static AccountsModel GetAccountsInfo()
		{
			return JsonConvert.DeserializeObject<AccountsModel>(RequestSender.SendGetRequest("api/v1/accounts").Content.ReadAsStringAsync().Result);
		}
	}
}
