using PM2E107.Controllers;
using System.Data.Common;

namespace PM2E107 {
    public partial class App : Application {
        public static readonly DBController db = new DBController();








        public App() {
            InitializeComponent();

            MainPage = new AppShell();
        }




    }
}