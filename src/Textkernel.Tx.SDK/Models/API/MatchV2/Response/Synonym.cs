//// Copyright © 2023 Textkernel BV. All rights reserved.
//// This file is provided for use by, or on behalf of, Textkernel licensees
//// within the terms of their license of Textkernel products or Textkernel customers
//// within the Terms of Service pertaining to the Textkernel SaaS products.

//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Textkernel.Tx.Models.API.SearchMatchV2.Response
//{
//    /// <summary>
//    /// Synonym info per item
//    /// </summary>
//    public class Synonym
//    {
//        /// <summary>
//        /// Item for which synonyms are selected
//        /// </summary>
//        public string Key { get; set; }

//        /// <summary>
//        /// Synonyms info for the item
//        /// </summary>
//        public IEnumerable<SynonymSection> Sections { get; set; }
//    }

//    /// <summary>
//    /// Synonym info per section
//    /// </summary>
//    public class SynonymSection
//    {
//        /// <summary>
//        /// Name of the section
//        /// </summary>
//        public string Name { get; set; }

//        /// <summary>
//        /// Determines whether this section is collapsed in the GUI widget
//        /// </summary>
//        public bool Collapsed { get; set; }

//        /// <summary>
//        /// List of actual synonyms per language
//        /// </summary>
//        public IEnumerable<SynonymItem> Items { get; set; }
//    }

//    /// <summary>
//    /// Synonym info per language
//    /// </summary>
//    public class SynonymItem
//    {
//        /// <summary>
//        /// Language
//        /// </summary>
//        public string Lang { get; set; }

//        /// <summary>
//        /// Are keywords in the query automatically expanded with synonyms
//        /// </summary>
//        public bool AutoExpansion { get; set; }

//        /// <summary>
//        /// Synonym values
//        /// </summary>
//        public IEnumerable<string> Values { get; set; }
//    }
//}
