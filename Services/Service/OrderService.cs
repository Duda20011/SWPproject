﻿using AutoMapper;
using Services.Entity;
using Services.Model;
using Services.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Order> CreateOrder(OrderModel req)
        {
            //checkUser
            User user = await _unitOfWork.userRepo.GetEntityByIdAsync(req.UserId);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            //checkCourse
            Course course = await _unitOfWork.courseRepo.GetEntityByIdAsync(req.CourseId);
            if(course == null)
            {
                throw new Exception("Course not found");
            }
            var checkPayment = await CreatePaymentOrder(req);
            if (checkPayment == null)
            {
                throw new Exception("Not Enough money");
            }
            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                TotalPrice = req.TotalPrice,
                UserId = req.UserId,
                OrderStatus = Enum.OrderStatus.Success,
                PaymentId = checkPayment.Id,
                CreationDate = DateTime.Now,
                IsDeleted = false
            };
            await PayWallet(req);
            //add UserCourse
            var checkUserCourse = await AddUserCourse(req);
            if (checkUserCourse)
            {
                await _unitOfWork.orderRepo.CreateAsync(order);
                await _unitOfWork.SaveChangeAsync();
                return order;
            }
            return null;
        }
        private async Task<Payment> CreatePaymentOrder(OrderModel req)
        {
            //checkUser
            User user = await _unitOfWork.userRepo.GetEntityByIdAsync(req.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            //checkCourse
            Course course = await _unitOfWork.courseRepo.GetEntityByIdAsync(req.CourseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }
            //checkWallet
            Wallet wallet = await _unitOfWork.walletRepo.GetEntityByIdAsync(user.WalletId);
            if (wallet == null)
            {
                throw new Exception("Wallet not found");
            }
            else
            {
                if(wallet.Balance < req.TotalPrice)
                {
                    return null;
                }
            }
            //create Payment
            Payment newPaymentOrder = new Payment()
            {
                Name = $"Thanh toán Course {course.title}",
                Balance = req.TotalPrice,
                CourseId = req.CourseId,
                Status = Enum.PaymentStatus.Success,
                CreationDate = DateTime.Now,
                IsDeleted = false
            };
            await _unitOfWork.paymentRepo.CreateAsync(newPaymentOrder);
            await _unitOfWork.SaveChangeAsync();
            return newPaymentOrder;
        }
        private async Task<bool> AddUserCourse(OrderModel req)
        {
            User user = await _unitOfWork.userRepo.GetEntityByIdAsync(req.UserId);
            if (user == null)
            {
                return false;
            }
            //checkCourse
            Course course = await _unitOfWork.courseRepo.GetEntityByIdAsync(req.CourseId);
            if (course == null)
            {
                return false;
            }
            UserCourse userCourse = new UserCourse()
            {
                UserId = req.UserId,
                CourseId = req.CourseId,
                CreationDate = DateTime.Now,
                IsDeleted = false
            };
            await _unitOfWork.userCourseRepo.CreateAsync(userCourse);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        private async Task<bool> PayWallet(OrderModel req)
        {
            User user = await _unitOfWork.userRepo.GetEntityByIdAsync(req.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            Wallet wallet = await _unitOfWork.walletRepo.GetEntityByIdAsync(user.WalletId);
            wallet.Balance = (int)(wallet.Balance - req.TotalPrice);
            _unitOfWork.walletRepo.UpdateAsync(wallet);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<List<Order>> GetAllOrder()
        {
            List<Order> listOrder = (await _unitOfWork.orderRepo.GetAllAsync()).ToList();
            return listOrder;
        }
        public async Task<Order> GetOrderById(int id)
        {
            Order order = await _unitOfWork.orderRepo.GetEntityByIdAsync(id);
            return order;
        }
        public async Task<Order> UpdateOrder(OrderModel req, int id)
        {
            Order order = await _unitOfWork.orderRepo.GetEntityByIdAsync(id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.TotalPrice = req.TotalPrice;
            order.UserId = req.UserId;
            order.OrderStatus = Enum.OrderStatus.Success;
            order.CreationDate = DateTime.Now;
            order.IsDeleted = false;
            _unitOfWork.orderRepo.UpdateAsync(order);
            await _unitOfWork.SaveChangeAsync();
            return order;
        }

    }
}