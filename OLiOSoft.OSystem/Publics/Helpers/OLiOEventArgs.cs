using System;

namespace OLiOSoft.OSystem.Helpers
{
    /// <summary>
    /// 奥利奥事件参数，表示包含事件数据的类的基类，并提供用于不包含事件数据的事件的值。（封装了一下，就似让你知道转换成谁罢了）
    /// </summary>
    /// <typeparam name="T1Data">介个奥利奥事件参数所带的数据</typeparam>
    sealed public class OLiOEventArgs<T1Data> : EventArgs
    {
        /// <summary>
        /// 给参数
        /// </summary>
        /// <param name="p_Data1">数据</param>
        public OLiOEventArgs(T1Data p_Data1)
        {
            Data1 = p_Data1;
        }

        #region -- Public ShotC --
        /// <summary>
        /// As T1Data
        /// </summary>
        public T1Data Data1 { get; private set; }

        #endregion
    
    }

    /// <summary>
    /// 奥利奥事件参数， 表示包含事件数据的类的基类，并提供用于不包含事件数据的事件的值。（封装了一下，就似让你知道转换成谁罢了）
    /// </summary>
    /// <typeparam name="T1Data">介个奥利奥事件参数所带的数据</typeparam>
    /// <typeparam name="T2Data">介个奥利奥事件参数所带的数据</typeparam>
    sealed public class OLiOEventArgs<T1Data, T2Data> : EventArgs
    {
        /// <summary>
        /// 给参数
        /// </summary>
        /// <param name="p_Data1">数据1</param>
        /// <param name="p_Data2">数据2</param>
        public OLiOEventArgs(T1Data p_Data1, T2Data p_Data2)
        {
            Data1 = p_Data1;
            Data2 = p_Data2;
        }

        #region -- Public ShotC --
        /// <summary>
        /// As T1Data
        /// </summary>
        public T1Data Data1 { get; private set; }

        /// <summary>
        /// As T2Data
        /// </summary>
        public T2Data Data2 { get; private set; }

        #endregion

    }

    /// <summary>
    /// 奥利奥事件参数，表示包含事件数据的类的基类，并提供用于不包含事件数据的事件的值。（封装了一下，就似让你知道转换成谁罢了）
    /// </summary>
    /// <typeparam name="T1Data">介个奥利奥事件参数所带的数据</typeparam>
    /// <typeparam name="T2Data">介个奥利奥事件参数所带的数据</typeparam>
    /// <typeparam name="T3Data">介个奥利奥事件参数所带的数据</typeparam>
    sealed public class OLiOEventArgs<T1Data, T2Data, T3Data> : EventArgs
    {
        /// <summary>
        /// 给参数
        /// </summary>
        /// <param name="p_Data1">数据1</param>
        /// <param name="p_Data2">数据2</param>
        /// <param name="p_Data3">数据3</param>
        public OLiOEventArgs(T1Data p_Data1, T2Data p_Data2, T3Data p_Data3)
        {
            Data1 = p_Data1;
            Data2 = p_Data2;
            Data3 = p_Data3;
        }

        #region -- Public ShotC --
        /// <summary>
        /// As T1Data
        /// </summary>
        public T1Data Data1 { get; private set; }

        /// <summary>
        /// As T2Data
        /// </summary>
        public T2Data Data2 { get; private set; }

        /// <summary>
        /// As T3Data
        /// </summary>
        public T3Data Data3 { get; private set; }

        #endregion
    }

    /// <summary>
    /// 奥利奥事件参数，表示包含事件数据的类的基类，并提供用于不包含事件数据的事件的值。（封装了一下，就似让你知道转换成谁罢了）
    /// </summary>
    /// <typeparam name="T1Data">介个奥利奥事件参数所带的数据</typeparam>
    /// <typeparam name="T2Data">介个奥利奥事件参数所带的数据</typeparam>
    /// <typeparam name="T3Data">介个奥利奥事件参数所带的数据</typeparam>
    /// <typeparam name="T4Data">介个奥利奥事件参数所带的数据</typeparam>
    sealed public class OLiOEventArgs<T1Data, T2Data, T3Data, T4Data> : EventArgs
    {
        /// <summary>
        /// 给参数
        /// </summary>
        /// <param name="p_Data1">数据1</param>
        /// <param name="p_Data2">数据2</param>
        /// <param name="p_Data3">数据3</param>
        /// <param name="p_Data4">数据4</param>
        public OLiOEventArgs(T1Data p_Data1, T2Data p_Data2, T3Data p_Data3, T4Data p_Data4)
        {
            Data1 = p_Data1;
            Data2 = p_Data2;
            Data3 = p_Data3;
            Data4 = p_Data4;
        }

        #region -- Public ShotC --
        /// <summary>
        /// As T1Data
        /// </summary>
        public T1Data Data1 { get; private set; }

