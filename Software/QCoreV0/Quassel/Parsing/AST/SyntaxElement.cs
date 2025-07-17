using QCoreV0.Common.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.Parsing.AST
{
	public abstract class SyntaxElement
	{
		public SyntaxElement? Parent { get; protected set; }
		protected List<SyntaxElement> children;

		public IEnumerable<SyntaxElement> Children => children;
		public IEnumerable<SyntaxElement> Descendants => children.SelectMany(c => c.Descendants).Concat(children);

		public string Content { get; protected set; }
		public Location Start { get; protected set; }
		public Location End { get; protected set; }
		public int Length => Content.Length;

		internal SyntaxElement(string content, Location start, List<SyntaxElement> children)
		{
			this.Content = content ?? throw new ArgumentNullException(nameof(content));
			this.Start = start;
			this.End = start.ComputeEnd(content);
			this.children = children ?? throw new ArgumentNullException(nameof(children));
			this.children.ForEach(c => c.Parent = this);
		}

		public string Render()
		{
			StringBuilder sb = new();
			Render(sb, string.Empty, true);
			return sb.ToString();
		}

		private void Render(StringBuilder sb, string indent, bool last)
		{
			sb.Append(indent);

			if (last)
			{
				sb.Append("└── ");
				indent += "    ";
			}
			else
			{
				sb.Append("├── ");
				indent += "│   ";
			}

			sb.AppendLine($"{this.GetType().Name} [{Start} - {End}]: '{RenderContent().Replace("\n", @"\n").Replace("\r", @"\r")}'");

			for (int i = 0; i < children.Count; i++)
			{
				children[i].Render(sb, indent, i == children.Count - 1);
			}
		}

		protected virtual string RenderContent()
		{
			return Content;
		}

		public T? GetChild<T>() where T : SyntaxElement
		{
			return children.OfType<T>().FirstOrDefault();
		}

		public IEnumerable<T> GetChildren<T>() where T : SyntaxElement
		{
			return children.OfType<T>();
		}

		public T? GetDescendant<T>() where T : SyntaxElement
		{
			return Descendants.OfType<T>().FirstOrDefault();
		}

		public bool TryGetChild<T>(out T? child) where T : SyntaxElement
		{
			child = GetChild<T>();
			return child != null;
		}
	}
}
