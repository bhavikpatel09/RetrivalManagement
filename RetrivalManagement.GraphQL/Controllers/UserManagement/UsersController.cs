using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using RetrivalManagement.Domain.UserManagement;
using RetrivalManagement.Models.DbEntities.Main;
using RetrivalManagement.Models.Models;
using RetrivalManagement.UnitOfWork.Login;
using RetrivalManagement.UnitOfWork.UserManagement;
using RxWeb.Core.AspNetCore;
using System.Net;

namespace RetrivalManagement.GraphQL.Controllers.UserManagement
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        //private APIResponse APIResponse { get; set; }

        private IUserUow UserUow { get; set; }
        private IUserDomain UserDomain { get; set; }
        public UsersController(IUserUow userUow, IUserDomain userDomain)
        {
            UserUow = userUow;
            UserDomain = userDomain;
            //APIResponse = new APIResponse();
        }

        public IActionResult GetAll()
        {
            var users = UserDomain.GetAll();
            //APIResponse = new APIResponse
            //{
            //    Response = users,
            //    StatusCode = System.Net.HttpStatusCode.OK,
            //    Status = true
            //};
            //APIResponse.Message = "Users list";
            return Ok(users);
        }

        //[HttpGet("{orderbycolumn}/{sortorder}/{pageindex}/{rowcount}")]
        //public IActionResult Get(string orderByColumn, string sortOrder, int pageIndex, int rowCount, string searchQuery, int userId, int roleId)
        //{
        //    string users = UserDomain.Get(orderByColumn, sortOrder, pageIndex, rowCount, searchQuery, userId, roleId);
        //    //APIResponse = new APIResponse
        //    //{
        //    //    Response = users,
        //    //    StatusCode = System.Net.HttpStatusCode.OK,
        //    //    Status = true
        //    //};
        //    //APIResponse.Message = "Users list";
        //    return Ok(users);
        //}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            User user = UserDomain.Get(id);
            //APIResponse = new APIResponse
            //{
            //    Response = user,
            //    Status = true,
            //    StatusCode = System.Net.HttpStatusCode.OK,
            //    Message = "User record Detail"
            //};
            return Ok(user);
        }


        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            user.IPAddress = GetIPAddress();
            //APIResponse = new APIResponse();
            HashSet<string> validations = UserDomain.AddValidation(user);
            if (validations.Count() == 0)
            {
                User result = UserDomain.Add(user);
                //APIResponse = new APIResponse
                //{
                //    Response = result,
                //    Status = true,
                //    StatusCode = System.Net.HttpStatusCode.OK,
                //    Message = "User record added successfully."
                //};
                return Ok(result);
            }
            //APIResponse.Message = validations.FirstOrDefault();
            //APIResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            //APIResponse.Status = false;
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            //APIResponse = new APIResponse();
            HashSet<string> validations = UserDomain.UpdateValidation(user);
            if (validations.Count() == 0)
            {
                string message = string.Empty;
                User result = UserDomain.Update(user);
                if (user.IsChangePassword)
                {
                    message = "User password has been changed successfully.";
                }
                else
                {
                    message = "User record updated successfully.";
                }
                //APIResponse = new APIResponse
                //{
                //    Response = result,
                //    StatusCode = System.Net.HttpStatusCode.OK,
                //    Status = true,
                //    Message = message
                //};
                return Ok(result);
            }
       
            return BadRequest();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] ChangePasswordViewModel changePasswordViewModel)
        {
            User updateDetails = UserUow.Repository<User>().FindByKey(id);
            if (updateDetails != null)
            {
                updateDetails.EmailAddress = changePasswordViewModel.EmailAddress;
                updateDetails.OldPassword = changePasswordViewModel.OldPassword;
                updateDetails.UserPassword = changePasswordViewModel.UserPassword;
                updateDetails.ConfirmPassword = changePasswordViewModel.ConfirmPassword;
                updateDetails.UserId = changePasswordViewModel.UserId;
                updateDetails.IsChangePassword = changePasswordViewModel.IsChangePassword;
                return Put(changePasswordViewModel.UserId, updateDetails);
            }
            else
            {
                return BadRequest();
            }
            //User user = Uow.User.Repository<User>().FindByKey(id);
            //patchUser.ApplyTo(user);
            //return Put(id, user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //APIResponse = new APIResponse();
            HashSet<string> validations = UserDomain.DeleteValidation(id);
            if (validations.Count() == 0)
            {
                UserDomain.Delete(id);
                //APIResponse = new APIResponse
                //{
                //    Status = true,
                //    StatusCode = System.Net.HttpStatusCode.OK,
                //    Message = "User record deleted successfully."
                //};
                return Ok();
            }
            
            return BadRequest();
        }

        private string GetIPAddress()
        {
            string IP4Address = String.Empty;
            IP4Address = HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
            if (IP4Address != String.Empty)
            {
                return IP4Address;
            }
            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }
            return IP4Address;
        }
    }
}
