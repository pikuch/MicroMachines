using MicroMachinesCommon.Dtos;

namespace MicroMachinesGateway.Services;

public interface IProductService
{
    public Task<IEnumerable<ProductReadDto>?> GetAllAsync();
}
