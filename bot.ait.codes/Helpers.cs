namespace bot.ait.codes
{
    public static class Helpers
    {
        public static string GetMessage(string message)
        {
            if (!message.StartsWith("@Angels2 Bot"))
                return message;
            return message.Replace("@Angels2 Bot ", "");
        }
    }
}