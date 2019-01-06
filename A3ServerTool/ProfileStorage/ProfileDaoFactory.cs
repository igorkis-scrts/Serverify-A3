using System;

namespace A3ServerTool.ProfileStorage
{
    /// <summary>
    /// Factory that gives the user the right DAO
    /// </summary>
    public class ProfileDaoFactory
    {
        public static IProfileDao GetDao(DaoType type)
        {
            switch (type)
            {
                default:
                case DaoType.None:
                    throw new Exception("Dao implementation is not specified!");
                case DaoType.Json:
                    return new JsonProfileDao();               
            }
        }
    }
}
