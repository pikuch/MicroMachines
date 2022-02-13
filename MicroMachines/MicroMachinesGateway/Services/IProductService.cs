using MicroMachinesCommon.Dtos;

namespace MicroMachinesGateway.Services;

public interface IProductService
{
    public Task<IEnumerable<ProductReadDto>?> GetAllAsync();
    public Task<IEnumerable<ProductReadDto>?> GetAllFromCategoryAsync(int categoryId);
}
