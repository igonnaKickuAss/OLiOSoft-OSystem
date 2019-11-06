using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OLiOSoft.OSystem.Helpers
{
    /// <summary>
    /// 奥利奥任务的奥利奥事件参数，包含任务运行数据和任务运行canceltoken
    /// </summary>
    /// <typeparam name="TEventArgs">事件参数（最好用奥利奥事件参数）</typeparam>
    sealed public class OLiOTaskEventArgs<TEventArgs>
        where TEventArgs : EventArgs
    {
        /// <summary>
        /// 喂给他一个奥利奥事件参数
        /// </summary>
        /// <param name="p_EventArgs">奥利奥事件参数</param>
        /// <param name="p_CancelToken"></param>
        public OLiOTaskEventArgs(TEventArgs p_EventArgs, CancellationToken p_CancelToken)
        {
            Current = p_EventArgs;
            cancelToken = p_CancelToken;
        }

        #region -- Private Data --
        private CancellationToken cancelToken;

        #endregion

        #region -- Public ShotC --
        /// <summary>
        /// 当前事件参数
        /// </summary>
        public TEventArgs Current { get; private set; }

        /// <summary>
        /// 该线程似否退出
        /// </summary>
        public bool Cancelled { get => cancelToken.IsCancellationRequested; }

        #endregion
    }
}
