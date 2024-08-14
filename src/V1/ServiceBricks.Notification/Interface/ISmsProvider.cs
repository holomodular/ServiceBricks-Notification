namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a provider that can send a SMS message.
    /// </summary>
    public partial interface ISmsProvider
    {
        /// <summary>
        /// Send a SMS message using the provider.
        /// </summary>
        /// <param name="message"></param>
        Task<IResponse> SendSmsAsync(NotifyMessageDto message);
    }
}