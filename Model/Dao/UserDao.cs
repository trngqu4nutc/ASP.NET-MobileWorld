using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Common;
using PagedList;
using Model.DTO;
using Model.Util;

namespace Model.Dao
{
    public class UserDao
    {
        OnlineShopDbContext _context = null;
        public UserDao()
        {
            _context = new OnlineShopDbContext();
        }
        public UserDTO GetUserById(int id)
        {
            var user = _context.Users.Find(id);
            return UserMapper.toDTO(user);
        }
        public int Login(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.username == username);
            if (user == null)
            {
                return 0;
            }
            else
            {
                if(user.status == false)
                {
                    return -1;
                }
                else
                {
                    if (BcryptPass.ValidatePassword(password, user.password))
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }
        public UserDTO GetByUserName(string username)
        {
            int role = 0;
            var query = from u in _context.Users
                        join ur in _context.UserRoles on u.id equals ur.userid
                        where u.username == username
                        select new { u.fullname, ur.roleid };
            var user = new UserDTO();
            foreach(var item in query)
            {
                user.fullname = item.fullname;
                if(item.roleid == 3)
                {
                    role = 3;
                    break;
                }else if(item.roleid == 2)
                {
                    role = 2;
                }
                else
                {
                    role = 1;
                }
            }
            user.role = role;
            return user;
        }
        public bool Insert(UserDTO model)
        {
            var result = _context.Users.Count(x => x.username == model.username);
            if(result > 0)
            {
                return false;
            }
            else
            {
                try
                {
                    var user = new User();
                    user = UserMapper.toUser(model, user);
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    AddRole(model.username);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public int InsertFacebook(User model)
        {
            var user = _context.Users.SingleOrDefault(x => x.username == model.username);
            if(user == null)
            {
                model.password = BcryptPass.HashPassword("facebook");
                _context.Users.Add(model);
                _context.SaveChanges();
                AddRole(model.username);
                return model.id;
            }
            else
            {
                return user.id;
            }
        }
        public bool Update(UserDTO model)
        {
            var result = _context.Users.Count(x => x.username == model.username);
            if(result > 0)
            {
                try
                {
                    var user = _context.Users.Find(model.id);
                    if (user == null)
                    {
                        return false;
                    }
                    user = UserMapper.toUser(model, user);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public void AddRole(string username)
        {
            var user = _context.Users.SingleOrDefault(x => x.username == username);
            var userRole = new UserRole();
            userRole.roleid = 1;
            userRole.userid = user.id;
            userRole.createdAt = DateTime.Now;
            userRole.updatedAt = DateTime.Now;
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
        }
        public User FindById(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                return user;
            }
            catch(Exception)
            {
                return null;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public bool ChangeStatus(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                user.status = !user.status;
                _context.SaveChanges();
                return user.status;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Dictionary<int, string> AuthAccount(UserDTO userDTO)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            var user = _context.Users.SingleOrDefault(x => x.username == userDTO.username);
            if(user == null)
            {
                result.Add(0, "");
            }else if (!user.email.Equals(userDTO.email))
            {
                result.Add(-1, "");
            }
            else
            {
                var password = GeneratorPassword.Password();
                user.password = BcryptPass.HashPassword(password);
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    result.Add(-2, "");
                }
                result.Add(1, password);
            }
            return result;
        }
        public PagedResult<UserDTO> ListAllPaging(string name, string status, int page, int pageSize)
        {
            var model = from u in _context.Users
                        join ur in _context.UserRoles on u.id equals ur.userid
                        join r in _context.Roles on ur.roleid equals r.id
                        select new { u, r };
            if (!string.IsNullOrEmpty(name))
            {
                model = model.Where(x => x.u.fullname.Contains(name));
            }
            if (!string.IsNullOrEmpty(status))
            {
                bool check = bool.Parse(status);
                model = model.Where(x => x.u.status == check);
            }
            var pagedResult = new PagedResult<UserDTO>();
            pagedResult.TotalRecord = model.Count();
            pagedResult.Items = model.OrderBy(x => x.u.id).Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new UserDTO() {
                    id = x.u.id,
                    username = x.u.username,
                    fullname = x.u.fullname,
                    address = x.u.address,
                    email = x.u.email,
                    phonenumber = x.u.phonenumber,
                    status = x.u.status,
                    role = x.r.id
                }).ToList();
            return pagedResult;
        }
    }
}
