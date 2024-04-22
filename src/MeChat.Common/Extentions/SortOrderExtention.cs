using MeChat.Common.Enumerations;

namespace MeChat.Common.Extentions;
public class SortOrderExtention
{
    /// <summary>
    /// Conver a string to Dictionary of list column with sort order
    /// </summary>
    public static Dictionary<string, SortOrderSql> Convert(string? sortColumnWithOrders)
    {
        var result = new Dictionary<string, SortOrderSql>();

        if (string.IsNullOrEmpty(sortColumnWithOrders))
            return result;

        var columnWithOrders = sortColumnWithOrders.Split(',');

        if(columnWithOrders.Length == 0 )
            return result;

        if(columnWithOrders.Length == 1)
        {
            if(!sortColumnWithOrders.Contains('-'))
                throw new FormatException("Sort condition should be follow by format: Column1-ASC, Column2-DESC,..");

            var property = sortColumnWithOrders.Trim().Split('-');
            var colum = property[0];
            var order = ConvertStringToSortOder(property[1]);
            result.Add(colum, order);
            return result;
        }

        foreach (var item in columnWithOrders)
        {
            if (!item.Contains('-'))
                throw new FormatException("Sort condition should be follow by format: Column1-ASC, Column2-DESC,..");

            var property = item.Trim().Split('-');
            var colum = property[0];
            var order = ConvertStringToSortOder(property[1]);
            result.Add(colum, order);
        }

        return result;
    }

    private static SortOrderSql ConvertStringToSortOder(string order)
    {
        if (string.IsNullOrEmpty(order))
            return SortOrderSql.Descending;

        if(order.ToUpper().Equals("ASC"))
            return SortOrderSql.Ascending;

        return SortOrderSql.Descending;
    }
}
