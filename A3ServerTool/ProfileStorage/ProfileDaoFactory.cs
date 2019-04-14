using System;
using Newtonsoft.Json;

namespace A3ServerTool.ProfileStorage
{
    /// <summary>
    /// Provides factory that gives the user the right DAO
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
                    return new JsonProfileDao(new JsonSerializerSettings());  //TODO: DI             
            }
        }
    }
}
