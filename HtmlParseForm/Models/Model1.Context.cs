﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HtmlParseForm.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RedditScanEntities : DbContext
    {
        public RedditScanEntities()
            : base("name=RedditScanEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<List> List { get; set; }
        public virtual DbSet<Reply> Reply { get; set; }
        public virtual DbSet<FormosaList> FormosaList { get; set; }
        public virtual DbSet<RedditAll> RedditAll { get; set; }
        public virtual DbSet<FormosaReply> FormosaReply { get; set; }
        public virtual DbSet<FormosaReply2> FormosaReply2 { get; set; }
        public virtual DbSet<ChenList> ChenList { get; set; }
        public virtual DbSet<ChenProfile> ChenProfile { get; set; }
    }
}
