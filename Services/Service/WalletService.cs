﻿using AutoMapper;
using Services.Entity;
using Services.Enum;
using Services.Model;
using Services.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WalletService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Wallet> CreateWallets(WalletModel req)
        {
            Wallet wallet = new Wallet()
            {
                Name = req.name,
                CreationDate = DateTime.Now,
                IsDeleted = false,
                Balance = req.balance,
                BalanceHistory = req.balanceHistory
            };
            await _unitOfWork.walletRepo.CreateAsync(wallet);
            await _unitOfWork.SaveChangeAsync();
            return wallet;
        }
        public async Task<bool> UpdateWallets(WalletModel req, int id)
        {
            Wallet wallet = await _unitOfWork.walletRepo.GetEntityByIdAsync(id);
            if (wallet == null)
            {
                return false;
            }
            wallet.Name = req.name;
            wallet.ModificationDate = DateTime.Now;
            wallet.Balance = req.balance;
            wallet.BalanceHistory = req.balanceHistory;
            _unitOfWork.walletRepo.UpdateAsync(wallet);
            int check = await _unitOfWork.SaveChangeAsync();
            return check > 0 ? true : false;
        }
        public async Task<bool> DeleteWallets(int id)
        {
            Wallet wallet = await _unitOfWork.walletRepo.GetEntityByIdAsync(id);
            if (wallet == null)
            {
                return false;
            }
            wallet.DeletionDate = DateTime.Now;
            wallet.IsDeleted = true;
            _unitOfWork.walletRepo.DeleteAsync(wallet);
            int check = await _unitOfWork.SaveChangeAsync();
            return check > 0 ? true : false;
        }
        public async Task<Wallet> GetWalletById(int id)
        {
            Wallet wallet = await _unitOfWork.walletRepo.GetEntityByIdAsync(id);
            return wallet;
        }
        public async Task<List<Wallet>> GetWalletsAsync()
        {
            List<Wallet> wallets = (await _unitOfWork.walletRepo.GetAllAsync()).ToList();
            List<Wallet> result = new List<Wallet>();
            foreach (var item in wallets)
            {
                if (item.IsDeleted == false)
                {
                    result.Add(item);
                }
            }
            return wallets;
        }
    }
}