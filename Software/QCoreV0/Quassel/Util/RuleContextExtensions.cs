using QCoreV0.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.Util
{
	public static class RuleContextExtensions
	{
		public static Location GetStart(this Antlr4.Runtime.ParserRuleContext context)
		{
			return context == null ? throw new ArgumentNullException(nameof(context)) : new Location(context.Start.Line, context.Start.Column);
		}

		public static Location GetEnd(this Antlr4.Runtime.ParserRuleContext context)
		{
			return context == null ? throw new ArgumentNullException(nameof(context)) : new Location(context.Stop.Line, context.Stop.Column);
		}
	}
}
