using SuperChat.Datamodel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.Services.JWTFactory
{
    public interface IJwtFactory
    {
        string GenerateToken(AppUser user);
    }
}
