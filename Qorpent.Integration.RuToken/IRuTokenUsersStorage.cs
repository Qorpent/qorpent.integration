namespace Qorpent.Integration.RuToken {
    /// <summary>
    /// 
    /// </summary>
    public interface IRuTokenUsersStorage {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        bool IsActivated(string tokenid = null, string username = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenid"></param>
        /// <param name="config"></param>
        void UpdateConfig(string tokenid, object config);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenid"></param>
        /// <param name="username"></param>
        object GetConfig(string tokenid = null, string username = null);
    }
}
