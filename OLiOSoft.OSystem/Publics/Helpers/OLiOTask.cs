using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OLiOSoft.OSystem.Helpers
{
    /// <summary>
    /// 奥利奥任务，包含任务数据
    /// </summary>
    /// <typeparam name="TRunEventArgs">介个任务运行方法的数据</typeparam>
    /// <typeparam name="TCancelEventArgs">介个任务结束方法的数据</typeparam>
    sealed public class OLiOTask<TRunEventArgs, TCancelEventArgs>
        where TRunEventArgs : EventArgs
        where TCancelEventArgs : EventArgs
    {
        /// <summary>
        /// 喂给他一个任务运行数据，一个任务结束数据
        /// </summary>
        /// <param name="p_RunEventArgs">介个任务运行方法的数据</param>
        /// <param name="p_CancelEventArgs">介个任务结束方法的数据</param>
        public OLiOTask(TRunEventArgs p_RunEventArgs, TCancelEventArgs p_CancelEventArgs)
        {
            runEventArgs = p_RunEventArgs;
            cancelEventArgs = p_CancelEventArgs;

            //timer = new OLiOTimer();
        }

        #region -- Private Data --
        private readonly TRunEventArgs runEventArgs;
        private readonly TCancelEventArgs cancelEventArgs;

        private OLiOTaskEventArgs<EventArgs> taskRunEventArgs = null;
        private OLiOTaskEventArgs<EventArgs> taskCancelEventArgs = null;
        private CancellationTokenSource taskRun_CancelTokenSource = null;
        private CancellationTokenSource taskCancel_CancelTokenSource = null;

        private Task taskRun = null;
        private Task taskCancel = null;

        //private OLiOTimer timer = null;

        private AsyncCompletedEventHandler coreRunCompletedEventHandler = null;
        private AsyncCompletedEventHandler coreCancelCompletedEventHandler = null;

        private Action<
            object,
            OLiOTaskEventArgs<EventArgs>
            > coreRun = null;
        private Action<
            object,
            OLiOTaskEventArgs<EventArgs>
            > coreCancel = null;
        private bool isRunning = false;
        private bool isCanceling = false;
        private int timeLimit = 5;

        #endregion

        #region -- Public Data --
        /// <summary>
        /// 运行方法完成事件，运行方法完成后调用
        /// </summary>
        public event AsyncCompletedEventHandler CoreRunCompletedEventHandles
        {
            add {
                if (coreRunCompletedEventHandler == null)
                    coreRunCompletedEventHandler += value;
            }
            remove {
                if (coreRunCompletedEventHandler == null)
                    return;
                coreRunCompletedEventHandler -= value;
            }
        }

        /// <summary>
        /// 结束方法完成事件，结束方法完成后调用
        /// </summary>
        public event AsyncCompletedEventHandler CoreCancelCompletedEventHandles
        {
            add {
                if (coreCancelCompletedEventHandler == null)
                    coreCancelCompletedEventHandler += value;
            }
            remove {
                if (coreCancelCompletedEventHandler == null)
                    return;
                coreCancelCompletedEventHandler -= value;
            }
        }

        #endregion

        #region -- Public ShotC --
        /// <summary>
        /// 喂给介个任务一个运行方法
        /// </summary>
        public Action<
            object,
            OLiOTaskEventArgs<EventArgs>
            > CoreRun { set => coreRun = value; }

        /// <summary>
        /// 喂给介个任务一个停止方法
        /// </summary>
        public Action<
            object,
            OLiOTaskEventArgs<EventArgs>
            > CoreCancel { set => coreCancel = value; }

        /// <summary>
        /// 介个任务似否已经正在运行了
        /// </summary>
        public bool IsRunning { get => isRunning; }

        /// <summary>
        /// 介个任务似否已经正在停止了
        /// </summary>
        public bool IsCanceling { get => isCanceling; }
        
        /// <summary>
        /// 等待次数限制（一次等待，周期时间为六秒）
        /// </summary>
        public int TimeLimit { get => timeLimit; set => timeLimit = value; }

        #endregion

        #region -- Var --
        private int timeSpan = 6000;

        #endregion

        #region -- Public APIMethods --
        /// <summary>
        /// 同步的方式开始介个任务的运行方法，并且阻塞当前线程
        /// </summary>
        public void Start()
        {
            if (isRunning)
                return;

            //cancel_token
            taskRun_CancelTokenSource = new CancellationTokenSource();

            //taskRunMethod_data
            taskRunEventArgs = new OLiOTaskEventArgs<EventArgs>(
                runEventArgs,
                taskRun_CancelTokenSource.Token
                );

            //taskRun
            taskRun = new Task(TaskRunMethod, taskRun_CancelTokenSource.Token);

            taskRun.RunSynchronously();
        }

        /// <summary>
        /// 异步的方式开始介个任务的运行方法，不阻塞当前线程
        /// </summary>
        public void StartAsync()
        {
            if (isRunning)
                return;

            //cancel_token
            taskRun_CancelTokenSource = new CancellationTokenSource();

            //taskRunMethod_data
            taskRunEventArgs = new OLiOTaskEventArgs<EventArgs>(
                runEventArgs,
                taskRun_CancelTokenSource.Token
                );

            //taskRun
            taskRun = new Task(TaskRunMethod, taskRun_CancelTokenSource.Token);

            taskRun.Start();
        }

        /// <summary>
        /// （一般用来让运行方法停止）同步的方式开始介个任务的停止方法，并且阻塞当前线程
        /// </summary>
        public void Stop()
        {
            if (!isRunning || isCanceling)
                return;

            //cancel_token
            taskCancel_CancelTokenSource = new CancellationTokenSource();

            //taskCancelMethod_data
            taskCancelEventArgs = new OLiOTaskEventArgs<EventArgs>(
                cancelEventArgs,
                taskCancel_CancelTokenSource.Token
                );

            //taskCancel
            taskCancel = new Task(TaskCancelMethod, taskCancel_CancelTokenSource.Token);

            taskCancel.RunSynchronously();
        }

        /// <summary>
        /// （一般用来让运行方法停止）异步的方式开始介个任务的停止方法，不阻塞当前线程
        /// </summary>
        public void StopAsync()
        {
            if (!isRunning || isCanceling)
                return;

            //cancel_token
            taskCancel_CancelTokenSource = new CancellationTokenSource();

            //taskCancelMethod_data
            taskCancelEventArgs = new OLiOTaskEventArgs<EventArgs>(
                cancelEventArgs,
                taskCancel_CancelTokenSource.Token
                );

            //taskCancel
            taskCancel = new Task(TaskCancelMethod, taskCancel_CancelTokenSource.Token);

            taskCancel.Start();
        }

        /// <summary>
        /// 等待介个任务的运行方法完成，并且阻塞当前线程
        /// </summary>
        /// <returns>若介个任务的运行方法在指定等待时间内完成的返回真，否则假</returns>
        public bool Await()
        {
            if (!isRunning)
                return true;

            int current = 0;

            TaskAwaiter awaiter = taskRun.GetAwaiter();

            while (!awaiter.IsCompleted)
            {
                if (current > timeLimit)
                    return false;

                current++;

                Thread.Sleep(timeSpan);
            }

            return true;
        }

        #endregion

        #region -- Private APIMethods --
        private void TaskRunMethod()
        {
            if (coreRun == null)
                return;

            isRunning = true;

            //timer.YouCanDoNothing();

            coreRun.Invoke(this, taskRunEventArgs);

            isRunning = false;

            if (coreRunCompletedEventHandler == null)
                return;

            coreRunCompletedEventHandler.Invoke(
                this,
                new AsyncCompletedEventArgs(null, taskRunEventArgs.Cancelled, null)
                );
        }

        private void TaskCancelMethod()
        {
            if (coreCancel == null)
                return;

            isCanceling = true;

            //timer.YouCanDoNothing();
            taskRun_CancelTokenSource.Cancel();

            coreCancel.Invoke(this, taskCancelEventArgs);

            isCanceling = false;

            if (coreCancelCompletedEventHandler == null)
                return;

            coreCancelCompletedEventHandler.Invoke(
                this,
                new AsyncCompletedEventArgs(null, taskCancelEventArgs.Cancelled, null)
                );
        }

        #endregion
    }
}