        /// <summary>
        /// As T2Data
        /// </summary>
        public T2Data Data2 { get; private set; }

        /// <summary>
        /// As T3Data
        /// </summary>
        public T3Data Data3 { get; private set; }

        /// <summary>
        /// As T4Data
        /// </summary>
        public T4Data Data4 { get; private set; }

        #endregion
    }

    /// <summary>
    /// 奥利奥事件参数，表示包含事件数据的类的基类，并提供用于不包含事件数据的事件的值。（封装了一下，就似让你知道转换成谁罢了）
    /// </summary>
    /// <typeparam name="T1Data">介个奥利奥事件参数所带的数据</typeparam>
    /// <typeparam name="T2Data">介个奥利奥事件参数所带的数据</typeparam>
    /// <typeparam name="T3Data">介个奥利奥事件参数所带的数据</typeparam>
    /// <typeparam name="T4Data">介个奥利奥事件参数所带的数据</typeparam>
    /// <typeparam name="T5Data">介个奥利奥事件参数所带的数据</typeparam>
    sealed public class OLiOEventArgs<T1Data, T2Data, T3Data, T4Data, T5Data> : EventArgs
    {
        /// <summary>
        /// 给参数
        /// </summary>
        /// <param name="p_Data1">数据1</param>
        /// <param name="p_Data2">数据2</param>
        /// <param name="p_Data3">数据3</param>
        /// <param name="p_Data4">数据4</param>
        /// <param name="p_Data5">数据5</param>
        public OLiOEventArgs(T1Data p_Data1, T2Data p_Data2, T3Data p_Data3, T4Data p_Data4, T5Data p_Data5)
        {
            Data1 = p_Data1;
            Data2 = p_Data2;
            Data3 = p_Data3;
            Data4 = p_Data4;
            Data5 = p_Data5;
        }

        #region -- Public ShotC --
        /// <summary>
        /// As T1Data
        /// </summary>
        public T1Data Data1 { get; private set; }

        /// <summary>
        /// As T2Data
        /// </summary>
        public T2Data Data2 { get; private set; }

        /// <summary>
        /// As T3Data
        /// </summary>
        public T3Data Data3 { get; private set; }

        /// <summary>
        /// As T4Data
        /// </summary>
        public T4Data Data4 { get; private set; }

        /// <summary>
        /// As T5Data
        /// </summary>
        public T5Data Data5 { get; private set; }

        #endregion

    }

    /// <summary>
    /// 奥利奥事件参数，表示包含事件数据的类的基类，并提供用于不包含事件数据的事件的值。（封装了一下，就似让你知道转换成谁罢了）
    /// </summary>
    /// <typeparam name="T1Data">介个奥利奥事件参数所带的数据</typeparam>
    /// <typeparam name="T2Data">介个奥利奥事件参数所带的数据</typeparam>
    /// <typeparam name="T3Data">介个奥利奥事件参数所带的数据</typeparam>
    /// <typeparam name="T4Data">介个奥利奥事件参数所带的数据</typeparam>
    /// <typeparam name="T5Data">介个奥利奥事件参数所带的数据</typeparam>
    /// <typeparam name="T6Data">介个奥利奥事件参数所带的数据</typeparam>
    sealed public class OLiOEventArgs<T1Data, T2Data, T3Data, T4Data, T5Data, T6Data> : EventArgs
    {
        /// <summary>
        /// 给参数
        /// </summary>
        /// <param name="p_Data1">数据1</param>
        /// <param name="p_Data2">数据2</param>
        /// <param name="p_Data3">数据3</param>
        /// <param name="p_Data4">数据4</param>
        /// <param name="p_Data5">数据5</param>
        /// <param name="p_Data6">数据6</param>
        public OLiOEventArgs(T1Data p_Data1, T2Data p_Data2, T3Data p_Data3, T4Data p_Data4, T5Data p_Data5, T6Data p_Data6)
        {
            Data1 = p_Data1;
            Data2 = p_Data2;
            Data3 = p_Data3;
            Data4 = p_Data4;
            Data5 = p_Data5;
            Data6 = p_Data6;
        }

        #region -- Public ShotC --
        /// <summary>
        /// As T1Data
        /// </summary>
        public T1Data Data1 { get; private set; }

        /// <summary>
        /// As T2Data
        /// </summary>
        public T2Data Data2 { get; private set; }

        /// <summary>
        /// As T3Data
        /// </summary>
        public T3Data Data3 { get; private set; }

        /// <summary>
        /// As T4Data
        /// </summary>
        public T4Data Data4 { get; private set; }

        /// <summary>
        /// As T5Data
        /// </summary>
        public T5Data Data5 { get; private set; }

        /// <summary>
        /// As T6Data
        /// </summary>
        public T6Data Data6 { get; private set; }

        #endregion
    }
}
