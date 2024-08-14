namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a provider that can send emails.
    /// </summary>
    public partial interface IEmailProvider
    {
        /// <summary>
        /// Send an email message using the provider.
        /// </summary>
        /// <param name="message"></param>
        Task<IResponse> SendEmailAsync(NotifyMessageDto message);
    }
}