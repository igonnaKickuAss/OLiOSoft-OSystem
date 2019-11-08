using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OSystem.Helpers
{
    /// <summary>
    /// 奥利奥数学扩展方法
    /// </summary>
    static public class OLiOMathMethodExtension
    {
        #region -- Minus APIMethods --
        /// <summary>
        /// 减去
        /// </summary>
        /// <param name="p_Minuend">被减数</param>
        /// <param name="p_Minus">减数</param>
        /// <returns>整型</returns>
        static public int MinusTo(this int p_Minuend, int p_Minus)
        {
            return p_Minuend - p_Minus;
        }

        /// <summary>
        /// 减去（不小于零）
        /// </summary>
        /// <param name="p_Minuend">被减数</param>
        /// <param name="p_Minus">减数</param>
        /// <returns>整型</returns>
        static public int MinusTo_NoLessThanZero(this int p_Minuend, int p_Minus)
        {
            return p_Minuend.MinusTo_NoLessThanMin(p_Minus, 0);
        }

        /// <summary>
        /// 减去（不小于最小值）
        /// </summary>
        /// <param name="p_Minuend">被减数</param>
        /// <param name="p_Minus">减数</param>
        /// <param name="p_Min">最小值</param>
        /// <returns>整型</returns>
        static public int MinusTo_NoLessThanMin(this int p_Minuend, int p_Minus, int p_Min)
        {
            return p_Minuend.LessThan(p_Minus) ? p_Min : p_Minuend.MinusTo(p_Minus);
        }

        #endregion

        #region -- Plus APIMethods --
        /// <summary>
        /// 加上
        /// </summary>
        /// <param name="p_AddendFirst">加数</param>
        /// <param name="p_AddendLast">加数</param>
        /// <returns>整型</returns>
        static public int PlusTo(this int p_AddendFirst, int p_AddendLast)
        {
            return p_AddendFirst + p_AddendLast;
        }

        /// <summary>
        /// 加上
        /// </summary>
        /// <param name="p_AddendFirst">加数</param>
        /// <param name="p_AddendLast">加数</param>
        /// <returns>整型</returns>
        static public int PlusTo_NoMoreThanZero(this int p_AddendFirst, int p_AddendLast)
        {
            return p_AddendFirst.PlusTo_NoMoreThanMax(p_AddendLast, 0);
        }

        /// <summary>
        /// 加上
        /// </summary>
        /// <param name="p_AddendFirst">加数</param>
        /// <param name="p_AddendLast">加数</param>
        /// <param name="p_Max">最大值</param>
        /// <returns>整型</returns>
        static public int PlusTo_NoMoreThanMax(this int p_AddendFirst, int p_AddendLast, int p_Max)
        {
            int result = p_AddendFirst.PlusTo(p_AddendLast);
            return result.LessThan(p_Max) ? result : p_Max;
        }

        #endregion

        #region -- Compare APIMethods --
        /// <summary>
        /// 小于  
        /// </summary>
        /// <param name="p_Front">前者</param>
        /// <param name="p_Behind">后者</param>
        /// <returns>布尔</returns>
        static public bool LessThan(this int p_Front, int p_Behind)
        {
            return p_Front < p_Behind;
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="p_Front">前者</param>
        /// <param name="p_Behind">后者</param>
        /// <returns>布尔</returns>
        static public bool MoreThan(this int p_Front, int p_Behind)
        {
            return p_Front > p_Behind;
        }

        #endregion

    }
}
