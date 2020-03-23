using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MUDhub.Prototype.Server.Configurations;
using MUDhub.Prototype.Server.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace MUDhub.Prototype.Server.Services
{
    public class UserManager
    {

        public UserManager(ApplicationDbContext dbContext, IOptions<UserSettings> options, ILogger<UserManager> logger)
        {

            _dbContext = dbContext;
            _logger = logger;
            _userSettings = options.Value;
        }

        private readonly UserSettings _userSettings;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<UserManager> _logger;

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            var user = await GetUserAsync(username);
            if (user == null)
                return new LoginResult(false);

            bool success = CheckPassword(user, password);

            if (!success)
                return new LoginResult(false);

            var token = CreateToken(user.Id);

            return new LoginResult(true, token, user);

        }

        private bool CheckPassword(User user, string password)
        {
            string passwordHash = CreatePasswordHash(password);
            return passwordHash == user.PasswordHash;

        }

        public static string CreatePasswordHash(string password)
        {
            byte[] data;
            using (HashAlgorithm algorithm = SHA256.Create())
                data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));

            string passwordHash = string.Create(data.Length, data, (target, arg) =>
            {
                for (int i = 0; i < arg.Length; i+=2)
                {
                    var t = arg[i].ToString("X2");
                    target[i] = Convert.ToChar(t[0]);
                    target[i+1] = Convert.ToChar(t[1]);
                }
            });
            return passwordHash;
        }

        private string CreateToken(string userid)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_userSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userid),
                    //new Claim(ClaimTypes.)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<RegisterResult> RegisterAsync(string username, string password, bool autologin = true)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (!(user is null))
                return new RegisterResult(false, true);
            user = new User
            {
                Username = username,
                PasswordHash = CreatePasswordHash(password)
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            LoginResult? result = null;
            if (autologin)
            {
                result = await LoginAsync(username, password);
            }
            return new RegisterResult(true, loginResult: result);
        }

        public Task<User?> GetUserAsync(string name)
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _dbContext.Users.FirstOrDefaultAsync(u => u.Username == name);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public IAsyncEnumerable<User> GetUsersAsync()
        {
            return _dbContext.Users.AsAsyncEnumerable();
        }
    }

    public class RegisterResult
    {

        public RegisterResult(bool succeeded, bool usernameAlreadyExists = false, LoginResult? loginResult = null)
        {
            Succeeded = succeeded;
            UsernameAlreadyExists = usernameAlreadyExists;
            LoginResult = loginResult;
        }
        public bool Succeeded { get; }
        public bool UsernameAlreadyExists { get; }
        public LoginResult? LoginResult { get; }

    }

    public class LoginResult
    {

        public LoginResult(bool succeeded, string? token = null, User? user = null)
        {
            Succeeded = succeeded;
            Token = token;
            User = user;
        }

        public bool Succeeded { get; }

        public string? Token { get; }

        public User? User { get; }

    }
}
