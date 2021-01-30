using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.Core.Basemodel.BaseEntity
{
    public class BaseEntity : Base.Base, IBaseEntity
    {
        public virtual DateTimeOffset? CreatedAt { get; set; }
        public virtual DateTimeOffset? UpdatedAt { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string UpdatedBy { get; set; }
    }
}
