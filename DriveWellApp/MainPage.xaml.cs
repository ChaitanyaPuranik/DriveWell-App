﻿using DriveWellApp.BusinessLogic;

namespace DriveWellApp
{
    public partial class MainPage : ContentPage
    {

        CarInventory inventory = new CarInventory();


        public MainPage()
        {
            InitializeComponent();
            CarTypePicker.ItemsSource = Enum.GetValues<CarType>();
            YearPicker.ItemsSource = GetYear();
            CarsListView.ItemsSource = inventory.Cars;
            CarCountLabel.Text = $"Car count {inventory.Cars.Count()}";
            InventoryPricelabel.Text = $"Total inventory net price: {inventory.TotalInventoryNetPrice()}";
   
         }

        public List<int> GetYear()
        {
            List<int> years = new List<int>();
            for(int year = 2013;  year <= 2024;  year++)
            {
                years.Add(year);
            }
            return years;
        }

        private void OnAddCar(object sender, EventArgs e)
        {
            try
            {
                string vin = vinEntry.Text;
                string carmake = makeEntry.Text;
                CarType cartype = (CarType)CarTypePicker.SelectedItem;
                float price = float.Parse(priceEntry.Text);
                int year = int.Parse(YearPicker.SelectedItem.ToString());

                Car carobj = new Car(vin, carmake, cartype, price, year);
                inventory.AddCar(carobj);
                DisplayAlert("Car", $"Car added to inventory", "Ok");
                CarsListView.ItemsSource = null;
                CarsListView.ItemsSource = inventory.Cars;
                CarCountLabel.Text = $"Car count {inventory.Cars.Count()}";
                InventoryPricelabel.Text = $"Total inventory net price: {inventory.TotalInventoryNetPrice()}";
            }
            catch (Exception ex)
            {
                DisplayAlert("Error404",$"{ex}","Ok"); 
            }
        }

        private void OnUpdateCar(object sender, EventArgs e)
        {
            try
            {
                vinLabel.Text = "Enter Car VIN to update: ";
                string vin = vinEntry.Text;

                if (inventory.GetByVIN(vin) is null)
                {
                    DisplayAlert("Error", $"Car with {vin} does not exists", "Ok");
                }
                else
                {
                    string carmake = makeEntry.Text;
                    CarType cartype = (CarType)CarTypePicker.SelectedItem;
                    float price = float.Parse(priceEntry.Text);
                    int year = int.Parse(YearPicker.SelectedItem.ToString());
                    inventory.UpdateCar(vin, carmake, cartype, price, year);
                    DisplayAlert("Car", $"Car updated successfully", "Ok");
                    CarsListView.ItemsSource = null;
                    CarsListView.ItemsSource = inventory.Cars;
                    CarCountLabel.Text = $"Car count {inventory.Cars.Count()}";
                    InventoryPricelabel.Text = $"Total inventory net price: {inventory.TotalInventoryNetPrice()}";

                }
            }
            catch(Exception ex)
            {
                DisplayAlert("Error404", $"{ex}", "Ok");
            }
        }

        private void OnClearCar(object sender, EventArgs e)
        {

            vinEntry.Text = null;
            makeEntry.Text = null;
            CarTypePicker.SelectedIndex = 0;
            CarImage.Source = "dotnet_bot.png";
            
            priceEntry.Text = null;
            YearPicker.SelectedItem = null;
        }

        private void OnSelectFromPicker(object sender, EventArgs e)
        {
            
            CarType cartype = (CarType)CarTypePicker.SelectedItem;
            switch (cartype)
            {
               
                case CarType.Coupe:
                    CarImage.Source = "coupe.png";
                    break;

                case CarType.Sedan:
                    CarImage.Source = "sedan.png";
                    break;

                case CarType.SUV:
                    CarImage.Source = "suv.png";
                    break;

                case CarType.Hatchback:
                    CarImage.Source = "hatchback.png";
                    break;

                case CarType.None:
                    CarImage.Source = "dotnet_bot,png";
                    break;

                default:
                    CarTypePicker.SelectedItem = null;
                    break;
            }
        }
    }

}
