using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFoodService.Models;
using BCrypt.Net;
using System.Security.Cryptography;
using AutoMapper;

namespace OnlineFoodService.Repository
{
    public class Sqlrepo:IRegister
    {
        private readonly onlinefoodservicecontext _dbcontext;
        private readonly IMapper _mapper;


        public Sqlrepo(onlinefoodservicecontext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<UserDetail>>> Getusers()
        {
            return await _dbcontext.UserDetails.ToListAsync();
        }

        public async Task<ActionResult<UserDetail>> PostRegisterUser(UserDetail register)
        {
            /*if (_dbcontext.UserDetails.Any(x => x.EmailId == register.EmailId))
            {
                return new BadRequestObjectResult(new { Message = "User already registered" });
            }*/
               

            // map model to new user object
            var user = _mapper.Map<UserDetail>(register);

            // hash password
            
            user.Password = BCrypt.Net.BCrypt.HashPassword(register.Password);

            _dbcontext.UserDetails.Add(user);
            await _dbcontext.SaveChangesAsync();

            return register;
        }

        public async Task<ActionResult<UserDetail>> DeleteRegisterUser(int userId)
        {
            var userDetail = await _dbcontext.UserDetails.FindAsync(userId);
            var cart = await _dbcontext.Carts
            .Where(c => c.UserId == userId)
            .FirstOrDefaultAsync();
            if (userDetail == null )
            {
                throw new NullReferenceException("Sorry, user not found.");
            }
            else
            {
                if (cart != null)
                {
                    _dbcontext.Carts.Remove(cart);
                    _dbcontext.UserDetails.Remove(userDetail);
                    await _dbcontext.SaveChangesAsync();
                }

                else
                {
                    _dbcontext.UserDetails.Remove(userDetail);
                    await _dbcontext.SaveChangesAsync();
                }

                return userDetail;
            }
        }

        public async Task<ActionResult<UserDetail>> GetRegisterUser(int userId)
        {
            var user = await _dbcontext.UserDetails
            .Where(c => c.UserId == userId)
            .FirstOrDefaultAsync();

            Console.WriteLine(user);

            if (user == null)
            {
                throw new NullReferenceException("Sorry, User not found.");
            }

            return user;
        }
        public async Task<ActionResult<UserDetail>> EditRegisterPassword(int userId, UserDetail register)
        {
            register.Password = BCrypt.Net.BCrypt.HashPassword(register.Password);

            _dbcontext.Entry(register).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return register;
        }

        public async Task<ActionResult<UserDetail>> EditRegisterUser(int userId, UserDetail register)
        {

            _dbcontext.Entry(register).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return register;
        }

        public async Task<ActionResult<int>> GetUserCount()
        {
            return await _dbcontext.UserDetails.CountAsync();

        }

        
    }
}
