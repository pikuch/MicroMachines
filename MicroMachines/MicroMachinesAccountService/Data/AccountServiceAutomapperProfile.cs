using AutoMapper;
using MicroMachinesAccountService.Models;
using MicroMachinesCommon.Dtos;

namespace MicroMachinesAccountService.Data;

public class AccountServiceAutomapperProfile : Profile
{
    public AccountServiceAutomapperProfile()
    {
        CreateMap<AccountCreateDto, Account>();
        CreateMap<Account, AccountReadDto>();
    }
}
