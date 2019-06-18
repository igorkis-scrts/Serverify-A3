using A3ServerTool.Attributes;
using A3ServerTool.Enums;

namespace A3ServerTool.Models
{
    /// <summary>
    /// Represents server difficulty settings.
    /// </summary>
    public class Difficulty
    {
        /// <summary>
        /// Gets or sets the value if damage will be decreased for players and AI members of his group.
        /// </summary>
        [ConfigProperty(PropertyName = "reducedDamage")]
        public int ReducedDamage { get; set; }

        /// <summary>
        /// Gets or sets the value if indication icons will be shown on units in player's group.
        /// </summary>
        [ConfigProperty(PropertyName = "groupIndicators")]
        public ThreeStateType GroupIndicationType { get; set; }

        /// <summary>
        /// Gets or sets the value if friendly unit tags will be shown.
        /// </summary>
        [ConfigProperty(PropertyName = "friendlyTags")]
        public ThreeStateType FriendlyTagsVisibilityType { get; set; }

        /// <summary>
        /// Gets or sets the value if enemy unit tags will be shown.
        /// </summary>
        [ConfigProperty(PropertyName = "enemyTags")]
        public int AreEnemyTagsShown { get; set; }

        /// <summary>
        /// Gets or sets the value if mine positions will be shown.
        /// </summary>
        [ConfigProperty(PropertyName = "detectedMines")]
        public ThreeStateType AreDetectedMinesShown { get; set; }

        /// <summary>
        /// Gets or sets the value for showing commands to player.
        /// </summary>
        [ConfigProperty(PropertyName = "commands")]
        public ThreeStateType CommandsVisibilityType { get; set; }

        /// <summary>
        /// Gets or sets the value for showing waypoints for player.
        /// </summary>
        [ConfigProperty(PropertyName = "waypoints")]
        public ThreeStateType WaypointsVisibilityType { get; set; }

        /// <summary>
        /// Gets or sets the value for showing weapon info for player.
        /// </summary>
        [ConfigProperty(PropertyName = "weaponInfo")]
        public ThreeStateType WeaponInfoVisibilityType { get; set; }

        /// <summary>
        /// Gets or sets the value for showing player's stance.
        /// </summary>
        [ConfigProperty(PropertyName = "stanceIndicator")]
        public ThreeStateType StanceIndicatorVisibilityType { get; set; }

        /// <summary>
        /// Gets or sets the value if stamina bar will be shown to player.
        /// </summary>
        [ConfigProperty(PropertyName = "staminaBar")]
        public int IsStaminaBarShown { get; set; }

        /// <summary>
        /// Gets or sets the value if crosshair will be shown to player.
        /// </summary>
        [ConfigProperty(PropertyName = "weaponCrosshair")]
        public int IsCrosshairShown { get; set; }

        /// <summary>
        /// Gets or sets the value if vision aid markers will be shown to player.
        /// </summary>
        [ConfigProperty(PropertyName = "visionAid")]
        public int IsVisionAidAllowed { get; set; }

        /// <summary>
        /// Gets or sets the value for allowing third person view.
        /// </summary>
        [ConfigProperty(PropertyName = "thirdPersonView")]
        public int IsThirdPersonAllowed { get; set; }

        /// <summary>
        /// Gets or sets the value for allowing camera shake.
        /// </summary>
        [ConfigProperty(PropertyName = "cameraShake")]
        public int IsCameraShakeAllowed { get; set; }

        /// <summary>
        /// Gets or sets the value for displaying table with kills, deaths and overall score in multiplayer.
        /// </summary>
        [ConfigProperty(PropertyName = "scoreTable")]
        public int IsScoreTableShown { get; set; }

        /// <summary>
        /// Gets or sets the value for showing death messages in chat window.
        /// </summary>
        [ConfigProperty(PropertyName = "deathMessages")]
        public int AreDeathMessagesShown { get; set; }

        /// <summary>
        /// Gets or sets the value for showing who speaks through VON communication.
        /// </summary>
        [ConfigProperty(PropertyName = "vonID")]
        public int AreVonIdsShown { get; set; }

        /// <summary>
        /// Gets or sets the value for showing friendly units, enemy units and detected mines on the map.
        /// </summary>
        [ConfigProperty(PropertyName = "mapContent")]
        public int IsExtendedMapContentAllowed { get; set; }

        /// <summary>
        /// Gets or sets the value for enabling/disabling automatic reporting of spotted enemies by players only.
        /// </summary>
        [ConfigProperty(PropertyName = "autoReport")]
        public int IsAutoReportEnabled { get; set; }

        /// <summary>
        /// Gets or sets the value for enabling/disabling multiple saves in a mission.
        /// </summary>
        [ConfigProperty(PropertyName = "multipleSaves")]
        public int AreMultipleSavesAllowed { get; set; }
    }
}
