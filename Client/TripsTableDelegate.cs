using System;
using System.Globalization;
using AppKit;
using Services;

namespace Client;

public class TripsTableDelegate: NSTableViewDelegate
{
    private const string CellIdentifier = "TripCell";
    
    private TripsDataSource DataSource;

    public TripsTableDelegate(TripsDataSource dataSource)
    {
        DataSource = dataSource;
    }
    

    public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint nint)
    {
        NSTextField view = (NSTextField)tableView.MakeView(CellIdentifier, this);
        if (view == null)
        {
            view = new NSTextField();
            view.Identifier = CellIdentifier;
            view.BackgroundColor = NSColor.Clear;
            view.Bordered = false;
            view.Selectable = true;
            view.Editable = false;
            view.Cell.Editable = false;
            view.Alignment = NSTextAlignment.Center;
        }
        
        view.TextColor = NSColor.White;
        if (DataSource.Trips[(int)nint].Slots == 0)
            view.TextColor = NSColor.Red;
            
        switch (tableColumn.Title)
        {
            case "Landmark":
                view.StringValue = DataSource.Trips[(int)nint].Landmark;
                break;

            case "Transport Company":
                view.StringValue = DataSource.Trips[(int)nint].TransportCompany;
                break;

            case "Departure Time":
                view.StringValue = DataSource.Trips[(int)nint].DepartureTime.ToString("dd.MM.yyyy HH:mm:ss");
                break;
            
            case "Price":
                view.StringValue = DataSource.Trips[(int)nint].Price.ToString(CultureInfo.InvariantCulture); 
                break;
            
            case "Slots Left":
                view.StringValue = DataSource.Trips[(int)nint].Slots.ToString();
                break;
        }

        return view;
    }

}