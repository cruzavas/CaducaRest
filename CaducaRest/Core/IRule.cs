using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.Core
{
	public interface IRule
	{
		CustomError customError { get; set; }
		bool IsValid();
	}
}
