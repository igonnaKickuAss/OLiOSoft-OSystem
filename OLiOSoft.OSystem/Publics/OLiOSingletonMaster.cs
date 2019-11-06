namespace OLiOSoft.OSystem
{
    using OLiOSoft.OSystem.Singleton;

    /// <summary>
    /// 继承了介个的类，是一个单例
    /// </summary>
    /// <typeparam name="T">你的类</typeparam>
    public abstract class OLiOSingletonMaster<T>
        where T : class
    {
        #region -- 单例 --
        static private OLiOSingletonMasterInternal<T> _master = null;

        /// <summary>
        /// 拿到你要的单例
        /// </summary>
        static public T CreateSingletonMaster
        {
            get
            {
                if (_master == null)
                    _master = new OLiOSingletonMasterInternal<T>();
                
                return _master.GetSingletonMaster;
            }
        }

        /// <summary>
        /// 记得把构造函数设为私有
        /// </summary>
        /// <param name="fourBytes">为了提醒你把构造函数设为私有的形参罢了</param>
        protected OLiOSingletonMaster(int fourBytes)
        {

        }
        
        #endregion
    }
}
