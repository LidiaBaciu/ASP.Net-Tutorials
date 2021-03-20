using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ada.Utils.WPF
{
    public static /*internal*/ class WpfUtils
    {
        // ---------------------------------------------------------------------------------------
        // <summary>
        // Extracts the name of the property in an expression like that : () => this.TextInfos
        // </summary>
        // <typeparam name="T"></typeparam>
        // <param name="propertyLambda"></param>
        // <returns></returns>
        // ---------------------------------------------------------------------------------------
        public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            MemberExpression me = propertyLambda.Body as MemberExpression;
            const char chr_point = '.';

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            string result = string.Empty;
            int pos = 0;

            do
            {
                result = me.Member.Name + chr_point + result;
                me = me.Expression as MemberExpression;
            } while (me != null);

            result = result.TrimEnd(chr_point); // remove the trailing "."
            pos = 1 + result.LastIndexOf(chr_point);

            if (pos != 0)
                result = result.Substring(pos);

            return result;
        }
    }
}
