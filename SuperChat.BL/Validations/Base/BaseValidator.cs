using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.BL.Validations.Base
{
    public abstract class BaseValidator<T> : AbstractValidator<T>, IBaseValidator
    {

        /// <summary>
        /// This method is the responsible to decide which RuleSet si going to be excecuted base on parameter evaluation
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public virtual string[] GetRuleSetName(object dto)
        {
            return new string[] { "default" };
        }
    }
}
