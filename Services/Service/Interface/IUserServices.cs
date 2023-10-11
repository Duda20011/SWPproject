using Services.Commons;
using Services.Model;

namespace Services.Service.Interface
{
    public interface IUserServices
    {
        Task<ResponseModel<string>> Login(LoginModel loginModel);
        Task<ResponseModel<string>> CreateUser(UserModel loginModel);

    }
}
