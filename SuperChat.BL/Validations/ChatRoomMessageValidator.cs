using FluentValidation;
using SuperChat.BL.DTOs;
using SuperChat.BL.Validations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.BL.Validations
{
    public class ChatRoomMessageValidator : BaseValidator<ChatRoomMessageDto>
    {
        public ChatRoomMessageValidator()
        {
            //
            RuleFor(x => x.FromUser)
                .NotEmpty()
                .WithMessage("The user identifier is required");

            RuleFor(x => x.MessageText)
                .NotEmpty()
                .WithMessage("The message cannot be empty");
        }
    }
}
