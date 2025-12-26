using System.Data;
using System.Reflection;

namespace BulkInsertServiceExample.Extensions;
public static class DataTableExtensions
{
    public static DataTable ToDataTable<T>(this List<T> iList)
    {
        DataTable dataTable = new DataTable();
    
        PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo prop in properties)
        {
            Type type = prop.PropertyType;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                type = Nullable.GetUnderlyingType(type)!;

            dataTable.Columns.Add(prop.Name, type!);
        }

        foreach (T item in iList)
        {
            object[] values = new object[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                values[i] = properties[i].GetValue(item) ?? DBNull.Value;
            }
            dataTable.Rows.Add(values);
        }

        return dataTable;
    }
}



