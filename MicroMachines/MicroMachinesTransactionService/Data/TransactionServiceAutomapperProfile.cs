using AutoMapper;
using MicroMachinesCommon.Dtos;
using MicroMachinesTransactionService.Models;

namespace MicroMachinesTransactionService.Data;

public class TransactionServiceAutomapperProfile : Profile
{
    public TransactionServiceAutomapperProfile()
    {
        CreateMap<TransactionCreateDto, Transaction>();
        CreateMap<Transaction, TransactionReadDto>();
    }
}
