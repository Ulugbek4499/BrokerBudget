using AutoMapper;
using BrokerBudget.Application.UseCases.Payments;
using BrokerBudget.Application.UseCases.Payments.Commands.CreatePayment;
using BrokerBudget.Application.UseCases.Payments.Commands.DeletePayment;
using BrokerBudget.Application.UseCases.Payments.Commands.UpdatePayment;
using BrokerBudget.Domain.Entities;

namespace BrokerBudget.Application.Common.Mappings
{
    public class PaymentMapping : Profile
    {
        public PaymentMapping()
        {
            CreateMap<CreatePaymentCommand, Payment>().ReverseMap();
            CreateMap<DeletePaymentCommand, Payment>().ReverseMap();
            CreateMap<UpdatePaymentCommand, Payment>().ReverseMap();
            CreateMap<PaymentResponse, Payment>().ReverseMap();
        }
    }
}
