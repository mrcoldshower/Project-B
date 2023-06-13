namespace Library;

public static class FoodLogic
{
    public static void AddDish(string name, double price, string description, FoodCategory category)
    {
        List<Food> foodList = Data.Foods;
        int nextId;
        if (foodList.Count == 0) nextId = 1;
        else nextId = foodList[foodList.Count - 1].Id + 1; // can be made generic, but we dont have a lot of time left
        foodList.Add(new Food(nextId, name, price, description, category));
        Data.FoodAccess.WriteAll(foodList);
        Data.Foods = foodList;
    }

    public static void ChangeDish(int id, string name, double price, string description, FoodCategory category)
    {
        List<Food> foodList = Data.Foods;
        Food dish = foodList.Find(x => x.Id == id)!;
        if (dish == null) throw new Exception("ChangeDish: dish not found using provided id");
        dish.Name = name;
        dish.Price = price;
        dish.Description = description;
        dish.Category = category;
        foodList.Where(x => x == dish).Select(x => dish);
        Data.FoodAccess.WriteAll(foodList);
        Data.Foods = foodList;
    }

    public static void RemoveDish(int id)
    {
        List<Food> foodList = Data.Foods;

        Food dish = foodList.Find(o => o.Id == id)!;
        if (dish != null)
        {
            try
            {
                foodList.Remove(dish);
            }
            catch
            {
                throw new Exception("RemoveDish: couldn't remove the dish from the foodList");
            }
        }

        for (int i = 0; i < foodList.Count; i++)
        {
            int j = i + 1;
            foodList[i].Id = j;
        }

        Data.FoodAccess.WriteAll(foodList);
        Data.Foods = foodList;
    }
}