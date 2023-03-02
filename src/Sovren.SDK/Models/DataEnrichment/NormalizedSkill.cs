using Sovren.Models.API.DataEnrichment.Skills.Response;

namespace Sovren.Models.DataEnrichment
{
    /// <inheritdoc/>
    public class NormalizedSkill : SkillAndConfidence
    {
        /// <summary>
        /// The raw text that matched to a skill description in the provided language.
        /// </summary>
        public string RawText { get; set; }
    }

    /// <summary>
    /// A skill from the skill taxonomy, and the confidence that this is the correct skill for the given input.
    /// </summary>
    public class SkillAndConfidence : Skill
    {
        /// <summary>
        /// Overall confidence that the skill was normalized to the correct skill.
        /// </summary>
        public float Confidence { get; set; }
    }
}
