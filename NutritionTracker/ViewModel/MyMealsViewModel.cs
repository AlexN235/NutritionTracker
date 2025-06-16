using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CsvHelper;
using NutruitionTracker.NutritionFacts;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;



namespace NutruitionTracker.ViewModel;

public partial class MyMealsViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private ObservableCollection<FoodDisplayGroup> mealList;
    [ObservableProperty]
    private FoodDisplay selectedItem;
    private IFileSaver fileSaver;
    

    public MyMealsViewModel(IFileSaver FileSaver) 
    {
        MealList = new ObservableCollection<FoodDisplayGroup>();
        this.fileSaver = FileSaver;
        ReadCSVMealData();
    }

    [RelayCommand]
    async Task GoBack() 
    {
        WriteCSVMealData(MealList.ToList());
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task GoToAddMeal()
    {
        await Shell.Current.GoToAsync(nameof(InputMealPage));
        Task.Delay(100);
    }

    [RelayCommand]
    async Task OnSelectionGoToDetails() 
    {
        Dictionary<string, object> dict = new Dictionary<string, object>
        {
            ["Food"] = SelectedItem
        };
        await Shell.Current.GoToAsync(nameof(DisplayDetailPage), dict);
    }

    private void DeleteMeal(FoodDisplay f) 
    {
        foreach (FoodDisplayGroup group in MealList.ToList()) 
        {
            for (int i = 0; i < group.Count; i++)
            {
                if (group[i].Equals(f)) {
                    group.RemoveAt(i);
                    UpdateList(group.Name);
                }
            }
        }
    }

    private void AddMeal(FoodDisplay newMeal) 
    {
        DateTime date = newMeal.GetDate();
        var group = MealList.Where(x => x.Name == date.ToString("d"));
        if (group.Any())
        {
            MealList.First(x => x.Name == date.ToString("d")).AddToList(newMeal);
        }
        else 
        {
            List<FoodDisplay> temp = [newMeal];
            MealList.Add(new FoodDisplayGroup(date.ToString("d"), temp));
        }
        UpdateList(date.ToString("d"));
    }

    private void UpdateList(string date)
    { 
        // Update observable collection of meals to dynamically display when adding or deleting meals.
        List<FoodDisplay> food_list = new List<FoodDisplay>();
        foreach (FoodDisplayGroup foodDisplays in MealList.ToList())
        {
            if (foodDisplays.Name == date) 
            {
                foreach (FoodDisplay foodDisplay in foodDisplays)
                {
                    food_list.Add(foodDisplay);
                }
            }
        }
        foreach (FoodDisplayGroup foodDisplays in MealList.ToList())
        {
            if (foodDisplays.Name == date)
            {
                MealList.Remove(foodDisplays);
                MealList.Add(new FoodDisplayGroup(date, food_list));
            }
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.Count == 0)
        {
            return;
        }
        else if (query.ContainsKey("toDelete"))
        {
            FoodDisplay q = query["toDelete"] as FoodDisplay;
            if (q == null)
                return;
            query.Clear();
            this.DeleteMeal(q);
        }
        else if (query.ContainsKey("Meal") != null)
        {
            FoodDisplay q = query["Meal"] as FoodDisplay;
            if (q == null)
                return;
            query.Clear();
            this.AddMeal(q);
        }
        else
            return; 
    }

    public void ReadCSVMealData()
    {
        string fileName = "MealInfo.csv";
        string path = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);

        if (!File.Exists(path)) 
        {
            File.Create(path);
            Debug.WriteLine("BACK IN IT");
        }

        using StreamReader reader = new StreamReader(path);
        using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
        {
            var records = csv.GetRecords<Meal>();
            MealList = new ObservableCollection<FoodDisplayGroup>(MealToFood(records));
        }
    }

    public void WriteCSVMealData(List<FoodDisplayGroup> foods)
    {
        string fileName = "MealInfo.csv";
        string path = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);

        Debug.WriteLine(FileSystem.Current.AppDataDirectory);
        if(!File.Exists(path)) 
        {
            //await SaveFile(FileSystem.Current.AppDataDirectory, new CancellationToken());
        }

        using StreamWriter writer = new StreamWriter(path);
        using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
        {
            List<Meal> meals = new List<Meal>();
            foreach (FoodDisplayGroup group in foods)
            {
                foreach (FoodDisplay food in group)
                {
                    MealItem item = (MealItem)food.Item;
                    Meal meal = new Meal(group.Name, food.Name, food.Value, item.Name, item.Date, item.itemsValue);
                    meals.Add(meal);
                }
            }
            csv.WriteRecords(meals);
        }
    }

    private List<FoodDisplayGroup> MealToFood(IEnumerable<Meal> meals)
    {
        List<FoodDisplayGroup> foodDisplays = new List<FoodDisplayGroup>();
        Dictionary<string, List<FoodDisplay>> map = new Dictionary<string, List<FoodDisplay>>();
        List<string> dateRecords = new List<string>();
        foreach (Meal meal in meals)
        {
            if (!dateRecords.Contains(meal.Date))
            {
                dateRecords.Add(meal.Date);
                List<FoodDisplay> newGroup = new List<FoodDisplay>();
                map.Add(meal.Date, newGroup);
            }
            MealItem m = new MealItem(meal.getMealValues(), meal.Name, meal.FullDate);
            FoodDisplay foodDisplay = new FoodDisplay(meal.DisplayName, meal.Value, m);
            map[meal.Date].Add(foodDisplay);
        }

        foreach (string group in dateRecords)
        {
            foodDisplays.Add(new FoodDisplayGroup(group, map[group]));
        }
        return foodDisplays;
    }

    public class Meal
    {

        public Meal() { }
        public Meal(string date, string display, float val, string name, DateTime fullDate, float[] stats)
        {
            Date = date;
            DisplayName = display;
            Value = val;
            Name = name;
            FullDate = fullDate;
            fillProperties(stats);
        }

        public string Date { get; init; }
        public string DisplayName { get; init; }
        public float Value { get; init; }
        public string Name { get; init; }
        public DateTime FullDate { get; init; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Carbohydrate { get; set; }
        public float Alcohol { get; set; }
        public float Calories { get; set; }
        public float Energy { get; set; }
        public float Fibre { get; set; }
        public float Sugars { get; set; }
        public float Calcium { get; set; }
        public float Iron { get; set; }
        public float Magnesium { get; set; }
        public float Phosphorus { get; set; }
        public float Potassium { get; set; }
        public float Sodium { get; set; }
        public float Zinc { get; set; }
        public float Copper { get; set; }
        public float Manganese { get; set; }
        public float VitB6 { get; set; }
        public float VitB12 { get; set; }
        public float VitC { get; set; }
        public float VitD { get; set; }
        public float Cholesterol { get; set; }
        public float Caffeine { get; set; }

        private void fillProperties(float[] stats)
        {
            Protein = stats[0];
            Fat = stats[1];
            Carbohydrate = stats[2];
            Alcohol = stats[3];
            Calories = stats[4];
            Energy = stats[5];
            Fibre = stats[6];
            Sugars = stats[7];
            Calcium = stats[8];
            Iron = stats[9];
            Magnesium = stats[10];
            Phosphorus = stats[11];
            Potassium = stats[12];
            Sodium = stats[13];
            Zinc = stats[14];
            Copper = stats[15];
            Manganese = stats[16];
            VitB6 = stats[17];
            VitB12 = stats[18];
            VitC = stats[19];
            VitD = stats[20];
            Cholesterol = stats[21];
            Caffeine = stats[22];
        }
        public float[] getMealValues()
        {
            float[] values = new float[23];
            values[0] = this.Protein;
            values[1] = this.Fat;
            values[2] = this.Carbohydrate;
            values[3] = this.Alcohol;
            values[4] = this.Calories;
            values[5] = this.Energy;
            values[6] = this.Fibre;
            values[7] = this.Sugars;
            values[8] = this.Calcium;
            values[9] = this.Iron;
            values[10] = this.Magnesium;
            values[11] = this.Phosphorus;
            values[12] = this.Potassium;
            values[13] = this.Sodium;
            values[14] = this.Zinc;
            values[15] = this.Copper;
            values[16] = this.Manganese;
            values[17] = this.VitB6;
            values[18] = this.VitB12;
            values[19] = this.VitC;
            values[20] = this.VitD;
            values[21] = this.Cholesterol;
            values[22] = this.Caffeine;
            return values;
        }
    }
}

