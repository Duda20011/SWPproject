using AutoMapper;
using Services.Entity;
using Services.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<Payment>> GetAllPayment()
        {
            List<Payment> paymentList = (await _unitOfWork.paymentRepo.GetAllAsync()).ToList();
            return paymentList;
        }
        public async Task<Payment> GetPaymentById(int id)
        {
            Payment payment = await _unitOfWork.paymentRepo.GetEntityByIdAsync(id);
            return payment;
        }
    }
}
