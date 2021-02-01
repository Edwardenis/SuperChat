using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperChat.API.Controllers.Base
{
    public interface IBaseController
    {
        IValidatorFactory _validationFactory { get; set; }
        //UnprocessableEntityObjectResult UnprocessableEntity(object error);
    }
}
