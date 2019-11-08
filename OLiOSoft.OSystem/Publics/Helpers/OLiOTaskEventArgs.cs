using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OLiOSoft.OSystem.Helpers
{
    /// <summary>
    /// （没写完）奥利奥任务的奥利奥事件参数，包含任务运行canceltoken
    /// </summary>
    sealed class OLiOTaskEventArgs : EventArgs
    {
        /// <summary>
        /// 喂给他一个奥利奥事件参数
        /// </summary>
        /// <param name="p_CancelToken">CancellationToken</param>
        public OLiOTaskEventArgs(CancellationToken p_CancelToken)
        {
            cancelToken = p_CancelToken;
        }

        #region -- Private Data --
        private CancellationToken cancelToken;

        #endregion

        #region -- Public ShotC --
        /// <summary>
        /// 该线程似否退出
        /// </summary>
        public bool Cancelled { get => cancelToken.IsCancellationRequested; }

        #endregion
    }
}
