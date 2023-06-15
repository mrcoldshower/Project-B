namespace Library;

public static class TablesLogic
{
    public static void GenerateTables()
    {
        List<Table> tables = new List<Table>();
        int counter = 1;
        for (int i = 0; i < 8; i++)
        {
            Table table = new Table(counter, 2);
            tables.Add(table);
            counter++;
        }
        for (int i = 0; i < 5; i++)
        {
            Table table = new Table(counter, 4);
            tables.Add(table);
            counter++;
        }
        for (int i = 0; i < 2; i++)
        {
            Table table = new Table(counter, 6);
            tables.Add(table);
            counter++;
        }
        for (int i = 0; i < 8; i++)
        {
            Table table = new Table(counter, 1);
            tables.Add(table);
            counter++;
        }
        Data.TableAccess.WriteAll(tables);
    }

    public static List<Table> TableIdsToTablesList(List<int> TableIdsList)
    {
        List<Table> tables = Data.Tables;
        List<Table> result = new List<Table>();
        for (int i = 0; i < TableIdsList.Count; i++)
        {
            for (int j = 0; j < tables.Count; j++)
            {
                if (tables[j].Id == TableIdsList[i])
                {
                    result.Add(tables[j]);
                    tables[j].IsAvailable = false;
                    break;
                }
            }
        }
        Data.TableAccess.WriteAll(tables);
        Data.Tables = Data.TableAccess.LoadAll();
        return result;
    }

    public static void AddTableToRestaurant(Table table)
    {
        Restaurant restaurant = Data.Restaurant;
        switch (table.Type)
        {
            case 2: restaurant.TwoPersonTables++; break;
            case 4: restaurant.FourPersonTables++; break;
            case 6: restaurant.SixPersonTables++; break;
            case 1: restaurant.BarChairs++; break;
        }
        Data.RestaurantAccess.WriteItem(restaurant);
    }

    public static Table ClearTable(int tableId)
    {
        List<Table> tables = Data.Tables;
        Table result = null!;
        for (int i = 0; i < tables.Count; i++)
        {
            if (tables[i].Id == tableId)
            {
                tables[i].IsAvailable = true;
                result = tables[i];
                break;
            }
        }
        Data.TableAccess.WriteAll(tables);
        AddTableToRestaurant(result);
        return result;
    }

    public static bool IsTableClearable(int tableId)
    {
        List<Customer> customers = Data.Customers;
        for (int i = 0; i < customers.Count; i++)
        {
            for (int j = 0; j < customers[i].Tables.Count; j++)
            {
                if (customers[i].Tables[j].Id == tableId) return true;
            }
        }
        return false;
    }

    public static bool TypeIdCheck(int type, int id)
    {
        Table? table = Data.Tables.Find(x => x.Id == id);
        if (table == null) throw new Exception("TypeIdCheck went wrong");
        if (table.Type == type) return true;
        return false;
    }
}