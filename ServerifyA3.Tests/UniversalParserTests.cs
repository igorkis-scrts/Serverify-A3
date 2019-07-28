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
    /// Provides tests for UniversalParser class.
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

        [Fact]
        private void Parse_WrongObject_ShouldThrowNotSupportedException()
        {
            var parser = new UniversalParser();

            Assert.Throws<NotSupportedException>(() => parser.Parse<object>(null));
        }

        [Fact]
        private void Parse_ValidArmaProfileProperties_ShouldReturnArmaProfile()
        {
            var properties = new List<string>();
            properties.AddRange(new[]
            {
                "class DifficultyPresets",
                "{",
                "class CustomDifficulty",
                "{",
                "class Options",
                "{",
                "groupIndicators = 1",
                "};",
                "aiLevelPreset = 2;",
                "};",
                "class CustomAiLevel",
                "{",
                "skillAI = 0.5;",
                "precisionAI = 0.5;",
                "};",
                "};",
                "difficulty = \"recruit\";"
            });
            var parser = new UniversalParser();

            var armaProfile = parser.Parse<ArmaProfile>(properties);

            Assert.NotNull(armaProfile);
            Assert.Equal(1, armaProfile.GroupIndicationType);
            Assert.Equal(2, armaProfile.AiLevelPreset);
            Assert.True(Math.Abs(0.5F - armaProfile.AiSkill.Value) < 0.00001);
            Assert.True(Math.Abs(0.5F - armaProfile.AiPrecision.Value) < 0.00001);
            Assert.Equal("recruit", armaProfile.DefaultDifficulty.ToLowerInvariant());
        }

        [Fact]
        private void Parse_NonValidArmaProfileProperties_ShouldReturnArmaProfile()
        {
            var properties = new List<string>();
            properties.AddRange(new[]
            {
                "groupIndicators = 1;",
                "wrongProperty = 321213512312213123;"
            });
            var parser = new UniversalParser();

            var armaProfile = parser.Parse<ArmaProfile>(properties);

            Assert.NotNull(armaProfile);
            Assert.Equal(1, armaProfile.GroupIndicationType);
        }

        [Fact]
        private void Parse_EmptyArmaProfileProperties_ShouldReturnArmaProfile()
        {
            var properties = new List<string>();
            var parser = new UniversalParser();

            var armaProfile = parser.Parse<ArmaProfile>(properties);

            Assert.NotNull(armaProfile);
        }

        [Fact]
        private void Parse_Null_ShouldReturnArmaProfile()
        {
            var parser = new UniversalParser();

            var armaProfile = parser.Parse<ArmaProfile>(null);

            Assert.NotNull(armaProfile);
        }

        [Fact]
        private void Parse_ValidBasicConfigProperties_ShouldReturnBasicConfig()
        {
            var properties = new List<string>();
            properties.AddRange(new[]
            {
                "class sockets",
                "{",
                "maxPacketSize = 1400;",
                "};",
                "adapter = -1;",
                "MaxMsgSend = 128;",
                "MinErrorToSend = 0.001;",
                "viewDistance = 2000;",
                "terrainGrid = 25;"
            });
            var parser = new UniversalParser();

            var basicConfig = parser.Parse<BasicConfig>(properties);

            Assert.NotNull(basicConfig);
            Assert.Equal(128, basicConfig.MaxMessagesSend);
            Assert.Equal(1400, basicConfig.MaxPacketSize);
            Assert.True(Math.Abs(0.001F - basicConfig.MinErrorToSend.Value) < 0.00001);
            Assert.True(Math.Abs(25F - basicConfig.TerrainGridViewDistance.Value) < 0.00001);
            Assert.Equal(2000, basicConfig.ObjectViewDistance);
        }

        [Fact]
        private void Parse_EmptyBasicConfigProperties_ShouldReturnBasicConfig()
        {
            var properties = new List<string>();
            var parser = new UniversalParser();

            var basicConfig = parser.Parse<BasicConfig>(properties);

            Assert.NotNull(basicConfig);
        }

        [Fact]
        private void Parse_NullBasicConfig_ShouldReturnBasicConfig()
        {
            var parser = new UniversalParser();

            var basicConfig = parser.Parse<BasicConfig>(null);

            Assert.NotNull(basicConfig);
        }

        [Fact]
        private void Parse_ValidServerConfigProperties_ShouldReturnServerConfig()
        {
            var properties = new List<string>();
            properties.AddRange(new[]
            {
                "hostname = \"ыфвфывфыв\";",
                "loopback = true;",
                "maxPlayers = 23;",
                "allowedLoadFileExtensions[] = {",
                "\"hpp\"",
                ",\"sqs\"",
                ",\"sqf\"",
                "};"
            });
            var parser = new UniversalParser();

            var serverConfig = parser.Parse<ServerConfig>(properties);

            Assert.NotNull(serverConfig);
            Assert.Equal(23, serverConfig.MaximumAmountOfPlayers);
            Assert.True(serverConfig.IsLan);
            Assert.True(new[] { "hpp", "sqs", "sqf" }.SequenceEqual(serverConfig.LoadFileExtensionsWhitelist));
        }

        [Fact]
        private void Parse_EmptyServerConfigProperties_ShouldReturnServerConfig()
        {
            var properties = new List<string>();
            var parser = new UniversalParser();

            var serverConfig = parser.Parse<ServerConfig>(properties);

            Assert.NotNull(serverConfig);
        }

        [Fact]
        private void Parse_NullServerConfig_ShouldReturnServerConfig()
        {
            var parser = new UniversalParser();

            var serverConfig = parser.Parse<ServerConfig>(null);

            Assert.NotNull(serverConfig);
        }
    }
}
