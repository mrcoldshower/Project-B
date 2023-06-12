namespace Library;

public static class OrderLogic
{
    private static int GetNextId()
    {
        List<Order> orderList = Data.Orders;
        int nextId;
        if (orderList.Count == 0) nextId = 1;
        else nextId = orderList[orderList.Count - 1].Id + 1;
        return nextId;
    }

    public static void PlaceOrder(List<Food> foodList, int tableId)
    {
        Order order = new Order(GetNextId(), foodList);
        ConnectOrderToCustomer(order.Id, tableId);
        Data.Orders.Add(order);
        Data.OrderAccess.WriteAll(Data.Orders);
    }

    public static void ConnectOrderToCustomer(int orderId, int tableId)
    {
        int customerId = CustomerLogic.GetCustomerIdFromTableId(tableId);
        if (customerId == 0) throw new Exception("ConnectOrderToCustomer, Table Id not found.");
        List<Customer> customerList = Data.Customers;
        Customer customer = customerList.FirstOrDefault(x => x.Id == customerId)!;
        if (customer == null) throw new Exception("ConnectOrderToCustomer, customer not found.");
        customer.OrderIds.Add(orderId);
        Data.CustomerAccess.WriteAll(customerList);
        Data.Customers = customerList;
    }

    public static double GetTotalPrice(int tableId)
    {
        int customerId = CustomerLogic.GetCustomerIdFromTableId(tableId);
        if (customerId == 0) throw new Exception("GetTotalPrice, Table Id not found.");
        List<Customer> customerList = Data.Customers;
        Customer customer = customerList.FirstOrDefault(x => x.Id == customerId)!;
        if (customer == null) throw new Exception("GetTotalPrice, customer not found.");
        double totalPrice = 0;
        foreach (int id in customer.OrderIds)
        {
            totalPrice += Data.Orders.FirstOrDefault(x => x.Id == id)!.TotalPrice;
        }
        return totalPrice;
    }


}