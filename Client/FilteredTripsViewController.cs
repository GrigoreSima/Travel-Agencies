using System;
using System.Collections.Generic;
using System.Linq;
using AppKit;
using Model;
using Foundation;
using Services;

namespace Client
{
    public partial class FilteredTripsViewController : NSViewController, IObserver
    {
        private readonly List<ReservationViewController> _reservationViewControllers =
            new List<ReservationViewController>();

        public String Landmark { get; set; }
        public Int32 lowerLimit { get; set; }
        public Int32 upperLimit { get; set; }

        public User User { get; set; }

        private TripsDataSource _tripsSource;

        public FilteredTripsViewController(IntPtr handle) : base(handle)
        {
        }

        public void ReloadTrips()
        {
            IService server = SingletonProxy.GetInstance();

            _tripsSource = new TripsDataSource();
            _tripsSource.Trips = server.FindAllTripsForLandmarkInInterval(Landmark, lowerLimit, upperLimit, User);

            FilteredTripsTable.DataSource = _tripsSource;
            FilteredTripsTable.Delegate = new TripsTableDelegate(_tripsSource);
        }

        partial void RowClicked(AppKit.NSTableView sender)
        {
            if (FilteredTripsTable.SelectedRow == -1 ||
                _tripsSource.Trips[(int)FilteredTripsTable.SelectedRow].Slots == 0
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
                viewController.Trip = _tripsSource.Trips[(int)FilteredTripsTable.SelectedRow];

                viewController.ViewDidLoad();
                controller.ShowWindow(this);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception);
            }
        }

        public void logOut()
        {
            foreach (var reservationViewController in _reservationViewControllers)
            {
                reservationViewController.View.Window.Close();
            }
        }

        public void SlotsModified()
        {
            BeginInvokeOnMainThread(() =>
            {
                _tripsSource.Trips = SingletonProxy.GetInstance()
                    .FindAllTripsForLandmarkInInterval(Landmark, lowerLimit, upperLimit, User);
                FilteredTripsTable.ReloadData();
            });
        }
    }
}