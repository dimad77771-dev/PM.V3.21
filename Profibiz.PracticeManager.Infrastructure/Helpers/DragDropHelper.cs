using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.DragDrop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class DragDropHelper
	{
		public static object GetDraggedRow(DragDropEventArgs e)
		{
			object dragRow = null;
			if (e.DraggedRows != null && e.DraggedRows.Count > 0)
			{
				dragRow = e.DraggedRows[0];
			}
			return dragRow;
		}

		public static object GetDraggedTreeRow(DragDropEventArgs e)
		{
			var node = GetDraggedRow(e);
			if (node is TreeListNode)
			{
				return (node as TreeListNode)?.Content;
			}
			return null;
		}
	}
}
