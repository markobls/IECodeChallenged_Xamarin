using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static PacmanSimulator.Pacman;

namespace PacmanSimulator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputPage : ContentPage
    {

        Pacman pacman;

        public InputPage()
        {
            InitializeComponent();
        }

        public InputPage(Pacman pac)
        {
            InitializeComponent();
            pacman = pac;
        }

        private async void Place_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xPositionEntry.Text) 
                || string.IsNullOrEmpty(yPositionEntry.Text)
                || directionPicker.SelectedItem == null)
            {
                await DisplayAlert("Error", "Please input all parameters correctly", "OK");
            }

            int positionX = int.Parse(xPositionEntry.Text);
            int positionY = int.Parse(yPositionEntry.Text);

            if (positionX > App.GridSize || positionX < 0 || positionY > App.GridSize || positionY < 0)
            {
                await DisplayAlert("Error", "Invalid position", "OK");
            }

            Direction dir = (Direction) directionPicker.SelectedIndex;

            // safety
            if (pacman != null)
            {
                pacman.Place(positionX, positionY, dir);
                MessagingCenter.Send(this, "UpdatePlace");
            }

            await Navigation.PopModalAsync();
        }
    }
}