using System;
using System.Reflection;

namespace OLiOSoft.OSystem.Singleton.Abstracts
{
    using OLiOSoft.OSystem.Singleton.Interfaces;

    internal abstract class ASingletonMasterInternal<T> : ISingletonMasterInternal<T>
        where T : class
    {
        #region -- Private Data --
        private T current;

        #endregion

        #region -- Protected APIMethods --
        /// <summary>
        /// 确认Singleton_T
        /// </summary>
        /// <returns>T</returns>
        protected T ComfirmSingletonMaster()
        {
            Type type = typeof(T);
            ConstructorInfo[] constructorInfoArray =
                type.GetConstructors(
                    BindingFlags.Instance |
                    BindingFlags.NonPublic
                    );

            ConstructorInfo oneParameterConstructorInfo = null;
            foreach (ConstructorInfo constructorInfo in constructorInfoArray)
            {
                ParameterInfo[] parameterInfoArray = constructorInfo.GetParameters();
                if (parameterInfoArray.Length == 1)
                {
                    oneParameterConstructorInfo = constructorInfo;
                    break;
                }
            }

            if (oneParameterConstructorInfo == null)
                throw new NotSupportedException("No constructor without 1 parameter");

            return (T)oneParameterConstructorInfo.Invoke(new object[] { 1 });
        }
        #endregion

        #region -- Interface APIMethods --
        /// <summary>
        /// 拿到单个主要的Singleton_T
        /// </summary>
        public virtual T GetSingletonMaster { get => current; }

        /// <summary>
        /// 设置单个主要的Singleton_T
        /// </summary>
        public virtual T SetSingletonMaster { set => current = value; }

        #endregion

    }
}
