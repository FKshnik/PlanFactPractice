using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFactPractice.Models
{
	public class DateRangeModel
	{
		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime StartDate { get; set; } = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM"), "yyyy-MM", System.Globalization.CultureInfo.InvariantCulture);

		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime EndDate { get; set; } = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM"), "yyyy-MM", System.Globalization.CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1);
	}
}
