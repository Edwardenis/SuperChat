using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.BL.DTOs.Base
{
    public class BaseDto : IBaseDto
    {
        public virtual int? Id { get; set; }
    }
}
