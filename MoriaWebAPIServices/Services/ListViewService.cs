
using Microsoft.EntityFrameworkCore;
using MoriaModels.Attributes;
using System.Linq.Expressions;
using System.Reflection;
using MoriaWebAPIServices.Services.Interfaces;
using MoriaWebAPIServices.Contexts;

namespace MoriaWebAPIServices.Services;

public class ListViewService : IListViewControllerService
{
    readonly ApplicationDbContext _context;

    public ListViewService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TDo>> SearchAsync<TDo>(string searchText)
        where TDo : class
    {
        Type entityType = ResolveEntityType(typeof(TDo));
        var method = typeof(ListViewService).GetMethod(nameof(SearchDynamicAsync))
            ?.MakeGenericMethod(entityType, typeof(TDo));

        if (method == null)
            throw new InvalidOperationException("Search method resolution failed");

        var task = (Task<List<TDo>>)method.Invoke(this, new object[] { searchText });

        var result = await task;
        return result.AsEnumerable();

    }


    public async Task<List<TDo>> SearchDynamicAsync<T, TDo>(string searchText)
       where T : class
       where TDo : class, new()
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var properties = GetSearchableProperties(typeof(T));

        Expression? combinedExpression = null;
        foreach (var property in properties)
        {
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
            Expression propertyExpression = Expression.Property(parameter, property.Item1);

            if (property.Item2 != null)
            {
                propertyExpression = Expression.Property(propertyExpression, property.Item2);
            }

            var searchExpression = Expression.Call(propertyExpression, containsMethod, Expression.Constant(searchText));
            combinedExpression = combinedExpression == null ? searchExpression : Expression.OrElse(combinedExpression, searchExpression);
        }

        if (combinedExpression == null) return new List<TDo>();

        var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
        IQueryable<T> query = _context.Set<T>().Where(lambda);

        foreach (var property in GetNavigationalProperties(typeof(T)))
        {
            query = query.Include(property.Name);
        }

        var entities = await query.ToListAsync();
        return entities.Select(entity => MapToDo<T, TDo>(entity)).ToList();
    }

    private List<(PropertyInfo, PropertyInfo?)> GetSearchableProperties(Type type)
    {
        var result = new List<(PropertyInfo, PropertyInfo?)>();

        foreach (var property in type.GetProperties())
        {
            if (property.GetCustomAttribute<SearchableAttribute>() != null && property.PropertyType == typeof(string))
            {
                result.Add((property, null));
            }
            else if (!property.PropertyType.IsPrimitive && !property.PropertyType.IsEnum && property.PropertyType != typeof(string))
            {
                foreach (var nestedProperty in property.PropertyType.GetProperties())
                {
                    if (nestedProperty.GetCustomAttribute<SearchableAttribute>() != null && nestedProperty.PropertyType == typeof(string))
                    {
                        result.Add((property, nestedProperty));
                    }
                }
            }
        }
        return result;
    }

    private List<PropertyInfo> GetNavigationalProperties(Type type)
    {
        return type.GetProperties()
            .Where(p => !p.PropertyType.IsPrimitive && !p.PropertyType.IsEnum && p.PropertyType != typeof(string))
            .ToList();
    }

    private TDo MapToDo<T, TDo>(T entity) where TDo : class, new()
    {
        if (entity == null) return null;
        TDo dto = new TDo();
        var entityProperties = typeof(T).GetProperties();
        var dtoProperties = typeof(TDo).GetProperties();

        foreach (var dtoProp in dtoProperties)
        {
            var entityProp = entityProperties.FirstOrDefault(p => p.Name == dtoProp.Name);

            if (entityProp != null)
            {
                var value = entityProp.GetValue(entity);

                if (value != null && entityProp.PropertyType.IsClass && entityProp.PropertyType != typeof(string))
                {
                    var nestedDto = Activator.CreateInstance(dtoProp.PropertyType);
                    var mappedNestedDto = MapToDo(value, nestedDto);
                    dtoProp.SetValue(dto, mappedNestedDto);
                }
                else
                {
                    dtoProp.SetValue(dto, value);
                }
            }
        }
        return dto;
    }

    private object MapToDo(object entity, object dto)
    {
        if (entity == null) return null;
        var entityProperties = entity.GetType().GetProperties();
        var dtoProperties = dto.GetType().GetProperties();

        foreach (var dtoProp in dtoProperties)
        {
            var entityProp = entityProperties.FirstOrDefault(p => p.Name == dtoProp.Name);

            if (entityProp != null)
            {
                var value = entityProp.GetValue(entity);

                if (value != null && entityProp.PropertyType.IsClass && entityProp.PropertyType != typeof(string))
                {
                    var nestedDto = Activator.CreateInstance(dtoProp.PropertyType);
                    var mappedNestedDto = MapToDo(value, nestedDto);
                    dtoProp.SetValue(dto, mappedNestedDto);
                }
                else
                {
                    dtoProp.SetValue(dto, value);
                }
            }
        }
        return dto;
    }

    private Type ResolveEntityType(Type dtoType)
    {
        string entityTypeName = dtoType.Name.Replace("Do", "");
        var entityType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => t.Name == entityTypeName);

        if (entityType == null)
            throw new InvalidOperationException("Zły typ " + entityTypeName);

        return entityType;
    }
}
