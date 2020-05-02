using Model.Common;
using Model.DTO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Util
{
    public static class UserMapper
    {
        public static User toUser(UserDTO model, User user)
        {
            if (model.id == 0)
            {
                user.createdAt = DateTime.Now;
            }
            if(model.password != null)
            {
                user.password = BcryptPass.HashPassword(model.password);
            }
            user.username = model.username;
            user.email = model.email;
            user.address = model.address;
            user.status = model.status;
            user.updatedAt = DateTime.Now;
            return user;
        }
        public static UserDTO toDTO(User user)
        {
            var dto = new UserDTO();
            dto.id = user.id;
            dto.username = user.username;
            dto.fullname = user.fullname;
            dto.phonenumber = user.phonenumber;
            dto.email = user.email;
            dto.address = user.address;
            dto.status = user.status;
            return dto;
        }
    }
}
