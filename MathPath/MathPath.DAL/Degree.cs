//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MathPath.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Degree
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ProgramCode { get; set; }
        public bool AdditionalRequirements { get; set; }
        public bool AidEligible { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime DateModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}