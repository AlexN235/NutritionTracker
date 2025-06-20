using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CsvHelper;
using Microcharts;
using NutruitionTracker.NutritionFacts;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Linq;

namespace NutruitionTracker.ViewModel;

public partial class MyMealsViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private ObservableCollection<FoodDisplayGroup> mealList;
    [ObservableProperty]
    private FoodDisplay selectedItem;
    [ObservableProperty]
    private LineChart lineChart;

    private const string FILENAME = "MealInfo.csv";
    private string FilePath { get; set; }

    public MyMealsViewModel() 
    {
        MealList = new ObservableCollection<FoodDisplayGroup>();
        FilePath = Path.Combine(FileSystem.Current.AppDataDirectory, FILENAME);
        ImportLocalData();
        CreateLineChart();
    }

    [RelayCommand]
    async Task GoBack() 
    {
        SaveLocalData(MealList.ToList());
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task GoToAddMeal()
    {
        await Shell.Current.GoToAsync(nameof(InputMealPage));
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
                    if (group.Count == 0) 
                    {
                        MealList.Remove(group);
                    }
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
            MealList.Insert(0, new FoodDisplayGroup(date.ToString("d"), temp));
        }
        UpdateList(date.ToString("d"));
    }

    // Update observable collection of meals to dynamically display when adding or deleting meals.
    private void UpdateList(string date)
    { 
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
                MealList.Insert(0, new FoodDisplayGroup(date, food_list));
            }
        }
        CreateLineChart(); // update chart as well
    }

    // Called to ensure date of meals are displayed in the correct order when changes are made.
    private void SortMealList() 
    {
        
        var sortedList = MealList.OrderByDescending(m => m.Name);
        MealList.Clear();
        foreach (var meal in sortedList) 
        {
            MealList.Add(meal);
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
            if (query["toDelete"] is not FoodDisplay q)
                return;
            this.DeleteMeal(q);
        }
        else if (query?.ContainsKey("Meal") != null)
        {
            if (query["Meal"] is not FoodDisplay q)
                return;
            this.AddMeal(q);
        }
        query?.Clear();
        return;
    }

    private void CreateLineChart() 
    {
        List<ChartEntry> entries = new List<ChartEntry>();
        int lastNumberOfDays = 7;
        DateTime dateUntil = DateTime.Now.AddDays(-lastNumberOfDays);
        foreach (FoodDisplayGroup day in MealList) {
            if (day.GetDateTime() < dateUntil) {
                break;
            }

            // get totals for the day and add a new 
            float dayTotal = 0;
            foreach (FoodDisplay meal in day) 
            {
                dayTotal += (meal.Item as MealItem).GetCalories();
            }
            entries.Add(new ChartEntry(dayTotal)
            {
                Label = day.Name,
                ValueLabel = dayTotal.ToString("G"),
                ValueLabelColor = SKColor.Parse("#f9fafb"),
                Color = SKColor.Parse("#90d585")
            });
        }
        LineChart = new LineChart { Entries = entries, LineMode=LineMode.Straight };
        LineChart.BackgroundColor = SKColor.Parse("#383e42");
        LineChart.LabelColor = SKColor.Parse("#f9fafb");
    }

    // ------------------------------------------------------------------------------------------
    // Methods for loading and saving presistent data locally.
    // ------------------------------------------------------------------------------------------

    // Get data from locally saved files and populate the meal list.
    public void ImportLocalData()
    {
        if (!File.Exists(FilePath)) 
        {
            File.Create(FilePath);
        }

        ObservableCollection<FoodDisplayGroup> temp = new ObservableCollection<FoodDisplayGroup>();
        using StreamReader reader = new StreamReader(FilePath);
        using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
        {
            var records = csv.GetRecords<Meal>();
            temp = new ObservableCollection<FoodDisplayGroup>(MealToFood(records));
        }
        
        var sortedMeals = temp.OrderByDescending(m => m.Name);
        foreach (FoodDisplayGroup day in sortedMeals) 
        {
            MealList.Add(day);
        }
    }

    // Saves meal list locally.
    public void SaveLocalData(List<FoodDisplayGroup> foods)
    {
        if(!File.Exists(FilePath)) 
        {
            return;
        }

        using StreamWriter writer = new StreamWriter(FilePath);
        using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
        {
            List<Meal> meals = new List<Meal>();
            foreach (FoodDisplayGroup group in foods)
            {
                foreach (FoodDisplay food in group)
                {
                    MealItem item = (MealItem)food.Item;
                    Meal meal = new Meal(group.Name, food.Name, food.Value, item.Name, item.Date, item.ItemsValue);
                    meals.Add(meal);
                }
            }
            csv.WriteRecords(meals);
        }
    }

    private List<FoodDisplayGroup> MealToFood(IEnumerable<Meal> meals)
    {
        // Deserialize data into groups
        Dictionary<string, List<FoodDisplay>> map = new Dictionary<string, List<FoodDisplay>>();
        List<string> dateRecords = new List<string>();
        foreach (Meal meal in meals)
        {
            if (!dateRecords.Contains(meal.Date))
            {
                map.Add(meal.Date, new List<FoodDisplay>());
                dateRecords.Add(meal.Date);
            }
            MealItem m = new MealItem(meal.getMealValues(), meal.Name, meal.FullDate);
            FoodDisplay foodDisplay = new FoodDisplay(meal.DisplayName, meal.Value, m);
            map[meal.Date].Add(foodDisplay);
        }

        List<FoodDisplayGroup> foodDisplays = new List<FoodDisplayGroup>();
        foreach (string group in dateRecords)
        {
            foodDisplays.Add(new FoodDisplayGroup(group, map[group]));
        }
        return foodDisplays;
    }

    // Class used to serialize data.
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

