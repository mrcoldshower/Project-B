namespace Library;

class CustomerLogic
{
    public static void CreateCustomer(List<Table> tables)
    {
        List<Customer> customerList = Data.Customers;
        int nextId;
        if (customerList.Count == 0) nextId = 1;
        else nextId = customerList[customerList.Count - 1].Id + 1;
        Customer customer = new Customer(nextId, tables);
        customerList.Add(customer);
        Data.CustomerAccess.WriteAll(customerList);
        Data.Customers = Data.CustomerAccess.LoadAll();
    }

    public static void RemoveCustomer(List<Table> tablesList)
    {
        List<Customer> customers = Data.Customers;
        for (int i = 0; i < customers.Count; i++)
        {
            for (int j = 0; j < tablesList.Count; j++)
            {
                for (int k = 0; k < customers[i].Tables.Count; k++)
                {
                    if (customers[i].Tables[k].Id == tablesList[j].Id)
                    {
                        customers[i].Tables.Remove(customers[i].Tables[k]);
                        if (!customers[i].Tables.Any()) customers.Remove(customers[i]);
                        goto LoopEnd;
                    }
                }
            }
        }
    LoopEnd:
        Data.CustomerAccess.WriteAll(customers);
    }

    public static int GetCustomerIdFromTableId(int tableId)
    {
        List<Customer> customers = Data.Customers;
        for (int i = 0; i < customers.Count; i++)
        {
            for (int j = 0; j < customers[i].Tables.Count; j++)
            {
                if (customers[i].Tables[j].Id == tableId) return customers[i].Id;
            }
        }
        return 0;
    }

    public static void DismissCustomer(int tableId)
    {
        int customerId = CustomerLogic.GetCustomerIdFromTableId(tableId);
        if (customerId == 0) throw new Exception("DismissCustomer, Table Id not found.");
        List<Customer> customerList = Data.Customers;
        Customer customer = customerList.FirstOrDefault(x => x.Id == customerId)!;
        if (customer == null) throw new Exception("DismissCustomer, customer not found.");
        List<int> tableIds = customer.Tables.Select(x => x.Id).ToList();
        List<Table> tables = Data.Tables;
        customerList.Remove(customer);
        foreach (int id in tableIds)
        {
            tables.First(x => x.Id == id).IsAvailable = true;
        }
        List<Order> orders = Data.Orders;
        foreach (int id in customer.OrderIds)
        {
            orders.Remove(Data.Orders.FirstOrDefault(x => x.Id == id)!);
        }
        Data.CustomerAccess.WriteAll(customerList);
        Data.Customers = customerList;
        Data.TableAccess.WriteAll(tables);
        Data.Tables = tables;
        Data.OrderAccess.WriteAll(orders);
        Data.Orders = orders;
    }
}
