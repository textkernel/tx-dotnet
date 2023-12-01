using System.Collections.Generic;

namespace Textkernel.Tx.Models.DataEnrichment
{
    /// <summary>
    /// An extracted, normalized skill from the Skills taxonomy
    /// </summary>
    public class ExtractedSkill : SkillAndConfidence
    {
        /// <summary>
        /// A list of matches where this skill was found in the text.
        /// </summary>
        public List<SkillMatch> Matches { get; set; }
    }

    /// <summary>
    /// A matched skill that was found in the text provided.
    /// </summary>
    public class SkillMatch
    {
        /// <summary>
        /// The index of the first character of the match (0-based)
        /// </summary>
        public int BeginSpan { get; set; }
        /// <summary>
        /// The index of the last character of the match (0-based).
        /// </summary>
        public int EndSpan { get; set; }
        /// <summary>
        /// Likelihood that the matched term actually refers to a skill in the context of the text.
        /// </summary>
        public float Likelihood { get; set; }
        /// <summary>
        /// The actual term that was found as evidence of this skill (the substring from BeginSpan to EndSpan).
        /// </summary>
        public string RawText { get; set; }
    }
}
