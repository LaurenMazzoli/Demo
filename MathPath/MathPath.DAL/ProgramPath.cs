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
    
    public partial class ProgramPath
    {
        public int ProgramPathId { get; set; }
        public Nullable<int> ProgramId { get; set; }
        public Nullable<int> PathId { get; set; }
        public string Name { get; set; }
        public string RequiredCourse { get; set; }
    
        public virtual Path Path { get; set; }
        public virtual Program Program { get; set; }
    }
}
