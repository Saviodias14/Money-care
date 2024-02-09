using System.Globalization;
using AutoMapper;
using Controle_Financeiro___Back.Data;
using Controle_Financeiro___Back.Data.Dtos.Services;

namespace Controle_Financeiro___Back.Controllers;

public class TypeController
{
    private readonly FinaceContext _context;
    private readonly IMapper _mapper;
    public TypeController(FinaceContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Models.Type> CreateType(CreateTypeRequest typeDto)
    {
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        typeDto.Name = textInfo.ToTitleCase(typeDto.Name.ToLower());
        var type = _mapper.Map<Models.Type>(typeDto);
        await _context.Type.AddAsync(type);
        await _context.SaveChangesAsync();
        return type;
    }
}
