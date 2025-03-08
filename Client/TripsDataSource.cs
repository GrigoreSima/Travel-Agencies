using System;
using System.Collections.Generic;
using AppKit;
using Model;

namespace Client
{
    public class TripsDataSource: NSTableViewDataSource
    {
        public List<Trip> Trips = new List<Trip>();

        public TripsDataSource()
        {
        }

        public override nint GetRowCount (NSTableView tableView)
        {
            return Trips.Count;
        }
    }
}