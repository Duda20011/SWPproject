using Services.Entity;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.Interface
{
    public interface IWalletService
    {
        Task<Wallet> CreateWallets(WalletModel req);
        Task<bool> UpdateWallets(WalletModel req, int id);
        Task<bool> DeleteWallets(int id);
        Task<Wallet> GetWalletById(int id);
        Task<List<Wallet>> GetWalletsAsync();

    }
}
