using System.Collections.Generic;
using System.Linq;
using A3ServerTool.Helpers;
using A3ServerTool.Models;
using A3ServerTool.Storage;
using Moq;
using Xunit;

namespace A3ServerTool.Tests
{
    /// <summary>
    /// Provides tests for MissionDirector class.
    /// </summary>
    public class MissionDirectorTests
    {
        //миссии есть в обоих списках
        //миссий нет в одном из списков
        //не те значения
        //нуллы       

        [Fact]
        private void GetMissions_ValidConfigProperties_ShouldReturnMissions()
        {
            var missionDao = new Mock<IDao<Mission>>();
            var missionList = new List<Mission>();
            missionList.AddRange(new[] { new Mission
            {
                Difficulty = A3ServerTool.Enums.DifficultyType.Regular,
                Name = "SampleMission1"
            },
            new Mission
            {
                Difficulty = A3ServerTool.Enums.DifficultyType.Regular,
                Name = "SampleMission2"
            },
            new Mission
            {
                Difficulty = A3ServerTool.Enums.DifficultyType.Veteran,
                Name = "SampleMission3"
            }});
            var properties = new List<string>();
            properties.AddRange(new[]
            {
            "class Missions",
            "{",
            "class Mission_1",
            "{",
            "template = \"SampleMission1\";",
            "difficulty = \"Regular\";",
            "};",
            "class Mission_2",
            "{",
            "template = \"SampleMission3\";",
            "difficulty = \"Veteran\";",
            "};",
            "};"
            });

            missionDao.Setup(md => md.GetAll(string.Empty)).Returns(missionList);

            var director = new MissionDirector(missionDao.Object);
            var resultMissions = director.GetMissions(properties, string.Empty);

            Assert.True(resultMissions.Any());

            //temporarily disabled, requires Equals and GetHashCode override
            //Assert.True(new[] { new Mission
            //{
            //    Difficulty = A3ServerTool.Enums.DifficultyType.Regular,
            //    IsSelected = true,
            //    IsWhitelisted = false,
            //    Name = "SampleMission1"
            //},
            //new Mission
            //{
            //    Difficulty = A3ServerTool.Enums.DifficultyType.Regular,
            //    IsWhitelisted = false,
            //    Name = "SampleMission2"
            //},
            //new Mission
            //{
            //    Difficulty = A3ServerTool.Enums.DifficultyType.Veteran,
            //    IsSelected = true,
            //    IsWhitelisted = false,
            //    Name = "SampleMission3"
            //}}.SequenceEqual(resultMissions));
        }

        [Fact]
        private void GetMissions_NoConfigProperties_ShouldReturnMissions()
        {
            var missionDao = new Mock<IDao<Mission>>();
            var missionList = new List<Mission>();
            missionList.AddRange(new[] { new Mission
            {
                Difficulty = A3ServerTool.Enums.DifficultyType.Regular,
                Name = "SampleMission1"
            },
            new Mission
            {
                Difficulty = A3ServerTool.Enums.DifficultyType.Regular,
                Name = "SampleMission2"
            },
            new Mission
            {
                Difficulty = A3ServerTool.Enums.DifficultyType.Veteran,
                Name = "SampleMission3"
            }});

            missionDao.Setup(md => md.GetAll(string.Empty)).Returns(missionList);

            var director = new MissionDirector(missionDao.Object);
            var resultMissions = director.GetMissions(null, string.Empty);

            Assert.True(resultMissions.Any());
        }

        [Fact]
        private void GetMissions_NoConfigPropertiesNoMissions_ShouldReturnMissions()
        {
            var missionDao = new Mock<IDao<Mission>>();
            missionDao.Setup(md => md.GetAll(string.Empty)).Returns(new List<Mission>());

            var director = new MissionDirector(missionDao.Object);
            var resultMissions = director.GetMissions(null, string.Empty);

            Assert.True(!resultMissions.Any());
        }
    }
}
