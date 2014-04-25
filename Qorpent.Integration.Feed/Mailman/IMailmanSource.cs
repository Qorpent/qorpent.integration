using System.Collections.Generic;

namespace Qorpent.Integration.Feed.Mailman {
    /// <summary>
    /// 
    /// </summary>
    public interface IMailManSource {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<MailItem> GetOutbox();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailItem"></param>
        void MarkAsSend(MailItem mailItem);
    }
}

