using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Core.Business
{
    public class ApplicationArea
    {
        [Key]
        [Column("ApplicationAreaId")]
        public Guid ApplicationAreaId { get; set; }

        [Column("ApplicationId")]
        public Guid ApplicationId { get; set; }

        [Column("AreaName")]
        public string AreaName { get; set; }

        [Column("AreaTitle")]
        public string AreaTitle { get; set; }

        [Column("Active")]
        public bool Active { get; set; }

        [Column("SortOrder")]
        public int SortOrder { get; set; }
    }
}
