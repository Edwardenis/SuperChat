using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SuperChat.BL.DTOs;
using SuperChat.BL.Validations.Base;
using SuperChat.Datamodel.Contexts;
using SuperChat.Datamodel.Entities;
using SuperChat.Datamodel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperChat.BL.Validations
{
    public class ChatRoomValidator : BaseValidator<ChatRoomDto>
    {
        private readonly IUnitOfWork<SuperChatDbContext> _uow;
        public ChatRoomValidator(IUnitOfWork<SuperChatDbContext> uow)
        {
            _uow = uow;
            //
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("The chat room name is required");

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("The chat room code is required");

            RuleFor(x => x.Code)
                .MustAsync(BeUnique)
                .WithMessage("This chat room code is already in use");
        }

        private async Task<bool> BeUnique(string chatRoomCode, CancellationToken arg2)
        {
            var chatRoom = await _uow.GetRepository<ChatRoom>()
                            .GetNoTracking(c => c.Code == chatRoomCode)
                            .FirstOrDefaultAsync();

            var chatRoomAlreadyExist = chatRoom != null;

            return !chatRoomAlreadyExist;
        }
    }
}
