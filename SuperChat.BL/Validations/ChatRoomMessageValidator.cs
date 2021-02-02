using FluentValidation;
using SuperChat.BL.DTOs;
using SuperChat.BL.Validations.Base;
using SuperChat.Datamodel.Contexts;
using SuperChat.Datamodel.Entities;
using SuperChat.Datamodel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperChat.BL.Validations
{
    public class ChatRoomMessageValidator : BaseValidator<ChatRoomMessageDto>
    {
        private readonly IUnitOfWork<SuperChatDbContext> _uow;
        public ChatRoomMessageValidator(IUnitOfWork<SuperChatDbContext> uow)
        {
            _uow = uow;
            //
            RuleFor(x => x.FromUser)
                .NotEmpty()
                .WithMessage("The user identifier is required");

            RuleFor(x => x.MessageText)
                .NotEmpty()
                .WithMessage("The message cannot be empty");

            RuleFor(x => x.ChatRoomId)
                .NotEqual(0)
                .WithMessage("The chat room id cannot be empty");

            RuleFor(x => x.ChatRoomId)
                .MustAsync(Exists)
                .WithMessage("This chat room does not exist");
        }

        private async Task<bool> Exists(int chatRoomId, CancellationToken arg2)
        {
            var chatRoom = await _uow.GetRepository<ChatRoom>()
                            .GetById(chatRoomId);

            var chatRoomExists = chatRoom != null;

            return chatRoomExists;
        }
    }
}
