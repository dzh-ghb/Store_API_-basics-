namespace Api.Common
{
    public static class SharedData
    {
        public static class Roles
        {
            public const string Admin = "admin";
            public const string Consumer = "consumer";

            // свойство
            public static IReadOnlyList<string> AllRoles
            {
                get => new List<string>() { Admin, Consumer };
            }

            #region test
            // public static List<string> GetAllRoles()
            // {
            //     return new List<string>() { Admin, Consumer };
            // }
            #endregion
        }
    }
}