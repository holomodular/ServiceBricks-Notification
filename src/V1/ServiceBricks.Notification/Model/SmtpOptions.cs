using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBricks.Notification
{
    public class SmtpOptions
    {
        /// <summary>
        /// Email setting for host name.
        /// </summary>
        public virtual string EmailServer { get; set; }

        /// <summary>
        /// Port.
        /// </summary>
        public virtual int EmailPort { get; set; }

        /// <summary>
        /// Determine if request should use SSL.
        /// </summary>
        public virtual bool EmailEnableSsl { get; set; }

        /// <summary>
        /// The username for authentication.
        /// </summary>
        public virtual string EmailUsername { get; set; }

        /// <summary>
        /// The password for authentication.
        /// </summary>
        public virtual string EmailPassword { get; set; }
    }
}