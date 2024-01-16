using Microsoft.Data.SqlClient;
using RetrivalManagement.Models;
using RetrivalManagement.Models.Constants;
using RetrivalManagement.Models.DbEntities.Main;
using RetrivalManagement.Models.Enums;
using RetrivalManagement.UnitOfWork.UserManagement;
using RxWeb.Core.Data;
using RxWeb.Core.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RetrivalManagement.Domain.UserManagement
{
    public class UserDomain : IUserDomain
    {
        public UserDomain(IUserUow userUow, IDbContextManager<RetrivalManagementDbContext> dbContextManager, IPasswordHash passwordHash)
        {
            UserUow = userUow;
            ValidationMessages = new HashSet<string>();
            DbContextManager = dbContextManager;
            PasswordHash = passwordHash;
        }
        public List<User> GetAll()
        {
            return UserUow.Repository<User>().All().ToList();
        }
        //public string Get(string orderByColumn, string sortOrder, int pageIndex, int rowCount, string searchQuery, int userId, int roleId)
        //{
        //    var spParameters = new object[8];
        //    spParameters[0] = new SqlParameter() { ParameterName = "OrderByColumn", Value = string.IsNullOrEmpty(orderByColumn) ? "" : orderByColumn };
        //    spParameters[1] = new SqlParameter() { ParameterName = "SortOrder", Value = string.IsNullOrEmpty(sortOrder) ? "" : sortOrder };
        //    spParameters[2] = new SqlParameter() { ParameterName = "PageIndex", Value = pageIndex };
        //    spParameters[3] = new SqlParameter() { ParameterName = "RowCount", Value = rowCount };
        //    spParameters[4] = new SqlParameter() { ParameterName = "UserId", Value = userId };
        //    spParameters[5] = new SqlParameter() { ParameterName = "RoleId", Value = roleId };
        //    spParameters[6] = new SqlParameter() { ParameterName = "SearchQuery", Value = string.IsNullOrEmpty(searchQuery) ? "" : searchQuery };
        //    spParameters[7] = new SqlParameter() { ParameterName = "LoginUserId", Value = UserClaim.UserId };
        //    var storeProcSearchResult = DbContextManager.SqlQueryAsync<StoreProcSearchViewModel>("EXEC dbo.spUsers @OrderByColumn,@SortOrder,@PageIndex,@RowCount,@UserId,@RoleId,@SearchQuery,@LoginUserId", spParameters).Result;
        //    return storeProcSearchResult.SingleOrDefault()?.Result;
        //}

        public User Get(int id) => UserUow.Repository<User>().SingleOrDefault(t => t.UserId == id);

        public HashSet<string> AddValidation(User user)
        {
            CommonValidation(user);
            var isEmailExists = this.UserUow.Repository<User>().SingleOrDefault(t => t.EmailAddress == user.EmailAddress.Trim() && t.StatusId != Status.Deleted);
            if (isEmailExists != null)
            {
                ValidationMessages.Add("Email Address already exist.");
            }
            var isUserNameExist = this.UserUow.Repository<User>().SingleOrDefault(t => t.UserName == user.UserName.Trim() && t.StatusId != Status.Deleted);
            if (isUserNameExist != null)
            {
                ValidationMessages.Add("User name already exists");
            }

            var passwordPattern = new Regex(RegexConstant.systemPasswordPattern);
            if (!string.IsNullOrEmpty(user.UserPassword))
            {
            }
            else
            {
                ValidationMessages.Add("Please enter password");
            }

            if (user.UserPassword.ToString().Trim() != user.ConfirmPassword.ToString().Trim())
            {
                ValidationMessages.Add("Password and retype password don't match.");
            }
            return ValidationMessages;
        }

        public User Add(User user)
        {
            // Encrypt password

            var newPassword = this.PasswordHash.Encrypt(user.UserPassword);
            user.Password = newPassword.Signature;
            user.Salt = newPassword.Salt;
            user.EmailAddress = user.EmailAddress.ToLower();
            user.StatusId = Status.Active;

            UserUow.RegisterNewAsync<User>(user);
            UserUow.CommitAsync();

            return user;
        }

        public HashSet<string> UpdateValidation(User user)
        {
            if (user.IsEmailVerification)
            {
                return ValidationMessages;
            }
            else
            {
                if (user.IsChangePassword == true)
                {
                    var passwordHash = this.PasswordHash.Encrypt(user.OldPassword);
                    var OldPassword = passwordHash.Signature;

                    var userDetails = this.UserUow.Repository<User>().SingleOrDefault(t => t.EmailAddress == user.EmailAddress && t.UserId == user.UserId && t.StatusId != Status.Deleted);
                    if (userDetails != null)
                    {
                        if (!PasswordHash.VerifySignature(user.OldPassword, userDetails.Password, userDetails.Salt))
                        {
                            ValidationMessages.Add("Password doesn't match.");
                        }
                    }
                    else
                    {
                        ValidationMessages.Add("Your password reset link has expired");
                    }


                    if (user.OldPassword == user.UserPassword)
                    {
                        ValidationMessages.Add("Old Password is same as New Password.");
                    }

                }
                else
                {
                    CommonValidation(user);
                    var isEmailExists = this.UserUow.Repository<User>().SingleOrDefault(t => t.EmailAddress == user.EmailAddress && t.UserId != user.UserId && t.StatusId != Status.Deleted);
                    if (isEmailExists != null)
                    {
                        ValidationMessages.Add("Email ID already exist.");
                    }

                    var isUserNameExist = this.UserUow.Repository<User>().SingleOrDefault(t => t.UserName == user.UserName && t.UserId != user.UserId && t.StatusId != Status.Deleted);
                    if (isUserNameExist != null)
                    {
                        ValidationMessages.Add("User name already exists");
                    }
                }
            }
            return ValidationMessages;
        }

        public User Update(User user)
        {
            var userObject = UserUow.Repository<User>().SingleOrDefault(t => t.UserId == user.UserId && t.StatusId != Status.Deleted);
            if (user.IsChangePassword == true)
            {
                if (userObject != null)
                {
                    var passwordHash = this.PasswordHash.Encrypt(user.UserPassword);
                    user.Password = passwordHash.Signature;
                    user.Salt = passwordHash.Salt;
                    UserUow.RegisterDirtyAsync<User>(user);
                    UserUow.CommitAsync();
                    return user;
                    //SendEmail(user, false);
                }
            }
            else
            {
                user.Password = userObject.Password;
                user.Salt = userObject.Salt;
                this.UserUow.RegisterDirtyAsync<User>(user);
                this.UserUow.CommitAsync();
            }
            user.Password = null;
            user.Salt = null;
            return user;
        }

        public HashSet<string> DeleteValidation(int id)
        {

            return ValidationMessages;
        }

        public void Delete(int id)
        {
            var user = UserUow.Repository<User>().FindByKey(id);
            if (user != null)
            {
                user.StatusId = Status.Deleted;
                UserUow.RegisterDirtyAsync<User>(user);
                UserUow.CommitAsync();
            }

        }

        private void CommonValidation(User user)
        {

            var emailPattern = new Regex(RegexConstant.systemEmailPattern);
            if (!string.IsNullOrEmpty(user.EmailAddress))
            {
                var emailMatchString = emailPattern.IsMatch(user.EmailAddress);
                if (!emailMatchString)
                {
                    ValidationMessages.Add(user.UserName + ": " + "Invalid email address");
                }
            }
        }
        private IPasswordHash PasswordHash { get; set; }
        private IUserUow UserUow { get; set; }

        private HashSet<string> ValidationMessages { get; set; }

        private IDbContextManager<RetrivalManagementDbContext> DbContextManager { get; set; }

    }
    public interface IUserDomain
    {
        List<User> GetAll();
        //string Get(string orderByColumn, string sortOrder, int pageIndex, int rowCount, string searchQuery, int userId, int roleId);
        User Get(int id);

        HashSet<string> AddValidation(User user);
        HashSet<string> UpdateValidation(User user);

        User Add(User user);
        User Update(User user);
        HashSet<string> DeleteValidation(int id);
        void Delete(int id);

    }
}
