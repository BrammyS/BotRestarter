namespace BotRestarter.Logger.Commands
{
    public interface IConsoleCommands
    {

        /// <summary>
        /// Executes the `bots` command. So the user can change the auto restart setting for a bot.
        /// </summary>
        void BotsCommand();
    }
}