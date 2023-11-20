using AutoMapper;
using Services.Commons;
using Services.Entity;
using Services.Enum;
using Services.Model;
using Services.Service.Interface;

namespace Services.Service
{
    public class UserService : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseModel<string>> Login(LoginModel loginModel)
        {
            var response = new ResponseModel<string>();
            var user = await _unitOfWork.userRepo.Login(loginModel.Email, loginModel.Password);
            if (user == null || user.IsDeleted)
            {
                response.Errors = "User not exsits or has been banned.";
                return response;
            }
            response.Data = "Login successful.";

            return response;
        }
        public async Task<ResponseModel<string>> CreateUser(UserModel loginModel)
        {
            var isExist = await _unitOfWork.userRepo.ExistEmail(loginModel.Email);

            if (!isExist)
            {
                var user = _mapper.Map<User>(loginModel);
                user.Role = Role.Customer;
                user.WalletId = await _unitOfWork.userRepo.AutoIncreamentId();
                await _unitOfWork.userRepo.CreateAsync(user);
                var isSuccess = await _unitOfWork.SaveChangeAsync();
                if (isSuccess > 0)
                {
                    return new ResponseModel<string> { Data = "success" };
                }
            }
            return new ResponseModel<string> { Errors = "Fail to create user." };
        }
    }
}