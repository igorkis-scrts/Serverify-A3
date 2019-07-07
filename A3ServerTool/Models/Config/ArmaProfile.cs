using A3ServerTool.Attributes;
using A3ServerTool.Enums;

namespace A3ServerTool.Models.Config
{
    /// <summary>
    /// Represents server difficulty settings.
    /// </summary>
    public class ArmaProfile : IConfig
    {
        public string FileLocation { get; set ; }

        /// <summary>
        /// Gets or sets the default difficulty for the server.
        /// </summary>
        [ConfigProperty(PropertyName = "difficulty", IsLowerCaseRequired = true, IsQuotationMarksRequired = true)]
        public string DefaultDifficulty { get; set; }

        /// <summary>
        /// Gets or sets the value if damage will be decreased for players and AI members of his group.
        /// </summary>
        [ConfigProperty(PropertyName = "reducedDamage")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? IsDamageReduced { get; set; }

        /// <summary>
        /// Gets or sets the value if indication icons will be shown on units in player's group.
        /// </summary>
        [ConfigProperty(PropertyName = "groupIndicators")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? GroupIndicationType { get; set; }

        /// <summary>
        /// Gets or sets the value if friendly unit tags will be shown.
        /// </summary>
        [ConfigProperty(PropertyName = "friendlyTags")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? FriendlyTagsVisibilityType { get; set; }

        /// <summary>
        /// Gets or sets the value if enemy unit tags will be shown.
        /// </summary>
        [ConfigProperty(PropertyName = "enemyTags")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? EnemyTagsVisibilityType { get; set; }

        /// <summary>
        /// Gets or sets the value if mine positions will be shown.
        /// </summary>
        [ConfigProperty(PropertyName = "detectedMines")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? DetectedMinesVisibilityType { get; set; }

        /// <summary>
        /// Gets or sets the value for showing commands to player.
        /// </summary>
        [ConfigProperty(PropertyName = "commands")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? CommandsVisibilityType { get; set; }

        /// <summary>
        /// Gets or sets the value for showing waypoints for player.
        /// </summary>
        [ConfigProperty(PropertyName = "waypoints")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? WaypointsVisibilityType { get; set; }

        /// <summary>
        /// Gets or sets the value for showing weapon info for player.
        /// </summary>
        [ConfigProperty(PropertyName = "weaponInfo")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? WeaponInfoVisibilityType { get; set; }

        /// <summary>
        /// Gets or sets the value for showing player's stance.
        /// </summary>
        [ConfigProperty(PropertyName = "stanceIndicator")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? StanceIndicatorVisibilityType { get; set; }

        /// <summary>
        /// Gets or sets the value if stamina bar will be shown to player.
        /// </summary>
        [ConfigProperty(PropertyName = "staminaBar")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? IsStaminaBarShown { get; set; }

        /// <summary>
        /// Gets or sets the value if crosshair will be shown to player.
        /// </summary>
        [ConfigProperty(PropertyName = "weaponCrosshair")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? IsCrosshairShown { get; set; }

        /// <summary>
        /// Gets or sets the value if vision aid markers will be shown to player.
        /// </summary>
        [ConfigProperty(PropertyName = "visionAid")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? IsVisionAidAllowed { get; set; }

        /// <summary>
        /// Gets or sets the value for allowing third person view.
        /// </summary>
        [ConfigProperty(PropertyName = "thirdPersonView")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? IsThirdPersonViewAllowed { get; set; }

        /// <summary>
        /// Gets or sets the value for allowing camera shake.
        /// </summary>
        [ConfigProperty(PropertyName = "cameraShake")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? IsCameraShakeAllowed { get; set; }

        /// <summary>
        /// Gets or sets the value for displaying table with kills, deaths and overall score in multiplayer.
        /// </summary>
        [ConfigProperty(PropertyName = "scoreTable")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? IsScoreTableShown { get; set; }

        /// <summary>
        /// Gets or sets the value for showing death messages in chat window.
        /// </summary>
        [ConfigProperty(PropertyName = "deathMessages")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? AreDeathMessagesShown { get; set; }

        /// <summary>
        /// Gets or sets the value for showing who speaks through VON communication.
        /// </summary>
        [ConfigProperty(PropertyName = "vonID")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? AreVonIdsShown { get; set; }

        /// <summary>
        /// Gets or sets the value for showing friendly units on the map.
        /// </summary>
        [ConfigProperty(PropertyName = "mapContentFriendly")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? IsExtendedMapFriendlyContentAllowed { get; set; }

        /// <summary>
        /// Gets or sets the value for showing enemy units and detected mines on the map.
        /// </summary>
        [ConfigProperty(PropertyName = "mapContentEnemy")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? IsExtendedMapEnemyContentAllowed { get; set; }

        /// <summary>
        /// Gets or sets the value for showing enemy units and detected mines on the map.
        /// </summary>
        [ConfigProperty(PropertyName = "mapContentMines")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? IsExtendedMapMinesContentAllowed { get; set; }

        /// <summary>
        /// Gets or sets the value for enabling/disabling automatic reporting of spotted enemies by players only.
        /// </summary>
        [ConfigProperty(PropertyName = "autoReport")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? IsAutoReportEnabled { get; set; }

        /// <summary>
        /// Gets or sets the value for enabling/disabling multiple saves in a mission.
        /// </summary>
        [ConfigProperty(PropertyName = "multipleSaves")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? AreMultipleSavesAllowed { get; set; }

        /// <summary>
        /// Gets or sets the value for allowing tactical ping for players.
        /// </summary>
        [ConfigProperty(PropertyName = "tacticalPing")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty", "Options" })]
        public int? TacticalPingType { get; set; }

        /// <summary>
        /// Gets or sets the ai level preset.
        /// </summary>
        /// <value>
        /// The ai level preset.
        /// </value>
        [ConfigProperty(PropertyName = "aiLevelPreset")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomDifficulty"})]
        public int AiLevelPreset { get; set; }

        /// <summary>
        /// Gets or sets the ai skill.
        /// </summary>
        [ConfigProperty(PropertyName = "skillAI")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomAiLevel" })]
        public float? AiSkill { get; set; }

        /// <summary>
        /// Gets or sets the ai precision.
        /// </summary>
        [ConfigProperty(PropertyName = "precisionAI")]
        [WrappingClass(new string[] { "DifficultyPresets", "CustomAiLevel" })]
        public float? AiPrecision { get; set; }
    }
}
