namespace OLiOSoft.OSystem.Singleton
{
    using OLiOSoft.OSystem.Singleton.Abstracts;

    internal class OLiOSingletonMasterInternal<T> : ASingletonMasterInternal<T>
        where T : class
    {
        #region -- Override APIMethods --
        public override T GetSingletonMaster
        {
            get {
                if (base.GetSingletonMaster == null)
                    base.SetSingletonMaster = this.ComfirmSingletonMaster();

                return base.GetSingletonMaster;
            }
        }

        #endregion
    }
}
