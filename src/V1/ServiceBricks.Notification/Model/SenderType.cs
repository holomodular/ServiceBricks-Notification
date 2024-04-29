using System.Collections.Generic;

namespace ServiceBricks.Notification
{
    public class SenderType : DomainType
    {
        public const int Email = 1;
        public const string Email_TEXT = "Email";

        public const int SMS = 2;
        public const string SMS_TEXT = "SMS";

        public static List<SenderType> GetAll()
        {
            return new List<SenderType>()
            {
                new SenderType(){ Key = Email, Name = Email_TEXT },
                new SenderType(){ Key = SMS, Name = SMS_TEXT },
            };
        }
    }
}