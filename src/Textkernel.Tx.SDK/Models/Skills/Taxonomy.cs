// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;
using System.Text.Json;

namespace Textkernel.Tx.Models.Skills
{
    /// <summary>
    /// A container to group similar skills subtaxonomies (see <see cref="SubTaxonomy"/>)
    /// </summary>
    public interface ITaxonomy<T>
    {
        /// <summary>
        /// The id of the skills taxonomy
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// The human-readable name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The subtaxonomy children of this taxonomy (more specific groupings of skills)
        /// </summary>
        List<T> SubTaxonomies { get; set; }
    }

    /// <inheritdoc/>
    public class Taxonomy : ITaxonomy<SubTaxonomy>
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public List<SubTaxonomy> SubTaxonomies { get; set; }

        private static List<Taxonomy> _txDefaults = null;

        /// <summary>
        /// A list of all the default taxonomy/subtaxonomy. This list can also be found here:
        /// <see href="https://api.us.textkernel.com/tx/v9/scripts/lib/taxonomies.js"/>
        /// </summary>
        public static List<Taxonomy> TxDefaults
        {
            get
            {
                if (_txDefaults == null)
                {
                    _txDefaults = JsonSerializer.Deserialize<List<Taxonomy>>(_txDefaultsJson);
                }
                return _txDefaults;
            }
        }

