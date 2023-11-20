﻿using Services.Commons;
using Services.Entity;
using Services.Model;

namespace Services.Service.Interface
{
    public interface IUserServices
    {
        Task<ResponseModel<string>> Login(LoginModel loginModel);
        Task<ResponseModel<string>> CreateUser(UserModel loginModel);
        //Task<ResponseModel<Pagination<UserModel>>> GetUsers(int pageIndex = 0, int pageSize = 10);
        //Task<ResponseModel<string>> UpdateUser(int id, UserModel loginModel);
        //Task<ResponseModel<string>> RemoveUser(int id);
        Task<User> GetUserById(int id);
        Task<List<User>> GetAllUser();
        Task<bool> DeleteUser(int id);
        Task<bool> UpdateUser(UserModel req, int id);
        Task<bool> CheckCourseUser(int userId, int courseId);

    }
}
