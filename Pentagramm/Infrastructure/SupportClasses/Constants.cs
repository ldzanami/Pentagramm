namespace Pentagramm.Infrastructure.SupportClasses
{
    public static class Constants
    {
        public static string AdminRole => "Admin";
        public static string UserRole => "User";
        public static string ModeratorRole => "Moderator";
        public static string UserNotFoundError => "Не найден пользователь";

        public static object ErrorFactory(string error, string obj) => new { Error = error + $": {obj}" };
    }
}
