using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.BL.DTOs.Base
{
    public class BaseEntityDto : BaseDto, IBaseEntityDto
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedAt { get; set; }
    }
}
