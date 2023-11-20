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
            response.Role = new UserwithRole()
            {
                Role = user.Role.ToString(),
            };

            return response;
        }
        private async Task<string> CreateWallet()
        {
            var wallet = new Wallet()
            {
                Name = "Ví Tiền",
                Balance = 100000,
                BalanceHistory = 100000,
                CreationDate = DateTime.Now,
                IsDeleted = false
            };
            await _unitOfWork.walletRepo.CreateAsync(wallet);
            var isSuccess = await _unitOfWork.SaveChangeAsync();
            if (isSuccess > 0)
            {
                return "success";
            }
            return "fail";
        }
        public async Task<ResponseModel<string>> CreateUser(UserModel loginModel)
        {
            var isExist = await _unitOfWork.userRepo.ExistEmail(loginModel.Email);

            if (!isExist)
            {
                var user = _mapper.Map<User>(loginModel);
                string walletResult = await CreateWallet();
                user.Role = Role.Customer;
                if(walletResult.Equals( "success"))
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
        public async Task<User> GetUserById(int id)
        {
            var user = await _unitOfWork.userRepo.GetEntityByIdAsync(id);
            return user;
        }
        public async Task<List<User>> GetAllUser()
        {
            List<User> users = (await _unitOfWork.userRepo.GetAllAsync()).ToList();
            List<User> result = new List<User>();
            foreach (var item in users)
            {
                if (item.IsDeleted == false)
                {
                    result.Add(item);
                }
            }
            return result;
        } 
        public async Task<bool> DeleteUser(int id)
        {
            User user = await _unitOfWork.userRepo.GetEntityByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            user.DeletionDate = DateTime.Now;
            user.IsDeleted = true;
            _unitOfWork.userRepo.DeleteAsync(user);
            int check = await _unitOfWork.SaveChangeAsync();
            return check > 0 ? true : false;
        }
        public async Task<bool> UpdateUser(UserModel req, int id)
        {
            User user = await _unitOfWork.userRepo.GetEntityByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            user.Name = req.Name;
            user.Address = req.Address;
            user.Phone = req.Phone;
            user.Email = req.Email;
            user.Password = req.Password;
            user.ModificationDate = DateTime.Now;
            _unitOfWork.userRepo.UpdateAsync(user);
            int check = await _unitOfWork.SaveChangeAsync();
            return check > 0 ? true : false;
        }
    }
}