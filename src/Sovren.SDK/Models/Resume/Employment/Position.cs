// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sovren.Models.Resume.Employment
{
    /// <summary>
    /// A position/job on a resume
    /// </summary>
    public class Position
    {
        /// <summary>
        /// The id of this position (one-based, so POS-1 is the first, etc)
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// The employer/company for this position. Will be <see langword="null"/> 
        /// if <see cref="IsSelfEmployed"/> is <see langword="true"/>
        /// </summary>
        public Employer Employer { get; set; }
        
        /// <summary>
        /// A list of <see cref="Id"/> that have overlapping dates with this <see cref="Position"/>
        /// </summary>
        public List<string> RelatedToByDates { get; set; }
        
        /// <summary>
        /// A list of <see cref="Id"/>s that have the same <see cref="Employer"/> as this <see cref="Position"/>
        /// </summary>
        public List<string> RelatedToByCompanyName { get; set; }
        
        /// <summary>
        /// <see langword="true"/> if the candidate was self-employed at this job/position
        /// </summary>
        public bool IsSelfEmployed { get; set; }
        
        /// <summary>
        /// <see langword="true"/> if the job/position is listed as current on the resume
        /// </summary>
        public bool IsCurrent { get; set; }
        
        /// <summary>
        /// The job title for this position/job
        /// </summary>
        public JobTitle JobTitle { get; set; }
        
        /// <summary>
        /// The start date listed for this position
        /// </summary>
        public SovrenDate StartDate { get; set; }
        
        /// <summary>
        /// The end date listed for this position
        /// </summary>
        public SovrenDate EndDate { get; set; }
        
        /// <summary>
        /// How many employees were supervised in this position/job
        /// </summary>
        public SovrenPrimitive<int> NumberEmployeesSupervised { get; set; }
        
        /// <summary>
        /// The type of job. One of:
        /// <br/>directHire
		/// <br/>contract
		/// <br/>temp
		/// <br/>volunteer
		/// <br/>internship
		/// <br/>UNSPECIFIED
        /// </summary>
        public string JobType { get; set; }
        
        /// <summary>
        /// The name of the skills taxonomy that this position was categorized as based on skills
        /// found in the job description.
        /// </summary>
        public string TaxonomyName { get; set; }
        
        /// <summary>
        /// The name of the skills subtaxonomy that this position was categorized as based on skills
        /// found in the job description.
        /// </summary>
        public string SubTaxonomyName { get; set; }
        
        /// <summary>
        /// The level determined by length of experience and job titles. One of:
        /// <br/>Entry Level
        /// <br/>Experienced(non-manager)
        /// <br/>Senior(more than 5 years experience)
        /// <br/>Manager
        /// <br/>Senior Manager(more than 5 years management experience)
        /// <br/>Executive(VP, Dept.Head)
        /// <br/>Senior Executive(President, C-level)
        /// </summary>
        public string JobLevel { get; set; }
        
        /// <summary>
        /// The percentage of this job described by the <see cref="TaxonomyName"/>
        /// </summary>
        public int TaxonomyPercentage { get; set; }
        
        /// <summary>
        /// The job description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Bullet points found in the <see cref="Description"/> (available when <code>OutputFormat.CreateBullets = true</code> is set in the Configuration string on the request)
        /// </summary>
        public List<Bullet> Bullets { get; set; }
    }
    
    /// <summary>
    /// A single entry in a bullet-point list in a position/job description on a resume
    /// </summary>
    public class Bullet
    {
        /// <summary>
        /// The type of text/term found for this bullet point. One of:
        /// <br/>creativeTerm - <see cref="Text"/> indicates that an action was taken by the candidate
        /// <br/>sentence: the default, no special terms were found in <see cref="Text"/>
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// The text value of the bullet-list item (excluding the bullet point character)
        /// </summary>
        public string Text { get; set; }
    }
    
    /// <summary>
    /// A company name that has been normalized and assigned a probability
    /// </summary>
    public class CompanyNameWithProbability : NormalizedString
    {
        /// <summary>
        /// The degree of certainty that the company name is accurate. One of:
        /// <br/>VeryUnlikely - recommend discarding
        /// <br/>Unlikely - recommend discarding
        /// <br/>Probable - recommend review
        /// <br/>Confident - no action needed
        /// </summary>
        public string Probability { get; set; }
    }
    
    /// <summary>
    /// A name/location for a company/employer
    /// </summary>
    public class Employer
    {
        /// <summary>
        /// The name of the employer (and an accuracy probability)
        /// </summary>
        public CompanyNameWithProbability Name { get; set; }
        
        /// <summary>
        /// Sometimes a second possible company name is found, or a department/organization 
        /// within a company. This is that value, if it is found.
        /// </summary>
        public NormalizedString OtherFoundName { get; set; }
        
        /// <summary>
        /// The location/address of the employer
        /// </summary>
        public Location Location { get; set; }
    }
}