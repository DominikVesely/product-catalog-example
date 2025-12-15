using Business.Dto;
using Data.Entities;
using Riok.Mapperly.Abstractions;

namespace Business.Mappers;

[Mapper]
public partial class ProductMapper
{
    public partial ProductDto MapProductToDto(Product car);
}