        private const string _txDefaultsJson = "[{\"Id\":\"0\",\"Name\":\"Common End-user Software\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"108\",\"SubTaxonomyName\":\"Core Office\"},{\"SubTaxonomyId\":\"228\",\"SubTaxonomyName\":\"Data-centric\"},{\"SubTaxonomyId\":\"229\",\"SubTaxonomyName\":\"Visual\"},{\"SubTaxonomyId\":\"230\",\"SubTaxonomyName\":\"Planning and Analysis\"},{\"SubTaxonomyId\":\"231\",\"SubTaxonomyName\":\"Contact Mgmt\"},{\"SubTaxonomyId\":\"232\",\"SubTaxonomyName\":\"Other\"},{\"SubTaxonomyId\":\"299\",\"SubTaxonomyName\":\"Operating Systems\"},{\"SubTaxonomyId\":\"300\",\"SubTaxonomyName\":\"Mac\"}]},{\"Id\":\"1\",\"Name\":\"Administrative or Clerical\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"109\",\"SubTaxonomyName\":\"Document-centric\"},{\"SubTaxonomyId\":\"110\",\"SubTaxonomyName\":\"Billing and Collections\"},{\"SubTaxonomyId\":\"111\",\"SubTaxonomyName\":\"Recordkeeping and Supplies\"},{\"SubTaxonomyId\":\"112\",\"SubTaxonomyName\":\"Messages and Contact\"},{\"SubTaxonomyId\":\"113\",\"SubTaxonomyName\":\"Admin\"},{\"SubTaxonomyId\":\"114\",\"SubTaxonomyName\":\"Closing and Processing\"},{\"SubTaxonomyId\":\"115\",\"SubTaxonomyName\":\"Computer Related\"},{\"SubTaxonomyId\":\"116\",\"SubTaxonomyName\":\"Contracts\"},{\"SubTaxonomyId\":\"117\",\"SubTaxonomyName\":\"Accounting Related\"},{\"SubTaxonomyId\":\"118\",\"SubTaxonomyName\":\"Clerk\"},{\"SubTaxonomyId\":\"119\",\"SubTaxonomyName\":\"Special Events\"},{\"SubTaxonomyId\":\"499\",\"SubTaxonomyName\":\"Entry Level\"},{\"SubTaxonomyId\":\"945\",\"SubTaxonomyName\":\"Machines\"}]},{\"Id\":\"4\",\"Name\":\"CAD/CAM\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"356\",\"SubTaxonomyName\":\"Architectural\"},{\"SubTaxonomyId\":\"357\",\"SubTaxonomyName\":\"Engineering\"},{\"SubTaxonomyId\":\"358\",\"SubTaxonomyName\":\"Other\"}]},{\"Id\":\"5\",\"Name\":\"Engineering\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"132\",\"SubTaxonomyName\":\"Chemical\"},{\"SubTaxonomyId\":\"133\",\"SubTaxonomyName\":\"Civil\"},{\"SubTaxonomyId\":\"134\",\"SubTaxonomyName\":\"Design\"},{\"SubTaxonomyId\":\"135\",\"SubTaxonomyName\":\"Electrical\"},{\"SubTaxonomyId\":\"136\",\"SubTaxonomyName\":\"Environmental\"},{\"SubTaxonomyId\":\"137\",\"SubTaxonomyName\":\"Industrial\"},{\"SubTaxonomyId\":\"138\",\"SubTaxonomyName\":\"Mechanical\"},{\"SubTaxonomyId\":\"139\",\"SubTaxonomyName\":\"Network\"},{\"SubTaxonomyId\":\"140\",\"SubTaxonomyName\":\"Nuclear\"},{\"SubTaxonomyId\":\"141\",\"SubTaxonomyName\":\"Optical\"},{\"SubTaxonomyId\":\"142\",\"SubTaxonomyName\":\"Plant\"},{\"SubTaxonomyId\":\"143\",\"SubTaxonomyName\":\"Process\"},{\"SubTaxonomyId\":\"144\",\"SubTaxonomyName\":\"Computer Hardware\"},{\"SubTaxonomyId\":\"145\",\"SubTaxonomyName\":\"Structural\"},{\"SubTaxonomyId\":\"146\",\"SubTaxonomyName\":\"Telecom\"},{\"SubTaxonomyId\":\"263\",\"SubTaxonomyName\":\"Other\"},{\"SubTaxonomyId\":\"264\",\"SubTaxonomyName\":\"Techniques\"},{\"SubTaxonomyId\":\"265\",\"SubTaxonomyName\":\"Technologies\"},{\"SubTaxonomyId\":\"266\",\"SubTaxonomyName\":\"Circuits\"},{\"SubTaxonomyId\":\"267\",\"SubTaxonomyName\":\"Military\"},{\"SubTaxonomyId\":\"268\",\"SubTaxonomyName\":\"RF\"},{\"SubTaxonomyId\":\"296\",\"SubTaxonomyName\":\"Water, Wastewater, Soil, Hydrology\"},{\"SubTaxonomyId\":\"297\",\"SubTaxonomyName\":\"Transportation\"},{\"SubTaxonomyId\":\"298\",\"SubTaxonomyName\":\"Surveying\"},{\"SubTaxonomyId\":\"306\",\"SubTaxonomyName\":\"Certifications\"},{\"SubTaxonomyId\":\"307\",\"SubTaxonomyName\":\"General Engineering\"},{\"SubTaxonomyId\":\"308\",\"SubTaxonomyName\":\"Power Engineering\"},{\"SubTaxonomyId\":\"309\",\"SubTaxonomyName\":\"Stress Analysis and Testing\"},{\"SubTaxonomyId\":\"310\",\"SubTaxonomyName\":\"Software and Tools\"},{\"SubTaxonomyId\":\"311\",\"SubTaxonomyName\":\"Air and Aerospace\"},{\"SubTaxonomyId\":\"312\",\"SubTaxonomyName\":\"Refrigeration\"},{\"SubTaxonomyId\":\"735\",\"SubTaxonomyName\":\"Robotics and Automation\"}]},{\"Id\":\"6\",\"Name\":\"Environmental\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"328\",\"SubTaxonomyName\":\"Testing, Assessments, and Monitoring\"},{\"SubTaxonomyId\":\"329\",\"SubTaxonomyName\":\"Air\"},{\"SubTaxonomyId\":\"330\",\"SubTaxonomyName\":\"Water\"},{\"SubTaxonomyId\":\"331\",\"SubTaxonomyName\":\"Soil\"},{\"SubTaxonomyId\":\"332\",\"SubTaxonomyName\":\"Remediation, Mitigation, Cleanup, Removal\"},{\"SubTaxonomyId\":\"333\",\"SubTaxonomyName\":\"General\"},{\"SubTaxonomyId\":\"334\",\"SubTaxonomyName\":\"Hazmat\"},{\"SubTaxonomyId\":\"335\",\"SubTaxonomyName\":\"Compliance, Licensing, Certifications\"},{\"SubTaxonomyId\":\"336\",\"SubTaxonomyName\":\"Permitting\"},{\"SubTaxonomyId\":\"337\",\"SubTaxonomyName\":\"Safety\"}]},{\"Id\":\"7\",\"Name\":\"Finance\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"233\",\"SubTaxonomyName\":\"Mortgage\"},{\"SubTaxonomyId\":\"234\",\"SubTaxonomyName\":\"Lending\"},{\"SubTaxonomyId\":\"235\",\"SubTaxonomyName\":\"Financial Planning \\u0026 Analysis\"},{\"SubTaxonomyId\":\"236\",\"SubTaxonomyName\":\"Securities\"},{\"SubTaxonomyId\":\"237\",\"SubTaxonomyName\":\"Operations\"},{\"SubTaxonomyId\":\"238\",\"SubTaxonomyName\":\"Equities\"},{\"SubTaxonomyId\":\"239\",\"SubTaxonomyName\":\"Trading\"},{\"SubTaxonomyId\":\"240\",\"SubTaxonomyName\":\"Credit and Underwriting\"},{\"SubTaxonomyId\":\"241\",\"SubTaxonomyName\":\"Management\"},{\"SubTaxonomyId\":\"242\",\"SubTaxonomyName\":\"Admin and Customer Service\"},{\"SubTaxonomyId\":\"243\",\"SubTaxonomyName\":\"Other\"},{\"SubTaxonomyId\":\"244\",\"SubTaxonomyName\":\"Broker\"},{\"SubTaxonomyId\":\"245\",\"SubTaxonomyName\":\"Compliance\"},{\"SubTaxonomyId\":\"246\",\"SubTaxonomyName\":\"Treasury\"},{\"SubTaxonomyId\":\"247\",\"SubTaxonomyName\":\"Collections\"},{\"SubTaxonomyId\":\"702\",\"SubTaxonomyName\":\"Tax\"},{\"SubTaxonomyId\":\"703\",\"SubTaxonomyName\":\"Corporate Development\"},{\"SubTaxonomyId\":\"704\",\"SubTaxonomyName\":\"Investor Relations\"},{\"SubTaxonomyId\":\"705\",\"SubTaxonomyName\":\"Accounting\"},{\"SubTaxonomyId\":\"706\",\"SubTaxonomyName\":\"Internal Audit\"},{\"SubTaxonomyId\":\"708\",\"SubTaxonomyName\":\"Procurement\"},{\"SubTaxonomyId\":\"709\",\"SubTaxonomyName\":\"Global Security\"},{\"SubTaxonomyId\":\"710\",\"SubTaxonomyName\":\"Real Estate \\u0026 Facilities\"},{\"SubTaxonomyId\":\"728\",\"SubTaxonomyName\":\"Payroll\"},{\"SubTaxonomyId\":\"732\",\"SubTaxonomyName\":\"Integration\"},{\"SubTaxonomyId\":\"280\",\"SubTaxonomyName\":\"Retail\"},{\"SubTaxonomyId\":\"281\",\"SubTaxonomyName\":\"Trust\"}]},{\"Id\":\"9\",\"Name\":\"Human Resources\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"182\",\"SubTaxonomyName\":\"Administration\"},{\"SubTaxonomyId\":\"183\",\"SubTaxonomyName\":\"Benefits\"},{\"SubTaxonomyId\":\"184\",\"SubTaxonomyName\":\"Compensation\"},{\"SubTaxonomyId\":\"185\",\"SubTaxonomyName\":\"Payroll\"},{\"SubTaxonomyId\":\"187\",\"SubTaxonomyName\":\"Employee Relations\"},{\"SubTaxonomyId\":\"188\",\"SubTaxonomyName\":\"Management\"},{\"SubTaxonomyId\":\"258\",\"SubTaxonomyName\":\"Compliance\"},{\"SubTaxonomyId\":\"259\",\"SubTaxonomyName\":\"Software\"},{\"SubTaxonomyId\":\"262\",\"SubTaxonomyName\":\"Other\"},{\"SubTaxonomyId\":\"580\",\"SubTaxonomyName\":\"Learning \\u0026 Development\"},{\"SubTaxonomyId\":\"581\",\"SubTaxonomyName\":\"Organization Development\"},{\"SubTaxonomyId\":\"583\",\"SubTaxonomyName\":\"Talent Sourcing\"},{\"SubTaxonomyId\":\"584\",\"SubTaxonomyName\":\"Talent Management\"},{\"SubTaxonomyId\":\"574\",\"SubTaxonomyName\":\"Diversity \\u0026 Inclusion\"},{\"SubTaxonomyId\":\"575\",\"SubTaxonomyName\":\"Global Mobility\"},{\"SubTaxonomyId\":\"577\",\"SubTaxonomyName\":\"HR Analytics\"},{\"SubTaxonomyId\":\"578\",\"SubTaxonomyName\":\"HR Operations\"},{\"SubTaxonomyId\":\"579\",\"SubTaxonomyName\":\"HR Management\"}]},{\"Id\":\"10\",\"Name\":\"Information Technology\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"191\",\"SubTaxonomyName\":\"AS/400\"},{\"SubTaxonomyId\":\"192\",\"SubTaxonomyName\":\"Data Mining\"},{\"SubTaxonomyId\":\"193\",\"SubTaxonomyName\":\"Database\"},{\"SubTaxonomyId\":\"194\",\"SubTaxonomyName\":\"Help Desk\"},{\"SubTaxonomyId\":\"195\",\"SubTaxonomyName\":\"ERP and CRM\"},{\"SubTaxonomyId\":\"196\",\"SubTaxonomyName\":\"Internet\"},{\"SubTaxonomyId\":\"197\",\"SubTaxonomyName\":\"Mainframe\"},{\"SubTaxonomyId\":\"198\",\"SubTaxonomyName\":\"Network\"},{\"SubTaxonomyId\":\"199\",\"SubTaxonomyName\":\"Project Management\"},{\"SubTaxonomyId\":\"200\",\"SubTaxonomyName\":\"QA and QC\"},{\"SubTaxonomyId\":\"201\",\"SubTaxonomyName\":\"Architecture\"},{\"SubTaxonomyId\":\"202\",\"SubTaxonomyName\":\"Training\"},{\"SubTaxonomyId\":\"203\",\"SubTaxonomyName\":\"UNIX and LINUX\"},{\"SubTaxonomyId\":\"204\",\"SubTaxonomyName\":\"Programming\"},{\"SubTaxonomyId\":\"251\",\"SubTaxonomyName\":\"Config Deploy Upgrade Migrate\"},{\"SubTaxonomyId\":\"252\",\"SubTaxonomyName\":\"Prebuilt Software\"},{\"SubTaxonomyId\":\"253\",\"SubTaxonomyName\":\"Protocols and Standards\"},{\"SubTaxonomyId\":\"338\",\"SubTaxonomyName\":\"Security\"},{\"SubTaxonomyId\":\"339\",\"SubTaxonomyName\":\"Java\"},{\"SubTaxonomyId\":\"340\",\"SubTaxonomyName\":\"Embedded and Realtime\"},{\"SubTaxonomyId\":\"341\",\"SubTaxonomyName\":\"Reporting\"},{\"SubTaxonomyId\":\"342\",\"SubTaxonomyName\":\"Workflow and Imaging\"},{\"SubTaxonomyId\":\"343\",\"SubTaxonomyName\":\"Mail\"},{\"SubTaxonomyId\":\"344\",\"SubTaxonomyName\":\"GIS\"},{\"SubTaxonomyId\":\"345\",\"SubTaxonomyName\":\"Middleware and Integration\"},{\"SubTaxonomyId\":\"346\",\"SubTaxonomyName\":\"Messaging\"},{\"SubTaxonomyId\":\"347\",\"SubTaxonomyName\":\"Telephony\"},{\"SubTaxonomyId\":\"348\",\"SubTaxonomyName\":\"Service Providers\"},{\"SubTaxonomyId\":\"349\",\"SubTaxonomyName\":\"Operations, Monitoring and Software Management\"},{\"SubTaxonomyId\":\"350\",\"SubTaxonomyName\":\"Financial\"},{\"SubTaxonomyId\":\"351\",\"SubTaxonomyName\":\"Multimedia\"},{\"SubTaxonomyId\":\"352\",\"SubTaxonomyName\":\"Content Management\"},{\"SubTaxonomyId\":\"353\",\"SubTaxonomyName\":\"Medical\"},{\"SubTaxonomyId\":\"355\",\"SubTaxonomyName\":\"Search\"},{\"SubTaxonomyId\":\"550\",\"SubTaxonomyName\":\"Cloud Computing\"},{\"SubTaxonomyId\":\"551\",\"SubTaxonomyName\":\"Gaming\"},{\"SubTaxonomyId\":\"552\",\"SubTaxonomyName\":\"Mobile Applications\"},{\"SubTaxonomyId\":\"553\",\"SubTaxonomyName\":\"Big Data\"},{\"SubTaxonomyId\":\"554\",\"SubTaxonomyName\":\"Business Intelligence\"},{\"SubTaxonomyId\":\"555\",\"SubTaxonomyName\":\"Enterprise Storage\"},{\"SubTaxonomyId\":\"556\",\"SubTaxonomyName\":\"Privacy and Data Security\"},{\"SubTaxonomyId\":\"718\",\"SubTaxonomyName\":\"Distributed Systems\"},{\"SubTaxonomyId\":\"719\",\"SubTaxonomyName\":\"Compiler \\u0026 Runtime\"},{\"SubTaxonomyId\":\"720\",\"SubTaxonomyName\":\"Machine Learning\"},{\"SubTaxonomyId\":\"731\",\"SubTaxonomyName\":\"User Interface\"},{\"SubTaxonomyId\":\"736\",\"SubTaxonomyName\":\"Robotic Process Automation\"}]},{\"Id\":\"11\",\"Name\":\"General Non-Skilled Labor\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"301\",\"SubTaxonomyName\":\"Drivers\"},{\"SubTaxonomyId\":\"302\",\"SubTaxonomyName\":\"Maintenance and Cleaning\"},{\"SubTaxonomyId\":\"303\",\"SubTaxonomyName\":\"Construction Labor\"},{\"SubTaxonomyId\":\"304\",\"SubTaxonomyName\":\"Non-Construction Labor\"},{\"SubTaxonomyId\":\"305\",\"SubTaxonomyName\":\"Warehouse and Picking\"}]},{\"Id\":\"12\",\"Name\":\"Legal\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"395\",\"SubTaxonomyName\":\"Intellectual Property\"},{\"SubTaxonomyId\":\"396\",\"SubTaxonomyName\":\"Contracts\"},{\"SubTaxonomyId\":\"397\",\"SubTaxonomyName\":\"Clerical \\u0026 Paralegal\"},{\"SubTaxonomyId\":\"398\",\"SubTaxonomyName\":\"Admin\"},{\"SubTaxonomyId\":\"399\",\"SubTaxonomyName\":\"Real Estate\"},{\"SubTaxonomyId\":\"400\",\"SubTaxonomyName\":\"Litigation\"},{\"SubTaxonomyId\":\"403\",\"SubTaxonomyName\":\"Corporate Finance\"},{\"SubTaxonomyId\":\"404\",\"SubTaxonomyName\":\"Other\"},{\"SubTaxonomyId\":\"721\",\"SubTaxonomyName\":\"Mergers \\u0026 Acquisitions\"},{\"SubTaxonomyId\":\"723\",\"SubTaxonomyName\":\"Employment\"},{\"SubTaxonomyId\":\"725\",\"SubTaxonomyName\":\"Immigration\"},{\"SubTaxonomyId\":\"726\",\"SubTaxonomyName\":\"Government Affairs\"},{\"SubTaxonomyId\":\"727\",\"SubTaxonomyName\":\"Corporate\"}]},{\"Id\":\"13\",\"Name\":\"Manufacturing\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"205\",\"SubTaxonomyName\":\"Cleanroom\"},{\"SubTaxonomyId\":\"206\",\"SubTaxonomyName\":\"Machining, Metalworking, Tool and Die\"},{\"SubTaxonomyId\":\"207\",\"SubTaxonomyName\":\"Process Control\"},{\"SubTaxonomyId\":\"208\",\"SubTaxonomyName\":\"Planning\"},{\"SubTaxonomyId\":\"209\",\"SubTaxonomyName\":\"Logistics\"},{\"SubTaxonomyId\":\"210\",\"SubTaxonomyName\":\"Management\"},{\"SubTaxonomyId\":\"362\",\"SubTaxonomyName\":\"General\"},{\"SubTaxonomyId\":\"363\",\"SubTaxonomyName\":\"Air and Aerospace\"},{\"SubTaxonomyId\":\"364\",\"SubTaxonomyName\":\"Equipment\"},{\"SubTaxonomyId\":\"365\",\"SubTaxonomyName\":\"Printing\"},{\"SubTaxonomyId\":\"405\",\"SubTaxonomyName\":\"Software\"},{\"SubTaxonomyId\":\"479\",\"SubTaxonomyName\":\"Chemical\"},{\"SubTaxonomyId\":\"480\",\"SubTaxonomyName\":\"Composites\"}]},{\"Id\":\"14\",\"Name\":\"Marketing\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"220\",\"SubTaxonomyName\":\"Consumer\"},{\"SubTaxonomyId\":\"222\",\"SubTaxonomyName\":\"Direct\"},{\"SubTaxonomyId\":\"223\",\"SubTaxonomyName\":\"Market Research\"},{\"SubTaxonomyId\":\"224\",\"SubTaxonomyName\":\"Public Relations\"},{\"SubTaxonomyId\":\"226\",\"SubTaxonomyName\":\"Channel Management\"},{\"SubTaxonomyId\":\"227\",\"SubTaxonomyName\":\"Brand Management\"},{\"SubTaxonomyId\":\"249\",\"SubTaxonomyName\":\"General\"},{\"SubTaxonomyId\":\"250\",\"SubTaxonomyName\":\"Events\"},{\"SubTaxonomyId\":\"585\",\"SubTaxonomyName\":\"Product\"},{\"SubTaxonomyId\":\"586\",\"SubTaxonomyName\":\"Technical Product Marketing\"},{\"SubTaxonomyId\":\"587\",\"SubTaxonomyName\":\"Category Management\"},{\"SubTaxonomyId\":\"588\",\"SubTaxonomyName\":\"Data \\u0026 Analytics\"},{\"SubTaxonomyId\":\"589\",\"SubTaxonomyName\":\"Business Planning\"},{\"SubTaxonomyId\":\"590\",\"SubTaxonomyName\":\"Channel, Partner \\u0026 Ecosystem Marketing\"},{\"SubTaxonomyId\":\"591\",\"SubTaxonomyName\":\"Audience\"},{\"SubTaxonomyId\":\"592\",\"SubTaxonomyName\":\"Marketing Communications\"},{\"SubTaxonomyId\":\"594\",\"SubTaxonomyName\":\"Media\"},{\"SubTaxonomyId\":\"595\",\"SubTaxonomyName\":\"Advertising\"},{\"SubTaxonomyId\":\"599\",\"SubTaxonomyName\":\"Digital\"},{\"SubTaxonomyId\":\"700\",\"SubTaxonomyName\":\"Production Studios\"}]},{\"Id\":\"15\",\"Name\":\"Scientific\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"372\",\"SubTaxonomyName\":\"Pharma\"},{\"SubTaxonomyId\":\"373\",\"SubTaxonomyName\":\"Bio\"},{\"SubTaxonomyId\":\"374\",\"SubTaxonomyName\":\"Animal\"},{\"SubTaxonomyId\":\"375\",\"SubTaxonomyName\":\"Clinical\"},{\"SubTaxonomyId\":\"376\",\"SubTaxonomyName\":\"Basic\"},{\"SubTaxonomyId\":\"377\",\"SubTaxonomyName\":\"Chemical\"},{\"SubTaxonomyId\":\"378\",\"SubTaxonomyName\":\"Imaging\"}]},{\"Id\":\"16\",\"Name\":\"Telecommunications\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"313\",\"SubTaxonomyName\":\"Wireless\"},{\"SubTaxonomyId\":\"314\",\"SubTaxonomyName\":\"Cell Sites and Towers\"},{\"SubTaxonomyId\":\"315\",\"SubTaxonomyName\":\"Cabling and Related\"},{\"SubTaxonomyId\":\"316\",\"SubTaxonomyName\":\"Central Office\"},{\"SubTaxonomyId\":\"317\",\"SubTaxonomyName\":\"Standards, Protocols, Technologies\"},{\"SubTaxonomyId\":\"318\",\"SubTaxonomyName\":\"Network\"},{\"SubTaxonomyId\":\"319\",\"SubTaxonomyName\":\"Hardware\"},{\"SubTaxonomyId\":\"320\",\"SubTaxonomyName\":\"Software\"},{\"SubTaxonomyId\":\"321\",\"SubTaxonomyName\":\"Power\"},{\"SubTaxonomyId\":\"322\",\"SubTaxonomyName\":\"Labor\"},{\"SubTaxonomyId\":\"323\",\"SubTaxonomyName\":\"Skilled\"}]},{\"Id\":\"19\",\"Name\":\"Insurance\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"406\",\"SubTaxonomyName\":\"Property and Casualty\"},{\"SubTaxonomyId\":\"408\",\"SubTaxonomyName\":\"Claims and Adjusting\"},{\"SubTaxonomyId\":\"409\",\"SubTaxonomyName\":\"Health\"},{\"SubTaxonomyId\":\"410\",\"SubTaxonomyName\":\"Commercial\"},{\"SubTaxonomyId\":\"411\",\"SubTaxonomyName\":\"Management\"},{\"SubTaxonomyId\":\"412\",\"SubTaxonomyName\":\"Admin and Clerical\"},{\"SubTaxonomyId\":\"413\",\"SubTaxonomyName\":\"Techniques\"},{\"SubTaxonomyId\":\"414\",\"SubTaxonomyName\":\"Other\"},{\"SubTaxonomyId\":\"415\",\"SubTaxonomyName\":\"Sales\"},{\"SubTaxonomyId\":\"416\",\"SubTaxonomyName\":\"Auto\"}]},{\"Id\":\"20\",\"Name\":\"Sales\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"100\",\"SubTaxonomyName\":\"Inside Sales\"},{\"SubTaxonomyId\":\"101\",\"SubTaxonomyName\":\"Outside Sales\"},{\"SubTaxonomyId\":\"102\",\"SubTaxonomyName\":\"Direct Sales\"},{\"SubTaxonomyId\":\"103\",\"SubTaxonomyName\":\"Channel Management\"},{\"SubTaxonomyId\":\"104\",\"SubTaxonomyName\":\"Account Management\"},{\"SubTaxonomyId\":\"105\",\"SubTaxonomyName\":\"General\"},{\"SubTaxonomyId\":\"106\",\"SubTaxonomyName\":\"Management\"},{\"SubTaxonomyId\":\"107\",\"SubTaxonomyName\":\"Presales\"},{\"SubTaxonomyId\":\"248\",\"SubTaxonomyName\":\"Telemarketing\"},{\"SubTaxonomyId\":\"500\",\"SubTaxonomyName\":\"Entry Level\"},{\"SubTaxonomyId\":\"930\",\"SubTaxonomyName\":\"Retail\"}]},{\"Id\":\"22\",\"Name\":\"Construction Non-Laborer\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"380\",\"SubTaxonomyName\":\"Estimating\"},{\"SubTaxonomyId\":\"381\",\"SubTaxonomyName\":\"Supervision\"},{\"SubTaxonomyId\":\"382\",\"SubTaxonomyName\":\"Inspections\"},{\"SubTaxonomyId\":\"383\",\"SubTaxonomyName\":\"Safety\"},{\"SubTaxonomyId\":\"384\",\"SubTaxonomyName\":\"Commercial\"},{\"SubTaxonomyId\":\"385\",\"SubTaxonomyName\":\"Residential\"},{\"SubTaxonomyId\":\"386\",\"SubTaxonomyName\":\"Industrial\"},{\"SubTaxonomyId\":\"387\",\"SubTaxonomyName\":\"Municipal\"},{\"SubTaxonomyId\":\"388\",\"SubTaxonomyName\":\"Transportation\"},{\"SubTaxonomyId\":\"389\",\"SubTaxonomyName\":\"General Tasks and Equipment\"},{\"SubTaxonomyId\":\"390\",\"SubTaxonomyName\":\"Design\"},{\"SubTaxonomyId\":\"391\",\"SubTaxonomyName\":\"Automation\"},{\"SubTaxonomyId\":\"392\",\"SubTaxonomyName\":\"Compliance\"},{\"SubTaxonomyId\":\"393\",\"SubTaxonomyName\":\"Civil\"},{\"SubTaxonomyId\":\"394\",\"SubTaxonomyName\":\"Office, Admin and Clerical\"}]},{\"Id\":\"26\",\"Name\":\"Power Engineering\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"923\",\"SubTaxonomyName\":\"Nuclear Misc\"},{\"SubTaxonomyId\":\"924\",\"SubTaxonomyName\":\"Non-Nuclear Power Misc\"},{\"SubTaxonomyId\":\"925\",\"SubTaxonomyName\":\"General Power Related\"}]},{\"Id\":\"27\",\"Name\":\"Light Technical/Trades/Skilled Labor\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"283\",\"SubTaxonomyName\":\"Union\"},{\"SubTaxonomyId\":\"284\",\"SubTaxonomyName\":\"Welding Soldering Brazing Cutting\"},{\"SubTaxonomyId\":\"285\",\"SubTaxonomyName\":\"Hvac and Refrig\"},{\"SubTaxonomyId\":\"286\",\"SubTaxonomyName\":\"Carpentry and Painting\"},{\"SubTaxonomyId\":\"287\",\"SubTaxonomyName\":\"Machine Shop\"},{\"SubTaxonomyId\":\"288\",\"SubTaxonomyName\":\"Fine Assembly\"},{\"SubTaxonomyId\":\"289\",\"SubTaxonomyName\":\"Printing\"},{\"SubTaxonomyId\":\"290\",\"SubTaxonomyName\":\"Maint and Repair\"},{\"SubTaxonomyId\":\"291\",\"SubTaxonomyName\":\"Electrical\"},{\"SubTaxonomyId\":\"292\",\"SubTaxonomyName\":\"Moving Equipment\"},{\"SubTaxonomyId\":\"293\",\"SubTaxonomyName\":\"Other\"},{\"SubTaxonomyId\":\"294\",\"SubTaxonomyName\":\"Electronic\"},{\"SubTaxonomyId\":\"295\",\"SubTaxonomyName\":\"Piping\"},{\"SubTaxonomyId\":\"379\",\"SubTaxonomyName\":\"Assembly\"},{\"SubTaxonomyId\":\"426\",\"SubTaxonomyName\":\"Other Construction Trades\"}]},{\"Id\":\"28\",\"Name\":\"Clinical\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"269\",\"SubTaxonomyName\":\"Claims and Billing\"},{\"SubTaxonomyId\":\"270\",\"SubTaxonomyName\":\"Coding\"},{\"SubTaxonomyId\":\"271\",\"SubTaxonomyName\":\"Analysis\"},{\"SubTaxonomyId\":\"272\",\"SubTaxonomyName\":\"Admin\"},{\"SubTaxonomyId\":\"273\",\"SubTaxonomyName\":\"Certifications\"},{\"SubTaxonomyId\":\"274\",\"SubTaxonomyName\":\"Research\"},{\"SubTaxonomyId\":\"275\",\"SubTaxonomyName\":\"Trials\"},{\"SubTaxonomyId\":\"276\",\"SubTaxonomyName\":\"Tests and Functions\"},{\"SubTaxonomyId\":\"277\",\"SubTaxonomyName\":\"Other\"},{\"SubTaxonomyId\":\"278\",\"SubTaxonomyName\":\"Regulatory\"}]},{\"Id\":\"29\",\"Name\":\"Hardware Engineering\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"713\",\"SubTaxonomyName\":\"EMC / SI\"},{\"SubTaxonomyId\":\"714\",\"SubTaxonomyName\":\"Test Engineering\"},{\"SubTaxonomyId\":\"715\",\"SubTaxonomyName\":\"Design for X\"},{\"SubTaxonomyId\":\"716\",\"SubTaxonomyName\":\"New Product Integration\"},{\"SubTaxonomyId\":\"717\",\"SubTaxonomyName\":\"Reliability\"},{\"SubTaxonomyId\":\"730\",\"SubTaxonomyName\":\"Firmware Engineering\"},{\"SubTaxonomyId\":\"733\",\"SubTaxonomyName\":\"Mechanical Engineering\"},{\"SubTaxonomyId\":\"734\",\"SubTaxonomyName\":\"Optical Engineering\"}]},{\"Id\":\"31\",\"Name\":\"Technical Writing\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"427\",\"SubTaxonomyName\":\"Software\"},{\"SubTaxonomyId\":\"428\",\"SubTaxonomyName\":\"Training\"},{\"SubTaxonomyId\":\"429\",\"SubTaxonomyName\":\"Specs and Documentation\"},{\"SubTaxonomyId\":\"430\",\"SubTaxonomyName\":\"General\"},{\"SubTaxonomyId\":\"431\",\"SubTaxonomyName\":\"Proposals and Related\"}]},{\"Id\":\"32\",\"Name\":\"Degreed Accounting\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"254\",\"SubTaxonomyName\":\"Software\"},{\"SubTaxonomyId\":\"255\",\"SubTaxonomyName\":\"Tax\"},{\"SubTaxonomyId\":\"256\",\"SubTaxonomyName\":\"Other\"},{\"SubTaxonomyId\":\"257\",\"SubTaxonomyName\":\"Reconciliations\"},{\"SubTaxonomyId\":\"932\",\"SubTaxonomyName\":\"Accounting\"},{\"SubTaxonomyId\":\"933\",\"SubTaxonomyName\":\"Accounts Payable\"},{\"SubTaxonomyId\":\"940\",\"SubTaxonomyName\":\"Accounts Receivable\"},{\"SubTaxonomyId\":\"942\",\"SubTaxonomyName\":\"Auditing\"},{\"SubTaxonomyId\":\"944\",\"SubTaxonomyName\":\"Bookkeeping\"},{\"SubTaxonomyId\":\"950\",\"SubTaxonomyName\":\"CPA\"},{\"SubTaxonomyId\":\"951\",\"SubTaxonomyName\":\"Consulting\"},{\"SubTaxonomyId\":\"952\",\"SubTaxonomyName\":\"Cost Accounting\"},{\"SubTaxonomyId\":\"960\",\"SubTaxonomyName\":\"Management\"},{\"SubTaxonomyId\":\"961\",\"SubTaxonomyName\":\"Payroll\"},{\"SubTaxonomyId\":\"962\",\"SubTaxonomyName\":\"Reporting\"}]},{\"Id\":\"33\",\"Name\":\"Graphic Design\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"432\",\"SubTaxonomyName\":\"Software\"},{\"SubTaxonomyId\":\"433\",\"SubTaxonomyName\":\"Techniques and Activities\"},{\"SubTaxonomyId\":\"435\",\"SubTaxonomyName\":\"Technical, Blueprints and Schematics\"}]},{\"Id\":\"34\",\"Name\":\"Business Operations and General Business\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"436\",\"SubTaxonomyName\":\"Management Activities or Functions\"},{\"SubTaxonomyId\":\"437\",\"SubTaxonomyName\":\"General Skills and Activities\"}]},{\"Id\":\"36\",\"Name\":\"Travel\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"487\",\"SubTaxonomyName\":\"Travel Software\"},{\"SubTaxonomyId\":\"488\",\"SubTaxonomyName\":\"Travel Related\"}]},{\"Id\":\"37\",\"Name\":\"Recruiting\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"489\",\"SubTaxonomyName\":\"Recruiting Software\"},{\"SubTaxonomyId\":\"492\",\"SubTaxonomyName\":\"Recruiting Tasks and Activities\"},{\"SubTaxonomyId\":\"493\",\"SubTaxonomyName\":\"Executive Recruiting\"},{\"SubTaxonomyId\":\"494\",\"SubTaxonomyName\":\"IT Recruiting\"}]},{\"Id\":\"44\",\"Name\":\"Petrochemical\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"438\",\"SubTaxonomyName\":\"Equipment\"},{\"SubTaxonomyId\":\"439\",\"SubTaxonomyName\":\"Standards\"},{\"SubTaxonomyId\":\"440\",\"SubTaxonomyName\":\"Activities\"},{\"SubTaxonomyId\":\"441\",\"SubTaxonomyName\":\"Drilling\"},{\"SubTaxonomyId\":\"442\",\"SubTaxonomyName\":\"Refining\"},{\"SubTaxonomyId\":\"443\",\"SubTaxonomyName\":\"Software\"},{\"SubTaxonomyId\":\"444\",\"SubTaxonomyName\":\"General\"}]},{\"Id\":\"45\",\"Name\":\"Transmission \\u0026 Distribution\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"324\",\"SubTaxonomyName\":\"Overhead\"},{\"SubTaxonomyId\":\"325\",\"SubTaxonomyName\":\"Substation\"},{\"SubTaxonomyId\":\"326\",\"SubTaxonomyName\":\"Transmission\"},{\"SubTaxonomyId\":\"327\",\"SubTaxonomyName\":\"Other\"}]},{\"Id\":\"46\",\"Name\":\"Call Center or Help Desk or Customer Service\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"128\",\"SubTaxonomyName\":\"Call Center\"},{\"SubTaxonomyId\":\"129\",\"SubTaxonomyName\":\"Help Desk\"},{\"SubTaxonomyId\":\"130\",\"SubTaxonomyName\":\"Customer Facing\"},{\"SubTaxonomyId\":\"131\",\"SubTaxonomyName\":\"Management\"},{\"SubTaxonomyId\":\"279\",\"SubTaxonomyName\":\"Other\"}]},{\"Id\":\"64\",\"Name\":\"Training\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"359\",\"SubTaxonomyName\":\"Computer-Based\"},{\"SubTaxonomyId\":\"360\",\"SubTaxonomyName\":\"One On One\"},{\"SubTaxonomyId\":\"361\",\"SubTaxonomyName\":\"Other\"}]},{\"Id\":\"69\",\"Name\":\"QA and QC\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"445\",\"SubTaxonomyName\":\"Standards\"},{\"SubTaxonomyId\":\"446\",\"SubTaxonomyName\":\"Techniques\"},{\"SubTaxonomyId\":\"447\",\"SubTaxonomyName\":\"DT\"},{\"SubTaxonomyId\":\"448\",\"SubTaxonomyName\":\"NDT\"},{\"SubTaxonomyId\":\"931\",\"SubTaxonomyName\":\"Software\"},{\"SubTaxonomyId\":\"934\",\"SubTaxonomyName\":\"Manufacturing\"},{\"SubTaxonomyId\":\"941\",\"SubTaxonomyName\":\"Other\"}]},{\"Id\":\"71\",\"Name\":\"Strategy and Planning\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"501\",\"SubTaxonomyName\":\"Modeling\"},{\"SubTaxonomyId\":\"502\",\"SubTaxonomyName\":\"Planning and Estimating\"},{\"SubTaxonomyId\":\"876\",\"SubTaxonomyName\":\"Workflow and Processes\"}]},{\"Id\":\"72\",\"Name\":\"Installation, Maintenance, Repair\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"449\",\"SubTaxonomyName\":\"IT Related\"},{\"SubTaxonomyId\":\"450\",\"SubTaxonomyName\":\"Preventative\"},{\"SubTaxonomyId\":\"451\",\"SubTaxonomyName\":\"General Installation\"},{\"SubTaxonomyId\":\"452\",\"SubTaxonomyName\":\"General Repair\"}]},{\"Id\":\"74\",\"Name\":\"Biotech/Life Sciences\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"629\",\"SubTaxonomyName\":\"Phylogenetics\"}]},{\"Id\":\"75\",\"Name\":\"Pharmaceutical\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"453\",\"SubTaxonomyName\":\"Drug\"},{\"SubTaxonomyId\":\"454\",\"SubTaxonomyName\":\"Non-Drug\"}]},{\"Id\":\"77\",\"Name\":\"Education\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"503\",\"SubTaxonomyName\":\"Recordkeeping\"},{\"SubTaxonomyId\":\"504\",\"SubTaxonomyName\":\"Curricula\"},{\"SubTaxonomyId\":\"505\",\"SubTaxonomyName\":\"Positions\"},{\"SubTaxonomyId\":\"506\",\"SubTaxonomyName\":\"Activities and Tasks\"},{\"SubTaxonomyId\":\"507\",\"SubTaxonomyName\":\"Programs and Projects\"}]},{\"Id\":\"78\",\"Name\":\"Retail\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"215\",\"SubTaxonomyName\":\"General Management\"},{\"SubTaxonomyId\":\"217\",\"SubTaxonomyName\":\"Safety and Loss Prevention\"},{\"SubTaxonomyId\":\"218\",\"SubTaxonomyName\":\"Operations\"},{\"SubTaxonomyId\":\"219\",\"SubTaxonomyName\":\"Sales\"},{\"SubTaxonomyId\":\"508\",\"SubTaxonomyName\":\"POS Systems\"},{\"SubTaxonomyId\":\"509\",\"SubTaxonomyName\":\"Money Handling\"},{\"SubTaxonomyId\":\"510\",\"SubTaxonomyName\":\"Types\"},{\"SubTaxonomyId\":\"511\",\"SubTaxonomyName\":\"Positions\"}]},{\"Id\":\"80\",\"Name\":\"General Management\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"516\",\"SubTaxonomyName\":\"Budget Related\"},{\"SubTaxonomyId\":\"517\",\"SubTaxonomyName\":\"People Oriented\"},{\"SubTaxonomyId\":\"518\",\"SubTaxonomyName\":\"Operations and Admin\"},{\"SubTaxonomyId\":\"519\",\"SubTaxonomyName\":\"Management and Management Tasks\"}]},{\"Id\":\"82\",\"Name\":\"Hotel and Hospitality\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"172\",\"SubTaxonomyName\":\"Banquet and Conventions\"},{\"SubTaxonomyId\":\"177\",\"SubTaxonomyName\":\"Front Office\"},{\"SubTaxonomyId\":\"180\",\"SubTaxonomyName\":\"Restaurant and Bar\"},{\"SubTaxonomyId\":\"512\",\"SubTaxonomyName\":\"Cooking and Food\"},{\"SubTaxonomyId\":\"513\",\"SubTaxonomyName\":\"Management\"},{\"SubTaxonomyId\":\"514\",\"SubTaxonomyName\":\"Food Industry Classifications\"},{\"SubTaxonomyId\":\"515\",\"SubTaxonomyName\":\"Safety Health and Sanitation\"}]},{\"Id\":\"85\",\"Name\":\"Architecture\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"467\",\"SubTaxonomyName\":\"Software\"},{\"SubTaxonomyId\":\"468\",\"SubTaxonomyName\":\"Certs\"},{\"SubTaxonomyId\":\"469\",\"SubTaxonomyName\":\"Urban Planning\"},{\"SubTaxonomyId\":\"470\",\"SubTaxonomyName\":\"Commercial and Industrial\"},{\"SubTaxonomyId\":\"471\",\"SubTaxonomyName\":\"Site\"},{\"SubTaxonomyId\":\"472\",\"SubTaxonomyName\":\"Structure Subparts\"},{\"SubTaxonomyId\":\"473\",\"SubTaxonomyName\":\"General\"},{\"SubTaxonomyId\":\"474\",\"SubTaxonomyName\":\"Residential and Interior Design\"},{\"SubTaxonomyId\":\"475\",\"SubTaxonomyName\":\"Civil\"},{\"SubTaxonomyId\":\"476\",\"SubTaxonomyName\":\"Landscape\"},{\"SubTaxonomyId\":\"477\",\"SubTaxonomyName\":\"Structural\"}]},{\"Id\":\"86\",\"Name\":\"Government\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"522\",\"SubTaxonomyName\":\"Security Clearances\"},{\"SubTaxonomyId\":\"523\",\"SubTaxonomyName\":\"Procurement\"},{\"SubTaxonomyId\":\"524\",\"SubTaxonomyName\":\"Regulatory\"}]},{\"Id\":\"87\",\"Name\":\"Warehouse\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"495\",\"SubTaxonomyName\":\"Warehouse Manual Labor\"},{\"SubTaxonomyId\":\"496\",\"SubTaxonomyName\":\"Warehouse Related\"},{\"SubTaxonomyId\":\"497\",\"SubTaxonomyName\":\"General Tasks\"},{\"SubTaxonomyId\":\"498\",\"SubTaxonomyName\":\"Shipping\"}]},{\"Id\":\"89\",\"Name\":\"Bookkeeping, Office Management\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"533\",\"SubTaxonomyName\":\"Bookeeping Tasks\"},{\"SubTaxonomyId\":\"534\",\"SubTaxonomyName\":\"Office Tasks\"},{\"SubTaxonomyId\":\"535\",\"SubTaxonomyName\":\"Payroll Tasks\"},{\"SubTaxonomyId\":\"536\",\"SubTaxonomyName\":\"Bookeeping Software\"}]},{\"Id\":\"90\",\"Name\":\"Personal Attributes\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"528\",\"SubTaxonomyName\":\"Attitude\"},{\"SubTaxonomyId\":\"529\",\"SubTaxonomyName\":\"Languages\"},{\"SubTaxonomyId\":\"531\",\"SubTaxonomyName\":\"Driver Licensing\"},{\"SubTaxonomyId\":\"532\",\"SubTaxonomyName\":\"Aptitudes\"}]},{\"Id\":\"91\",\"Name\":\"Translations and Language Work\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"901\",\"SubTaxonomyName\":\"Translation\"},{\"SubTaxonomyId\":\"902\",\"SubTaxonomyName\":\"Semantics\"}]},{\"Id\":\"92\",\"Name\":\"Knowledge and Learning Management\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"903\",\"SubTaxonomyName\":\"General Knowledge and Learning Management\"}]},{\"Id\":\"93\",\"Name\":\"User Experience\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"558\",\"SubTaxonomyName\":\"User Research\"},{\"SubTaxonomyId\":\"559\",\"SubTaxonomyName\":\"Ergonomics\"},{\"SubTaxonomyId\":\"560\",\"SubTaxonomyName\":\"UX Design\"},{\"SubTaxonomyId\":\"561\",\"SubTaxonomyName\":\"Human Computer Interaction\"},{\"SubTaxonomyId\":\"562\",\"SubTaxonomyName\":\"Interaction Design\"},{\"SubTaxonomyId\":\"563\",\"SubTaxonomyName\":\"Usability Design\"},{\"SubTaxonomyId\":\"564\",\"SubTaxonomyName\":\"Motion Design\"},{\"SubTaxonomyId\":\"565\",\"SubTaxonomyName\":\"Visual Design\"},{\"SubTaxonomyId\":\"566\",\"SubTaxonomyName\":\"Industrial Design\"},{\"SubTaxonomyId\":\"567\",\"SubTaxonomyName\":\"Information Architect\"},{\"SubTaxonomyId\":\"568\",\"SubTaxonomyName\":\"Integration Design\"},{\"SubTaxonomyId\":\"569\",\"SubTaxonomyName\":\"Graphic Design\"},{\"SubTaxonomyId\":\"570\",\"SubTaxonomyName\":\"Game Design\"},{\"SubTaxonomyId\":\"571\",\"SubTaxonomyName\":\"Web Design\"},{\"SubTaxonomyId\":\"572\",\"SubTaxonomyName\":\"Interactive Design\"},{\"SubTaxonomyId\":\"573\",\"SubTaxonomyName\":\"Product Design\"}]},{\"Id\":\"95\",\"Name\":\"Healthcare Non-physician Non-nurse\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"157\",\"SubTaxonomyName\":\"Administration\"},{\"SubTaxonomyId\":\"158\",\"SubTaxonomyName\":\"Allied Health\"},{\"SubTaxonomyId\":\"159\",\"SubTaxonomyName\":\"Sanitation and Sterilization\"},{\"SubTaxonomyId\":\"162\",\"SubTaxonomyName\":\"Facilities/Plant/Maintenance\"},{\"SubTaxonomyId\":\"165\",\"SubTaxonomyName\":\"Operations\"},{\"SubTaxonomyId\":\"168\",\"SubTaxonomyName\":\"Safety and Security\"},{\"SubTaxonomyId\":\"169\",\"SubTaxonomyName\":\"Social Work\"},{\"SubTaxonomyId\":\"171\",\"SubTaxonomyName\":\"Support Services\"}]},{\"Id\":\"96\",\"Name\":\"Executive\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"147\",\"SubTaxonomyName\":\"CEO\"},{\"SubTaxonomyId\":\"148\",\"SubTaxonomyName\":\"CFO\"},{\"SubTaxonomyId\":\"149\",\"SubTaxonomyName\":\"CIO\"},{\"SubTaxonomyId\":\"150\",\"SubTaxonomyName\":\"COO\"},{\"SubTaxonomyId\":\"151\",\"SubTaxonomyName\":\"CTO\"},{\"SubTaxonomyId\":\"964\",\"SubTaxonomyName\":\"CXO\"},{\"SubTaxonomyId\":\"965\",\"SubTaxonomyName\":\"CLO\"},{\"SubTaxonomyId\":\"966\",\"SubTaxonomyName\":\"CCO\"},{\"SubTaxonomyId\":\"967\",\"SubTaxonomyName\":\"CAO\"},{\"SubTaxonomyId\":\"968\",\"SubTaxonomyName\":\"CSO\"},{\"SubTaxonomyId\":\"969\",\"SubTaxonomyName\":\"CQO\"}]},{\"Id\":\"97\",\"Name\":\"Purchasing, Procurement, Inventory Control, Supply Chain\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"458\",\"SubTaxonomyName\":\"Purchasing\"},{\"SubTaxonomyId\":\"459\",\"SubTaxonomyName\":\"Inventory\"},{\"SubTaxonomyId\":\"460\",\"SubTaxonomyName\":\"Logistics\"},{\"SubTaxonomyId\":\"461\",\"SubTaxonomyName\":\"Supply Chain\"}]},{\"Id\":\"98\",\"Name\":\"Security\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"462\",\"SubTaxonomyName\":\"Government Clearance\"},{\"SubTaxonomyId\":\"463\",\"SubTaxonomyName\":\"IT\"},{\"SubTaxonomyId\":\"464\",\"SubTaxonomyName\":\"Disaster\"},{\"SubTaxonomyId\":\"465\",\"SubTaxonomyName\":\"Physical\"},{\"SubTaxonomyId\":\"466\",\"SubTaxonomyName\":\"Financial, Accounting and Processes\"}]},{\"Id\":\"99\",\"Name\":\"Nursing\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"366\",\"SubTaxonomyName\":\"Phlebotomy\"},{\"SubTaxonomyId\":\"367\",\"SubTaxonomyName\":\"Acute\"},{\"SubTaxonomyId\":\"368\",\"SubTaxonomyName\":\"Pediatric\"},{\"SubTaxonomyId\":\"369\",\"SubTaxonomyName\":\"Oncology\"},{\"SubTaxonomyId\":\"370\",\"SubTaxonomyName\":\"Dental\"},{\"SubTaxonomyId\":\"371\",\"SubTaxonomyName\":\"Radiology\"},{\"SubTaxonomyId\":\"417\",\"SubTaxonomyName\":\"Admin\"},{\"SubTaxonomyId\":\"418\",\"SubTaxonomyName\":\"CPR, EMT and First Aid\"},{\"SubTaxonomyId\":\"419\",\"SubTaxonomyName\":\"Long Term\"},{\"SubTaxonomyId\":\"420\",\"SubTaxonomyName\":\"Pain\"},{\"SubTaxonomyId\":\"421\",\"SubTaxonomyName\":\"Billing and Coding\"},{\"SubTaxonomyId\":\"422\",\"SubTaxonomyName\":\"Nurse Asst\"},{\"SubTaxonomyId\":\"423\",\"SubTaxonomyName\":\"Educator\"},{\"SubTaxonomyId\":\"424\",\"SubTaxonomyName\":\"Cardio\"},{\"SubTaxonomyId\":\"425\",\"SubTaxonomyName\":\"Blood and Circulatory\"},{\"SubTaxonomyId\":\"455\",\"SubTaxonomyName\":\"Other Asst\"},{\"SubTaxonomyId\":\"456\",\"SubTaxonomyName\":\"Nursing Vocations and Certs\"},{\"SubTaxonomyId\":\"457\",\"SubTaxonomyName\":\"Techniques\"}]},{\"Id\":\"1000\",\"Name\":\"No dominant taxonomy\",\"SubTaxonomies\":[{\"SubTaxonomyId\":\"1001\",\"SubTaxonomyName\":\"Not enough data\"}]}]";
    }

    /// <inheritdoc/>
    public abstract class FoundTaxonomy<T> : ITaxonomy<T>
    {
        /// <inheritdoc/>
        public List<T> SubTaxonomies { get; set; }

        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <summary>
        /// The percent (0-100) of skills found in the document that belong to this taxonomy
        /// </summary>
        public int PercentOfOverall { get; set; }
    }
}
