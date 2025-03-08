using System;
using System.Collections.Generic;
using System.Net.Mime;
using AppKit;
using Model;
using Foundation;
using Services;

namespace Client
{
    public partial class ViewController : NSViewController, IObserver
    {
        private TripsDataSource _tripsSource;

        private readonly List<FilteredTripsViewController> _tripsViewControllers =
            new List<FilteredTripsViewController>();

        private readonly List<ReservationViewController> _reservationViewControllers =
            new List<ReservationViewController>();

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public User User { get; set; }

        public void FillTable()
        {
            IService service = SingletonProxy.GetInstance();

            _tripsSource = new TripsDataSource();
            _tripsSource.Trips = service.FindAllTrips();

            tripTable.DataSource = _tripsSource;
            tripTable.Delegate = new TripsTableDelegate(_tripsSource);
        }

        partial void filterBtn(AppKit.NSButton sender)
        {
            if (landmarkTextField.StringValue == "" ||
                lowerLimitTextField.StringValue == "" ||
                upperLimitTextField.StringValue == "")
                return;

            try
            {
                var storyboard = NSStoryboard.MainStoryboard;
                var controller = storyboard.InstantiateControllerWithIdentifier("filteredWindow") as NSWindowController;
                var viewController = (FilteredTripsViewController)controller.ContentViewController;

                _tripsViewControllers.Add(viewController);

                viewController.User = User;
                viewController.Landmark = landmarkTextField.StringValue;
                viewController.lowerLimit = Int32.Parse(lowerLimitTextField.StringValue);
                viewController.upperLimit = Int32.Parse(upperLimitTextField.StringValue);
                viewController.View.Window.Title = $"User: {User.Username} | Landmark: {viewController.Landmark}" +
                                                   $" | Interval: [{viewController.lowerLimit}:00, {viewController.upperLimit - 1}:59]";


                viewController.ReloadTrips();
                controller.ShowWindow(this);

                landmarkTextField.StringValue = "";
                lowerLimitTextField.StringValue = "";
                upperLimitTextField.StringValue = "";
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception);
            }
        }

        partial void RowClicked(AppKit.NSTableView sender)
        {
            if (tripTable.SelectedRow == -1 ||
                _tripsSource.Trips[(int)tripTable.SelectedRow].Slots == 0
               ) return;

            try
            {
                var storyboard = NSStoryboard.MainStoryboard;
                var controller = storyboard.InstantiateControllerWithIdentifier("reservationController")
                    as NSWindowController;
                var viewController = (ReservationViewController)controller.ContentViewController;

                _reservationViewControllers.Add(viewController);

                viewController.View.Window.Title = $"User: {User.Username}";
                viewController.User = User;
                viewController.Trip = _tripsSource.Trips[(int)tripTable.SelectedRow];

                viewController.ViewDidLoad();
                controller.ShowWindow(this);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception);
            }
        }

        partial void logOutBtn(AppKit.NSButton sender)
        {
            SingletonProxy.GetInstance().Logout(User);
            foreach (var filteredTripsViewController in _tripsViewControllers)
            {
                filteredTripsViewController.logOut();
                filteredTripsViewController.View.Window.Close();
            }

            foreach (var reservationViewController in _reservationViewControllers)
            {
                reservationViewController.View.Window.Close();
            }

            View.Window.Close();
            NSApplication.SharedApplication.Terminate(this);
        }

        public void SlotsModified()
        {
            BeginInvokeOnMainThread(() =>
            {
                _tripsSource.Trips = SingletonProxy.GetInstance()
                    .FindAllTrips();
                tripTable.ReloadData();
                
                _tripsViewControllers.ForEach(controller => controller.SlotsModified());
            });
        }
    }
}