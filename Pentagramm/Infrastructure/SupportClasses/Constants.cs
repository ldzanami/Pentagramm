namespace Pentagramm.Infrastructure.SupportClasses
{
    public static class Constants
    {
        public static string AdminRole => "Admin";
        public static string UserRole => "User";
        public static string ModeratorRole => "Moderator";
        public static string UserNotFoundError => "Пользователь не найден";
        public static string ChatNotFoundError => "Чат не найден";
        public static string MemberNotFoundError => "Участник не найден";
        public static string MessaseNotFoundError => "Сообщение не найдено";

        public static object ErrorFactory(string error, string obj) => new { Error = error + $": {obj}" };
    }
}
