//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DuAnBao.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> PublishedDate { get; set; }
        public int AuthorId { get; set; }
    
        public virtual User_reader User_reader { get; set; }
    }
}
