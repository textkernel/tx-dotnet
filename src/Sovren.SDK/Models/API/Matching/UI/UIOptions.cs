// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.Matching.UI.Hooks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.Matching.UI
{
    /// <summary>
    /// Options for creating a Matching UI session
    /// </summary>
    public class BasicUIOptions
    {
        /// <summary>
        /// The username of the user for which you are generating a Matching UI session.
        /// <br/><b>If you do not provide this, the user will be required to login when they view the page</b>
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// If enabled on your account, this setting will override 'SovScore' with whatever you provide. This
        /// will also remove the name 'Sovren' from any UI elements.
        /// </summary>
        public string SovScoreName { get; set; }

        /// <summary>
        /// Specifies custom style options for this session.
        /// </summary>
        public StyleOptions Style { get; set; }

        internal static BasicUIOptions Create(UIOptions copyFrom)
        {
            return new BasicUIOptions
            {
                Username = copyFrom?.Username,
                SovScoreName = copyFrom?.SovScoreName,
                Style = copyFrom?.Style
            };
        }
    }

    /// <summary>
    /// Options for creating a Matching UI session
    /// </summary>
    public class UIOptions : BasicUIOptions
    {
        /// <summary>
        /// <see langword="true"/> to allow the user to see/modify the filter criteria (default = <see langword="true"/>).
        /// </summary>
        public bool ShowFilterCriteria { get; set; } = true;

        /// <summary>
        /// <see langword="true"/> to execute the query as soon as the page loads (default = <see langword="false"/>). This is only applicable for Searching.
        /// All matches/bimetric scoring are executed immediately even if this is <see langword="false"/>.
        /// </summary>
        public bool ExecuteImmediately { get; set; }

        /// <summary>
        /// <see langword="true"/> to show the banner containing your account logo inside the matching UI (default = <see langword="true"/>).
        /// </summary>
        public bool ShowBanner { get; set; } = true;

        /// <summary>
        /// <see langword="true"/> to allow the user to see/modify the category weights (default = <see langword="true"/>).
        /// </summary>
        public bool ShowWeights { get; set; } = true;

        /// <summary>
        /// <see langword="true"/> to show a button that opens the 'details' popup containing detailed job/resume info (default = <see langword="true"/>).
        /// </summary>
        public bool ShowDetailsButton { get; set; } = true;

        /// <summary>
        /// <see langword="true"/> to add a button in the Actions menu that matches other jobs/resumes similar to the current one (default = <see langword="true"/>).
        /// </summary>
        public bool ShowFindSimilar { get; set; } = true;

        /// <summary>
        /// <see langword="true"/> to include Sovren custom web sourcing in search/match results. Cannot be used for bimetric scoring (default = <see langword="false"/>).
        /// </summary>
        public bool ShowWebSourcing { get; set; }

        /// <summary>
        /// <see langword="true"/> to include job boards in search/match results. Cannot be used for bimetric scoring (default = <see langword="true"/>).
        /// Must add credentials in the <see href="https://portal.sovren.com">Sovren Portal</see>
        /// </summary>
        public bool ShowJobBoards { get; set; } = true;

        /// <summary>
        /// <see langword="true"/> to allow the user to save custom searches or select from premade criteria templates (default = <see langword="false"/>).
        /// </summary>
        public bool ShowSavedSearches { get; set; }

        /// <summary>
        /// Contains all the <see href="https://sovren.com/technical-specs/latest/rest-api/matching-ui/overview/#ui-match-hooks">User Action Hooks</see>
        /// for the Match UI session. These are used to make a seamless integration between your system and the Sovren Matching UI.
        /// </summary>
        public UserActionHookCollection Hooks { get; set; }

        /// <summary>
        /// Picklists shown in the UI for your users to filter on your user-defined tags.
        /// <br/>See <seealso href="https://sovren.com/technical-specs/latest/rest-api/ai-matching/overview/user-defined-tags/"/>
        /// </summary>
        public List<UserDefinedTagsPicklist> UserDefinedTagsPicklists { get; set; }

        /// <summary>
        /// If you are using custom skills, provide your custom skills list names here. The builtin Sovren skills lists are always included.
        /// </summary>
        public List<string> SkillsAutoCompleteCustomSkillsList { get; set; }
    }

    /// <summary>
    /// A picklist to show to a user for filtering on user-defined tags
    /// </summary>
    public class UserDefinedTagsPicklist
    {
        /// <summary>
        /// The label for this picklist in the UI
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// A list of user-defined tags that the user will be able to pick from
        /// </summary>
        public List<UserDefinedTagOption> Options { get; set; }
    }

    /// <summary>
    /// A label/value pair for user-defined tags in the UI
    /// </summary>
    public class UserDefinedTagOption
    {
        /// <summary>
        /// The value of the tag.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The text that will be shown to the user for the tag.
        /// </summary>
        public string Text { get; set; }
    }

    /// <summary>
    /// Style options for Matching UI sessions
    /// </summary>
    public class StyleOptions
    {
        /// <summary>
        /// An HTML color used to generate several related colors for various UI elements. For example: <code>#077799</code>
        /// </summary>
        public string PrimaryColor { get; set; }

        /// <summary>
        /// A CSS font-family to use for all UI elements. For example: <code>Arial</code>
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// If you'd like to use a non-standard font, specify the URL where that font can be downloaded here. For example: 
        /// <code>https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&amp;display=swap</code>
        /// Note that you also need to define the <see cref="FontFamily"/> if you use this option.
        /// </summary>
        public string FontUrl { get; set; }
    }
}
