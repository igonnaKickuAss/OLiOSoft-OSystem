using System;
using System.Collections;
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
    /// （没写完）奥利奥任务，包含任务数据
    /// </summary>
    sealed class OLiOTask
    {
        /// <summary>
        /// 
        /// </summary>
        public struct Messages
        {
            public Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>> runsMapperQueue;

            public Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>> cancelsMapperQueue;

            public Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>> runsMapperQueueReady;

            public Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>> cancelsMapperQueueReady;

            public Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>> runsMapperQueueStore;

            public Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>> cancelsMapperQueueStore;
        }

        /// <summary>
        /// 
        /// </summary>
        public Messages GetMessages
        {
            get
            {
                return new Messages
                {
                    runsMapperQueue = this.runsMapperQueue,

                    cancelsMapperQueue = this.cancelsMapperQueue,

                    runsMapperQueueReady = this.runsMapperQueueReady,

                    cancelsMapperQueueReady = this.cancelsMapperQueueReady,

                    runsMapperQueueStore = this.runsMapperQueueStore,

                    cancelsMapperQueueStore = this.cancelsMapperQueueStore
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public OLiOTask()
        {
            lockObj = new object();

            runsMapperQueue = new Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>>();

            cancelsMapperQueue = new Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>>();

            runsMapperQueueReady = new Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>>();

            cancelsMapperQueueReady = new Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>>();

            runsMapperQueueStore = new Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>>();

            cancelsMapperQueueStore = new Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>>();
        }

        #region -- Private Data --
        //need to new
        readonly private object lockObj = null;

        /// <summary>
        /// shared data..
        /// taskCoreMapperQueue（key: type, value: taskCore(taskCoreMethods)
        /// </summary>
        private Queue<
            Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>
            > runsMapperQueue = null;

        /// <summary>
        /// shared data..
        /// taskCoreMapperQueue（key: type, value: taskCore(taskCoreMethods)
        /// </summary>
        private Queue<
            Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>
            > cancelsMapperQueue = null;

        /// <summary>
        /// shared data..
        /// taskCoreMapperQueue（key: type, value: taskCore(taskCoreMethods)
        /// </summary>
        private Queue<
            Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>
            > runsMapperQueueStore = null;

        /// <summary>
        /// shared data..
        /// taskCoreMapperQueue（key: type, value: taskCore(taskCoreMethods)
        /// </summary>
        private Queue<
            Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>
            > cancelsMapperQueueStore = null;

        /// <summary>
        /// noshared data..
        /// taskCoreMapperQueue（key: type, value: taskCore(taskCoreMethods)
        /// </summary>
        private Queue<
            Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>
            > runsMapperQueueReady = null;

        /// <summary>
        /// noshared data..
        /// taskCoreMapperQueue（key: type, value: taskCore(taskCoreMethods)
        /// </summary>
        private Queue<
            Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>
            > cancelsMapperQueueReady = null;


        private CancellationTokenSource taskRun_CancelTokenSource = null;
        private CancellationTokenSource taskCancel_CancelTokenSource = null;

        private Task taskRun = null;
        private Task taskCancel = null;

        private AsyncCompletedEventHandler runCompletedEventHandler = null;
        private AsyncCompletedEventHandler cancelCompletedEventHandler = null;

        private bool isRunning = false;
        private bool isCanceling = false;
        private bool isModifying = false;
        private int timeLimit = 5;

        #endregion

        #region -- Public Data --
        /// <summary>
        /// 运行方法完成事件，运行方法完成后调用
        /// </summary>
        public event AsyncCompletedEventHandler CoreRunCompletedEventHandles
        {
            add {
                if (runHandleCurrent > handleLimit)
                    return;

                runHandleCurrent = runHandleCurrent.MinusTo_NoLessThanZero(1);

                runCompletedEventHandler += value;
            }
            remove {
                if (runCompletedEventHandler == null)
                    return;

                runHandleCurrent = runHandleCurrent.MinusTo_NoLessThanZero(1);

                runCompletedEventHandler -= value;
            }
        }

        /// <summary>
        /// 结束方法完成事件，结束方法完成后调用
        /// </summary>
        public event AsyncCompletedEventHandler CoreCancelCompletedEventHandles
        {
            add {
                if (cancelHandleCurrent > handleLimit)
                    return;

                cancelHandleCurrent++;

                cancelCompletedEventHandler += value;
            }
            remove {
                if (cancelCompletedEventHandler == null)
                    return;
                cancelCompletedEventHandler -= value;
            }
        }

        #endregion

        #region -- Var --
        int handleLimit = 3;
        int runHandleCurrent = 0;
        int cancelHandleCurrent = 0;
        int timeSpan = 1000;
        int stateRun = 0;
        int stateCancel = 0;

        #endregion

        #region -- Public ShotC --
        /// <summary>
        /// 介个任务似否已经正在运行了
        /// </summary>
        public bool IsRunning { get => isRunning; }

        /// <summary>
        /// 介个任务似否已经正在停止了
        /// </summary>
        public bool IsCanceling { get => isCanceling; }
        
        /// <summary>
        /// 介个任务似否有添加新的方法序列
        /// </summary>
        public bool IsModifying { get => isModifying; }

        /// <summary>
        /// 
        /// </summary>
        public int TimeLimit { get => timeLimit; }

        #endregion

        #region -- Public APIMethods --
        /// <summary>
        /// 同步的方式开始介个任务的运行方法，并且阻塞当前线程
        /// </summary>
        public void Start(object p_Sender, params Action<object, OLiOTaskEventArgs>[] p_TaskCoreMethods)
        {
            if (p_Sender == null)
                return;

            if (p_TaskCoreMethods != null && p_TaskCoreMethods.Length > 0)
            {
                Type type = p_Sender.GetType();

                InsertToMapperQueue(ref type, ref p_TaskCoreMethods, ref this.runsMapperQueue);
            }

            if (isRunning || stateRun > 0)
                return;

            stateRun++;


            //taskRunCancel_token
            taskRun_CancelTokenSource = new CancellationTokenSource();

            //taskRun
            taskRun = new Task(TaskRunMethod, taskRun_CancelTokenSource.Token);

            //taskRunStartSync
            taskRun.RunSynchronously();
        }

        /// <summary>
        /// 异步的方式开始介个任务的运行方法，不阻塞当前线程
        /// </summary>
        public void StartAsync(object p_Sender, params Action<object, OLiOTaskEventArgs>[] p_TaskCoreMethods)
        {
            if (p_Sender == null)
                return;

            if (p_TaskCoreMethods != null && p_TaskCoreMethods.Length > 0)
            {
                Type type = p_Sender.GetType();

                InsertToMapperQueue(ref type, ref p_TaskCoreMethods, ref this.runsMapperQueue);
            }

            if (isRunning || stateRun > 0)
                return;

            stateRun++;

            //cancel_token
            taskRun_CancelTokenSource = new CancellationTokenSource();

            //taskRun
            taskRun = new Task(TaskRunMethod, taskRun_CancelTokenSource.Token);

            taskRun.Start();
        }

        /// <summary>
        /// （一般用来让运行方法停止）同步的方式开始介个任务的停止方法，并且阻塞当前线程
        /// </summary>
        public void Stop(object p_Sender, params Action<object, OLiOTaskEventArgs>[] p_TaskCoreMethods)
        {
            if (p_Sender == null)
                return;

            if (p_TaskCoreMethods != null && p_TaskCoreMethods.Length > 0)
            {
                Type type = p_Sender.GetType();

                InsertToMapperQueue(ref type, ref p_TaskCoreMethods, ref this.cancelsMapperQueue);
            }

            if (!isRunning || isCanceling || stateCancel > 0)
                return;

            stateCancel++;

            //cancel_token
            taskCancel_CancelTokenSource = new CancellationTokenSource();

            //taskCancel
            taskCancel = new Task(TaskCancelMethod, taskCancel_CancelTokenSource.Token);

            taskCancel.RunSynchronously();
        }

        /// <summary>
        /// （一般用来让运行方法停止）异步的方式开始介个任务的停止方法，不阻塞当前线程
        /// </summary>
        public void StopAsync(object p_Sender, params Action<object, OLiOTaskEventArgs>[] p_TaskCoreMethods)
        {
            if (p_Sender == null)
                return;

            if (p_TaskCoreMethods != null && p_TaskCoreMethods.Length > 0)
            {
                Type type = p_Sender.GetType();

                InsertToMapperQueue(ref type, ref p_TaskCoreMethods, ref this.cancelsMapperQueue);
            }

            if (!isRunning || isCanceling || stateCancel > 0)
                return;

            stateCancel++;

            //cancel_token
            taskCancel_CancelTokenSource = new CancellationTokenSource();

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

        #region -- Task APIMethods --
        private void TaskRunMethod()
        {
            isRunning = true;

            IEnumerator workFlow = GetWorkFlow();

            while (workFlow.MoveNext())
            {
                //Console.WriteLine($"0runsMapperQueue:{GetMessages.runsMapperQueue.Count}");
                //Console.WriteLine($"0runsMapperQueueReady:{GetMessages.runsMapperQueueReady.Count}");
                //Console.WriteLine($"0runsMapperQueueStore:{GetMessages.runsMapperQueueStore.Count}");

                IEnumerator runs = GetRuns();

                while (runs.MoveNext())
                    Thread.Sleep(1000);

                //Console.WriteLine($"1runsMapperQueue:{GetMessages.runsMapperQueue.Count}");
                //Console.WriteLine($"1runsMapperQueueReady:{GetMessages.runsMapperQueueReady.Count}");
                //Console.WriteLine($"1runsMapperQueueStore:{GetMessages.runsMapperQueueStore.Count}");
            }

            stateRun = 0;
            isRunning = false;
        }

        private void TaskCancelMethod()
        {
            isCanceling = true;

            this.taskRun_CancelTokenSource.Cancel();

            stateCancel = 0;
            isCanceling = false;
        }

        #endregion

        #region -- Private APIMethods --
        private IEnumerator GetWorkFlow()
        {
            while (this.runsMapperQueue.Count > 0)
            {
                this.runsMapperQueueReady.Enqueue(this.runsMapperQueue.Dequeue());

                yield return null;
            }

            yield break;
        }

        private IEnumerator GetRuns()
        {
            while (this.runsMapperQueueReady.Count > 0 && !taskRun_CancelTokenSource.Token.IsCancellationRequested)
            {
                Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>> runsMapper =
                    this.runsMapperQueueReady.Dequeue();

                Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>.Enumerator enumerator =
                    runsMapper.GetEnumerator();

                Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>> _runsMapper =
                    new Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>();

                Queue<Action<object, OLiOTaskEventArgs>> _runs = new Queue<Action<object, OLiOTaskEventArgs>>();
                
                while (enumerator.MoveNext())
                {
                    Type type = enumerator.Current.Key;

                    Queue<Action<object, OLiOTaskEventArgs>> runs = enumerator.Current.Value;

                    while (runs.Count > 0)
                    {
                        Action<object, OLiOTaskEventArgs> run = runs.Dequeue();

                        run.Invoke(type, new OLiOTaskEventArgs(this.taskRun_CancelTokenSource.Token));

                        _runs.Enqueue(run);
                    }

                    _runsMapper[type] = _runs;
                }

                //TODO.. 回收
                RecycleToMapperQueueStore(ref _runsMapper, ref this.runsMapperQueueStore);
            }

            if (taskRun_CancelTokenSource.Token.IsCancellationRequested)
                RecycleToMapperQueueStore(ref this.runsMapperQueue, ref this.runsMapperQueueStore);                //TODO.. 回收

            yield break;
        }
        

        //private IEnumerator<bool> TaskCancel()
        //{

        //}

        private void RecycleToMapperQueueStore(
            ref Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>> p_TaskCoreMapperReady,
            ref Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>> p_TaskCoreMapperQueueStore
            )
        {
            lock (lockObj)
                p_TaskCoreMapperQueueStore.Enqueue(p_TaskCoreMapperReady);
        }

        private void RecycleToMapperQueueStore(
            ref Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>> p_TaskCoreMapperQueue,
            ref Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>> p_TaskCoreMapperQueueStore
            )
        {
            lock (lockObj)
                while (p_TaskCoreMapperQueue.Count > 0)
                    p_TaskCoreMapperQueueStore.Enqueue(p_TaskCoreMapperQueue.Dequeue());
        }

        private void InsertToMapperQueue(
            ref Type p_Type,
            ref Action<object, OLiOTaskEventArgs>[] p_TaskCoreMethods,
            ref Queue<Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>> p_TaskCoreMapperQueue)
        {
            Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>> taskCoreMapper =
                new Dictionary<Type, Queue<Action<object, OLiOTaskEventArgs>>>();

            taskCoreMapper.Add(
                p_Type,
                new Queue<Action<object, OLiOTaskEventArgs>>(p_TaskCoreMethods)
                );

            lock (lockObj)
                p_TaskCoreMapperQueue.Enqueue(taskCoreMapper);

        }

        #endregion
    }
}
