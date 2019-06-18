using A3ServerTool.Attributes;

namespace A3ServerTool.Models
{
    /// <summary>
    /// Represents overall AI skill level.
    /// </summary>
    public class AiLevel
    {
        /// <summary>
        /// Gets or sets the ai skill.
        /// </summary>
        [ConfigProperty(PropertyName = "skillAI")]
        public float AiSkill { get; set; }

        /// <summary>
        /// Gets or sets the ai precision.
        /// </summary>
        [ConfigProperty(PropertyName = "precisionAI")]
        public float AiPrecision { get; set; }
    }
}
