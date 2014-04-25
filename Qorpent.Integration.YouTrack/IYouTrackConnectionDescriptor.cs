namespace Qorpent.Integration.YouTrack {
    /// <summary>
    /// 
    /// </summary>
    public interface IYouTrackConnectionDescriptor {
        /// <summary>
        /// 
        /// </summary>
        string Server { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Password { get; set; }
    }
}