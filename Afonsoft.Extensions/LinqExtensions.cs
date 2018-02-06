using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Classe com extenção para LINQ 
/// AFONSO DUTRA NOGUEIRA FILHO
/// </summary>
public static class LinqExtensions
{

    /// <summary>
    /// Used by LINQ to SQL - Paginate Source
    /// </summary>
    public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int pageSize)
    {
        return source.Skip((page - 1) * pageSize).Take(pageSize);
    }
    /// <summary>
    /// Used by LINQ - Paginate Source
    /// </summary>
    public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
    {
        return source.Skip((page - 1) * pageSize).Take(pageSize);
    }   
}