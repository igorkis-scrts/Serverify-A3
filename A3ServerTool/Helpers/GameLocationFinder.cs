using A3ServerTool.Models;
using System.Linq;

namespace A3ServerTool.Helpers
{
    /// <summary>
    /// Represents various methods to manipulate Arma installation location.
    /// </summary>
    public sealed class GameLocationFinder
    {
        //TODO: Refactor this to a static object that contains game profile in a property instead of calling method every time
        /// <summary>
        /// Gets the game installation path.
        /// </summary>
        /// <param name="executablePath">Profile which exec path will be used.</param>
        /// <returns>Installation path</returns>
        public string GetGameInstallationPath(Profile profile)
        {
            if (profile == null || string.IsNullOrWhiteSpace(profile.ExecutablePath))
            {
                return string.Empty;
            }

            var arrayPath = profile.ExecutablePath.Split('\\').ToList();
            arrayPath.RemoveAll(x => x.Contains(".exe"));
            return string.Join("\\", arrayPath);
        }

        #region failed singleton

        //private static readonly GameLocationFinder _instance = new GameLocationFinder();

        //public Profile CurrentProfile { get; set; }

        ///// <summary>
        ///// Gets the game installation path.
        ///// </summary>
        //public string GameInstallationPath
        //{
        //    get
        //    {
        //        if (CurrentProfile == null || string.IsNullOrWhiteSpace(CurrentProfile.ExecutablePath))
        //        {
        //            return string.Empty;
        //        }

        //        var arrayPath = CurrentProfile.ExecutablePath.Split('\\').ToList();
        //        arrayPath.RemoveAll(x => x.Contains(".exe"));
        //        return string.Join("\\", arrayPath);
        //    }
        //}

        //static GameLocationFinder() {}

        //private GameLocationFinder() {}

        //public static GameLocationFinder Instance
        //{
        //    get => _instance;
        //}

        //public GameLocationFinder Create(Profile currentProfile)
        //{
        //    CurrentProfile = currentProfile;
        //    return _instance;
        //}

        #endregion
    }
}
