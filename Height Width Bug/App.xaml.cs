using System.Diagnostics;

namespace Height_Width_Bug {
    public partial class App : Application {

        public event Action screenSizeChanged = null;
        public double screenWidth = 0;
        public double screenHeight = 0;
        ContentPage mainPage;
        public App() {

            InitializeComponent();

            //=========
            //LAYOUT
            //=========
            mainPage = new();
            mainPage.Background = Colors.Magenta;
            MainPage = mainPage;
            mainPage.SizeChanged += delegate {
                invokeScreenSizeChangeEvent();
            };

            //VerticalStackLayout vert = new();
            AbsoluteLayout vert = new();
            mainPage.Content = vert;
            vert.BackgroundColor = Colors.Red;
            vert.HorizontalOptions = LayoutOptions.Center;
            vert.Margin = 0;
            vert.Padding = 0;

            
            Border border1 = new Border();
            border1.BackgroundColor = Colors.YellowGreen;
            border1.Margin = 0;
            vert.Children.Add(border1);
            border1.SizeChanged += delegate {
                Debug.WriteLine("BORDER WIDTH " + border1.Width + "BORDER HEIGHT " + border1.Height + " BOUNDS " + border1.Bounds);
            };

            //==================
            //RESIZE FUNCTION
            //==================
            screenSizeChanged += delegate {

                vert.HeightRequest = screenHeight;
                vert.WidthRequest = screenWidth;

                border1.HeightRequest = screenHeight;
                border1.WidthRequest = screenWidth; 

            };


        }
        private void invokeScreenSizeChangeEvent() {
            if (mainPage.Width > 0 && mainPage.Height > 0) {
                screenWidth = mainPage.Width;
                screenHeight = mainPage.Height;
                screenSizeChanged?.Invoke();
                Debug.WriteLine("main page size changed | width: " + screenWidth + " height: " + screenHeight);
            }
        }
    }
}