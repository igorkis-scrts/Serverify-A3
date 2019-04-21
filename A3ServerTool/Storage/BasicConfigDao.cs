using A3ServerTool.Models;
using A3ServerTool.Models.Config;
using Interchangeable.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Storage
{
    public class BasicConfigDao
    {
        private const string StorageFolder = "Profiles";

        public BasicConfig Get(Profile profile)
        {
            var file = FileHelper.GetFile(Path.Combine(StorageFolder, profile.Id.ToString(), profile.BasicConfig.FileName) 
                + profile.BasicConfig.FileExtension);
            if (file == null) return null;

            var properties = TextManager.ReadFileLineByLine(file);
            if(!properties.Any()) return null;

            var result = TextParseHandler.Parse<BasicConfig>(properties);
            result.FileLocation = file.FullName;
            return result;
        }

        public void Save(Profile profile)
        {
            try
            {
                var basicDto = new SaveDataDto
                {
                    Content = string.Join("\r\n", TextParseHandler.ConvertToText(profile.BasicConfig)),
                    FileExtension = profile.BasicConfig.FileExtension,
                    FileName = profile.BasicConfig.FileName,
                    Folders = new List<string>
                    {
                        StorageFolder,
                        profile.Id.ToString()
                    }
                };

                profile.BasicConfig.FileLocation = basicDto.GetFullPath();

                FileHelper.Save(basicDto);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
