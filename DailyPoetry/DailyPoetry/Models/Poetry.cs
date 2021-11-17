using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DailyPoetry.Models
{
    /// <summary>
    /// 诗词类
    /// </summary>
    [Table("works")]
    public class Poetry
    {
        /// <summary>
        /// id 主键
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Column("name")]
        public int Name { get; set; }

        [Column("author_name")]
        public string AuthorName { get; set; }

        [Column("dynasty")]
        public string Dynasty { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("translation")]
        public string Translation { get; set; }

        [Column("layout")]
        public string Layout { get; set; }

        /// <summary>
        /// 布局类型
        /// </summary>
        public const string CenterLayout = "center";
        public const string IndentLayout = "indent";

        private string _snippet;

        [Ignore]
        public string Snippet => _snippet ?? (_snippet = Content.Split('。')[0].Replace("\r\n",""));

    }

}
