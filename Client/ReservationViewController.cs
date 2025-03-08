using System;
using System.Globalization;
using AppKit;
using Foundation;
using Model;
using Services;

namespace Client
{
    public partial class ReservationViewController : NSViewController
    {

        public User User { get; set; }
        public Trip Trip { get; set; }


        public ReservationViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TicketsNoTextField.StringValue = "1";

            if (Trip == null) return;
            
            LandmarkTextField.StringValue = Trip.Landmark;
            TransporCompanyTextField.StringValue = Trip.TransportCompany;
            DepartureTimeTextField.StringValue = Trip.DepartureTime.ToString(CultureInfo.InvariantCulture);
            TotalTextField.StringValue = Trip.Price.ToString(CultureInfo.InvariantCulture);
        }

        partial void ModifiedTicketsNo(AppKit.NSTextFieldCell sender)
        {
            try
            {
                if (Int32.Parse(TicketsNoTextField.StringValue) > 
                    Trip.Slots)
                {
                    TicketsNoTextField.TextColor = NSColor.Red;
                    return;
                }
                
                TicketsNoTextField.TextColor = NSColor.White;
                TotalTextField.StringValue = (Int32.Parse(TicketsNoTextField.StringValue) * Trip.Price).ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception);
            }
        }

        partial void ReserveClicked(AppKit.NSButton sender)
        {
            try
            {
                Int32 ticketsNo = Int32.Parse(TicketsNoTextField.StringValue);
                IService server = SingletonProxy.GetInstance();
                if (ticketsNo > Trip.Slots)
                {
                    TicketsNoTextField.TextColor = NSColor.Red;
                    return;
                }
                
                server.SaveReservation(new Reservation(1L, NameTextField.StringValue, 
                    PhoneTextField.StringValue, ticketsNo, Trip));
                
                View.Window.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception);
            }
        }
    }
}
