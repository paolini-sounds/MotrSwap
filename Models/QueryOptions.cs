using System.Linq.Expressions;

namespace MotrSwap.Models;

public class QueryOptions<T> where T : class
{
    public Expression<Func<T, bool>>? Where { get; set; }
    public Expression<Func<T, Object>> OrderBy { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    private string[] includes = Array.Empty<string>();

    public string Includes
    {
        set => includes = value.Replace(" ", "").Split(',');
    }

    public string[] GetIncludes() => includes;

   

    public int Skip => (PageNumber - 1) * PageSize;

    public bool HasWhere => Where != null;
    public bool HasOrderBy => OrderBy != null;
}