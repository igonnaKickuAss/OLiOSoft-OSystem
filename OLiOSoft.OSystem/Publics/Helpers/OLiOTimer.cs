using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OLiOSoft.OSystem.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    sealed public class OLiOTimer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit"></param>
        public OLiOTimer(int limit = 3)
        {
            timeLimit = limit;
        }

        #region -- Private Data --
        private int timeLimit = 0;
        private int oneSecond = 1000;

        private IEnumerator enumerator = null;

        #endregion

        #region -- Public ShotC --
        /// <summary>
        /// 似否已经等待完成？
        /// </summary>
        public bool TimeToGiveUp
        {
            get {
                if (enumerator == null)
                    enumerator = WaitSeconds();

                return enumerator.MoveNext();
            }
        }

        /// <summary>
        /// 次数限制
        /// </summary>
        public int TimeLimit
        {
            set => timeLimit = value > 0 ? value : 1;
        }

        #endregion

        #region -- Public APIMethods --
        /// <summary>
        /// 阻塞当前栈limit秒
        /// </summary>
        public void YouCanDoNothing()
        {
            while (TimeToGiveUp)
            {

            }
        }
        #endregion

        #region -- Private APIMethods --
        private IEnumerator WaitSeconds()
        {
            int current = 0;
            while (current < timeLimit)
            {
                Thread.Sleep(oneSecond);

                current++;

                yield return null;
            }
            enumerator = null;
        }


        #endregion

    }
}
