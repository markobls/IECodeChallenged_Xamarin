using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PacmanSimulator
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        bool placed = false;

        public Pacman pac { get; set; }

        public MainPage()
        {
            InitializeComponent();

            pac = new Pacman();

            
            MessagingCenter.Subscribe<InputPage>(this, "UpdatePlace", (sender) =>
            {
                if (!placed)
                {
                    placed = true;
                    UpdateCommandState();
                }

                UpdatePacImage();
            });

            SetupSimulator();

        }

        public async void SetupSimulator()
        {
            await DisplayAlert("Welcome!", "Use a PLACE command to start the simulation", "OK");
            UpdateCommandState();
        }

        void UpdatePacImage()
        {
            Grid.SetRow(pacImage, 4 - (int)pac.PositionY);
            Grid.SetColumn(pacImage, (int)pac.PositionX);
            pacImage.Rotation = (int)pac.Facing;

            // report after move
            //x.Text = pac.PositionX.ToString();
            //y.Text = pac.PositionY.ToString();
            //ang.Text = pac.FacingString;

            Report_Clicked(null, null);
        }

        void UpdateCommandState()
        {
            if (placed)
            {
                pacImage.IsVisible = true;
                leftIcon.Opacity = 1f;
                rightIcon.Opacity = 1f;
                moveIcon.Opacity = 1f;
                reportIcon.Opacity = 1f;
            }
            else
            {
                pacImage.IsVisible = false;
                leftIcon.Opacity = 0.25f;
                rightIcon.Opacity = 0.25f;
                moveIcon.Opacity = 0.25f;
                reportIcon.Opacity = 0.25f;
            }
        }

        private void Left_Clicked(object sender, EventArgs e)
        {
            pac.Turn(true);
            UpdatePacImage();
        }

        private void Right_Clicked(object sender, EventArgs e)
        {         
            pac.Turn(false);
            UpdatePacImage();
        }

        private void Move_Clicked(object sender, EventArgs e)
        {
            pac.Move();
            UpdatePacImage();
        }

        private void Report_Clicked(object sender, EventArgs e)
        {            
            x.Text = (pac.PositionX).ToString();
            y.Text = (pac.PositionY).ToString();
            ang.Text = pac.FacingString;
        }

        private async void Place_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new InputPage(pac));
        }
    }
}
