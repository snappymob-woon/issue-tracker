using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public abstract class BaseService<T>
{
    protected readonly IMapper _mapper;
    protected readonly AppDbContext _context;
    protected readonly ILogger _logger;

    public BaseService(AppDbContext context, ILoggerFactory factory, IMapper mapper)
    {
        _context = context;
        _logger = factory.CreateLogger<T>();
        _mapper = mapper;
    }
}