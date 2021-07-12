using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Syncfusion.DocIO.DLS;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	public static class SyncfusionExtensions
	{
		public static void ReplaceField(this WordDocument document, string fieldName, string fieldValue)
		{
			foreach (var section in document.Sections.OfType<WSection>())
			{
				foreach (var paragraph in section.Paragraphs.OfType<WParagraph>())
				{
					paragraph.ReplaceField(fieldName, fieldValue);
				}
				foreach (var table in section.Tables.OfType<WTable>())
				{
					table.ReplaceField(fieldName, fieldValue);
				}
				foreach (var headerFooter in section.HeadersFooters.OfType<HeaderFooter>())
				{
					headerFooter.ReplaceField(fieldName, fieldValue);
				}
			}
		}

		public static void ReplaceField(this WTable table, string fieldName, string fieldValue)
		{
			foreach (var row in table.ChildEntities.OfType<WTableRow>())
			{
				foreach (var cell in row.Cells.OfType<WTableCell>())
				{
					foreach (var paragraph in cell.Paragraphs.OfType<WParagraph>())
					{
						paragraph.ReplaceField(fieldName, fieldValue);
					}
				}
			}
		}

		public static void ReplaceField(this WParagraph paragraph, string fieldName, string fieldValue)
		{
			if (paragraph.Text.Contains(fieldName))
			{
				paragraph.Text = paragraph.Text.Replace(fieldName, fieldValue ?? string.Empty);
			}
		}

		public static void ReplaceField(this HeaderFooter headerFooter, string fieldName, string fieldValue)
		{
			foreach (var paragraph in headerFooter.Paragraphs.OfType<WParagraph>())
			{
				paragraph.ReplaceField(fieldName, fieldValue);
			}
			foreach (var table in headerFooter.Tables.OfType<WTable>())
			{
				table.ReplaceField(fieldName, fieldValue);
			}
		}

		public static IEnumerable<WTable> GetAllTables(this WordDocument document)
		{
			foreach (var section in document.Sections.OfType<WSection>())
			{
				foreach (var table in section.Tables.OfType<WTable>())
				{
					yield return table;
				}
			}
		}

		public static void Clear(this IWParagraphCollection collection)
		{
			var count = collection.Count;
			for (int index = 0; index < count; index++)
			{
				collection.RemoveAt(0);
			}
		}

		public static void ReplaceImage(this WordDocument document, string fieldName, byte[] imagebytes)
		{
			var textSelections = document.FindAll(fieldName, true, false);
			for (int i = 0; i < textSelections.Length; i++)
			{
				WParagraph paragraph = new WParagraph(document);
				WPicture picture = paragraph.AppendPicture(imagebytes) as WPicture;
				TextSelection newSelection = new TextSelection(paragraph, 0, 1);
				TextBodyPart bodyPart = new TextBodyPart(document);
				bodyPart.BodyItems.Add(paragraph);
				document.Replace(textSelections[i].SelectedText, bodyPart, true, true);
			}
		}
	}
}	

