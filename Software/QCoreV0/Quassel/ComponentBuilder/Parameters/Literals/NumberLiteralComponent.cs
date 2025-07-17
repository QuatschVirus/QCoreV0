using QCoreV0.Quassel.Core;
using QCoreV0.Quassel.Parsing.AST;
using QCoreV0.Quassel.Parsing.AST.Nodes;
using QCoreV0.Quassel.Parsing.AST.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.ComponentBuilder.Parameters.Literals
{
	public class NumberLiteralComponent : ParameterComponent
	{
		protected byte[] bytes;

		public NumberLiteralComponent(NumberLiteralSyntaxToken element, CodeSyntaxNode ast, QuasselManager qm) : base(element, ast, qm)
		{
			bytes = 
		}

		public override bool MachineCodeConvertable => true;

		public override ulong ComputeMachineCode()
		{
			return value;
		}

		public T GetValue<T>() where T : struct
		{
			if (typeof(T) == typeof(byte))
			{
				return (T)(object)(byte)value;
			}
			else if (typeof(T) == typeof(ushort))
			{
				return (T)(object)BitConverter.ToUInt16;
			}
			else if (typeof(T) == typeof(uint))
			{
				return (T)(object)(uint)value;
			}
			else if (typeof(T) == typeof(ulong))
			{
				return (T)(object)value;
			}
			else if (typeof(T) == typeof(sbyte))
			{
				return (T)(object)(sbyte)value;
			}
			else if (typeof(T) == typeof(short))
			{
				return (T)(object)(short)value;
			}
			else if (typeof(T) == typeof(int))
			{
				return (T)(object)(int)value;
			}
			else if (typeof(T) == typeof(long))
			{
				return (T)(object)(long)value;
			}
			else
			{
				throw new NotSupportedException($"Type '{typeof(T).Name}' is not supported for number literals.");
			}
		}

		/// <summary>
		/// Used to get a bit representation from a string of a number literal. Accounts for negative numbers, put outputs are "packaged" as an unsinged long and use raw bit-by-bit conversion
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static bool TryGetBytesFromLiteral(string input, out byte[] bytes)
		{
			input = input.Trim();
			if (string.IsNullOrEmpty(input))
			{
				bytes = [];
				return false;
			}

			if (input.StartsWith('-'))
			{

			}
		}
	}
}
