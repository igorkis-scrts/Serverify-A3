using A3ServerTool.Helpers;
using A3ServerTool.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServerifyA3.Tests
{
    /// <summary>
    /// Provides tests for UniversalParser object.
    /// </summary>
    public class UniversalParserTests
    {
        //Reminder to test:
        //null value
        //normal value
        //extreme value
        //wrong object

        [Fact]
        private void ConvertToText_WrongType_ShouldThrowNotSupportedException()
        {
            var obj = new object();
            var parser = new UniversalParser();

            Assert.Throws<NotSupportedException>(() => parser.ConvertToText(obj));
        }

        [Fact]
        private void ConvertToText_ValidBasicConfig_ShouldReturnText()
        {
            IConfig basicConfig = new BasicConfig
            {
                MaxBandwidth = 12312,
                MinBandwidth = 21312,
                MaxCustomFileSize = 160,
                MaxMessagesSend = 321,
                MaxSizeGuaranteed = 554,
                MaxPacketSize = 321,
                MaxSizeNonguaranteed = 213,
                MinErrorToSend = 0.01F,
                MinErrorToSendNear = 0.1F,
                ObjectViewDistance = 2500,
                TerrainGridViewDistance = 25,
                FileLocation = "C:\\FilePath"
            };
            var parser = new UniversalParser();

            var text =  parser.ConvertToText(basicConfig);

            Assert.True(!string.IsNullOrEmpty(text));
        }

        [Fact]
        private void ConvertToText_EmptyServerConfig_ShouldReturnText()
        {
            IConfig serverConfig = new ServerConfig();
            var parser = new UniversalParser();

            var text = parser.ConvertToText(serverConfig);

            Assert.True(!string.IsNullOrEmpty(text));
        }

        [Fact]
        private void ConvertToText_ValidServerConfig_ShouldReturnText()
        {
            IConfig serverConfig = new ServerConfig
            {
                AdminPassword = "sample",
                IsPersistent = 1,
                FilePatchingMode = 2,
                MaximumPing = 200,
                IsDrawingOnMapAllowed = true
            };
            var parser = new UniversalParser();

            var text = parser.ConvertToText(serverConfig);

            Assert.True(!string.IsNullOrEmpty(text));
        }

        [Fact]
        private void ConvertToText_EmptyArmaProfile_ShouldReturnText()
        {
            IConfig armaProfile = new ArmaProfile();
            var parser = new UniversalParser();

            var text = parser.ConvertToText(armaProfile);

            Assert.True(!string.IsNullOrEmpty(text));
        }

        [Fact]
        private void ConvertToText_ValidArmaProfile_ShouldReturnText()
        {
            IConfig armaProfile = new ArmaProfile
            {
                AiLevelPreset = 2,
                AreDeathMessagesShown = 1,
                DefaultDifficulty = "custom",
                TacticalPingType = 2
            };
            var parser = new UniversalParser();

            var text = parser.ConvertToText(armaProfile);

            Assert.True(!string.IsNullOrEmpty(text));
        }
    }
}
