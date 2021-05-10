using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;


namespace Profibiz.PracticeManager.Infrastructure
{
    public static class ValidateHelper
    {
        static public string IS_REQUIRED = " is required.";
        static public string MORE_THEN_0 = " must be more then 0.";
		static public string IS_DUPLICATE = " is duplicate.";

		public static bool IsEmpty(object val)
        {
            var isEmpty = false;
            if (val == null) isEmpty = true;
            else if (val.ToString().Trim() == "") isEmpty = true;
            else if (val is Guid && (Guid)val == default(Guid)) isEmpty = true;
			else if (val is DateTime && (DateTime)val == default(DateTime)) isEmpty = true;

			return isEmpty;
        }

        public static void Empty(object val, string column, List<string> errors)
        {
            if (IsEmpty(val))
            {
                errors.Add(column + IS_REQUIRED);
            }
        }


        public static void EmptyIndex(int? val, string column, List<string> errors)
        {
            if (val == null || val < 0)
            {
                errors.Add(column + IS_REQUIRED);
            }
        }

        public static void Positive(int? val, string column, List<string> errors)
        {
            if ((val ?? 0) <= 0)
            {
                errors.Add(column + MORE_THEN_0);
            }
        }

		public static void Positive(decimal? val, string column, List<string> errors)
		{
			if ((val ?? 0) <= 0)
			{
				errors.Add(column + MORE_THEN_0);
			}
		}

		public static void Empty2<T>(T obj, Expression<Func<T, object>> propertyExpression)
        {

        }

        public static void EmptyEnumerable<M, T>(M parent, IEnumerable<T> rows, Expression<Func<T, object>> propertyExpression, string column, Expression<Func<T>> selectedEntityExpression, List<string> errors)
        {
            var propName = PropertyHelper.GetPropertyName<T>(selectedEntityExpression);
            var propSelected = typeof(M).GetProperty(propName);

            var prop = MapperReflectionHelper.FindProperty(propertyExpression);
            foreach (var row in rows)
            {
                var obj = prop.GetValue(row);
                if (IsEmpty(obj))
                {
                    errors.Add(column + IS_REQUIRED);
                    propSelected.SetValue(parent, row);
                    break;
                }
            }
        }

        public static void PositiveEnumerable<M, T>(M parent, IEnumerable<T> rows, Expression<Func<T, object>> propertyExpression, string column, Expression<Func<T>> selectedEntityExpression, List<string> errors)
        {
            var propName = PropertyHelper.GetPropertyName<T>(selectedEntityExpression);
            var propSelected = typeof(M).GetProperty(propName);

            var prop = MapperReflectionHelper.FindProperty(propertyExpression);
            foreach (var row in rows)
            {
                var obj = prop.GetValue(row);
                if (!IsPositive(obj))
                {
                    errors.Add(column + MORE_THEN_0);
                    propSelected.SetValue(parent, row);
                    break;
                }
            }
        }

        public static void ValidateEnumerable<M, T>(M parent, IEnumerable<T> rows, Func<T, bool> validateFunc, Func<T, string> errorMsgFunc, Expression<Func<T>> selectedEntityExpression, List<string> errors)
        {
            var propName = PropertyHelper.GetPropertyName<T>(selectedEntityExpression);
            var propSelected = typeof(M).GetProperty(propName);

            foreach (var row in rows)
            {
                if (!validateFunc(row))
                {
                    errors.Add(errorMsgFunc(row));
                    propSelected.SetValue(parent, row);
                    break;
                }
            }
        }


        public static void ValidateEnumerable<M, T>(M parent, IEnumerable<T> rows, Func<T, bool> validateFunc, string errorMsg, Expression<Func<T>> selectedEntityExpression, List<string> errors)
        {
            ValidateEnumerable(parent, rows, validateFunc, (q) => errorMsg, selectedEntityExpression, errors);
        }


		public static void ValidateDuplicate<M, T>(M parent, IList<T> rows, Expression<Func<T, object>> propertyExpression, Func<T, string> errorMsgFunc, Expression<Func<T>> selectedEntityExpression, List<string> errors, bool ignoreEmptyValues = true)
		{
			var propName = PropertyHelper.GetPropertyName<T>(selectedEntityExpression);
			var propSelected = typeof(M).GetProperty(propName);

			var count = rows.Count();
			var prop = MapperReflectionHelper.FindProperty(propertyExpression);
			for (var i = 0; i < count; i++)
			{
				var row = rows[i];

				var isdubl = false;
				var val = prop.GetValue(row);
				if (ignoreEmptyValues && IsEmpty(val))
				{
					isdubl = false;
				}
				else
				{
					for (var j = 0; j < i; j++)
					{
						var val2 = prop.GetValue(rows[j]);
						if (Object.Equals(val, val2))
						{
							isdubl = true;
							break;
						}
					}
				}
				if (isdubl)
				{
					var err = errorMsgFunc(row);
					errors.Add(err);
					propSelected.SetValue(parent, row);
					break;
				}
			}
		}

		static bool IsPositive(object val)
		{
			if (val == null) return false;

			if (val is double)
			{
				if ((double)val > 0) return true;
			}
			else if (val is float)
			{
				if ((float)val > 0) return true;
			}
			else if (val is int)
			{
				if ((int)val > 0) return true;
			}
			else if (val is long)
			{
				if ((long)val > 0) return true;
			}
			else if (val is decimal)
			{
				if ((decimal)val > 0) return true;
			}
			else throw new ArgumentException();

			return false;
		}
	}
}
