using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MUDhub.Prototype.Server.Configurations;
using MUDhub.Prototype.Server.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Globalization;

namespace MUDhub.Prototype.Server.Services
{
    public class UserManager
    {

        public UserManager(ApplicationDbContext dbContext, IOptions<UserSettings> options) 
            : this(dbContext, options?.Value ?? throw new ArgumentNullException(nameof(options)))
        {

            
        }
        public UserManager(ApplicationDbContext dbContext, UserSettings options)
        {
            _dbContext = dbContext;
            _userSettings = options ?? throw new ArgumentNullException(nameof(options));
        }

        private readonly UserSettings _userSettings;
        private readonly ApplicationDbContext _dbContext;

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            User? user = await GetUserAsync(username)
                .ConfigureAwait(false);
            if (user == null)
                return new LoginResult(false);

            bool success = CheckPassword(user, password);

            if (!success)
                return new LoginResult(false);

            var token = CreateToken(user.Id);

            return new LoginResult(true, token, user);

        }
        public async Task<RegisterResult> RegisterAsync(string username, string password, bool autologin = true)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username)
                .ConfigureAwait(false);
            if (!(user is null))
                return new RegisterResult(false, true);
            user = new User
            {
                Username = username,
                PasswordHash = CreatePasswordHash(password)
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync()
                .ConfigureAwait(false);
            LoginResult? result = null;
            if (autologin)
            {
                result = await LoginAsync(username, password)
                    .ConfigureAwait(false);
            }
            return new RegisterResult(true, loginResult: result);
        }
        public Task<User?> GetUserAsync(string name)
        {
            return _dbContext.Users.FirstOrDefaultAsync(u => u.Username == name);
        }

        public Task<User?> GetUserByIdAsync(string id)
        {
            return _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public IAsyncEnumerable<User> GetUsersAsync()
        {
            return _dbContext.Users.AsAsyncEnumerable();
        }


        public static string CreatePasswordHash(string password)
        {
            byte[] data;
            using (HashAlgorithm algorithm = SHA256.Create())
                data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));

            string passwordHash = string.Create(data.Length, data, (target, arg) =>
            {
                for (int i = 0; i < arg.Length; i += 2)
                {
                    var t = arg[i].ToString("X2", CultureInfo.InvariantCulture);
                    target[i] = Convert.ToChar(t[0]);
                    target[i + 1] = Convert.ToChar(t[1]);
                }
            });
            return passwordHash;
        }
        private bool CheckPassword(User user, string password)
        {
            string passwordHash = CreatePasswordHash(password);
            return passwordHash == user.PasswordHash;

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
                    new Claim(ClaimTypes.NameIdentifier, userid),
                    new Claim(ClaimTypes.Role, "Administrator"),
                    new Claim(ClaimTypes.Role, "MUD Master"),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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
