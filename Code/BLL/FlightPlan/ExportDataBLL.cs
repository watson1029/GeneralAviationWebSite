using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FlightPlan
{
    public class ExportDataBLL
    {
        private DAL.FlightPlan.FlightPlanDAL flightdal = new DAL.FlightPlan.FlightPlanDAL();
        private DAL.FlightPlan.RepetitivePlanDAL repetdal = new DAL.FlightPlan.RepetitivePlanDAL();
        public List<ViewModel.FlightPlan.FlightPlanExportVM> FlightPlanDataExport(List<string> planlist)
        {
            var exportlist = new List<ViewModel.FlightPlan.FlightPlanExportVM>();
            var flightlist = flightdal.GetList(planlist);
            foreach (var flight in flightlist)
            {
                var export = new ViewModel.FlightPlan.FlightPlanExportVM();
                export.company = flight.CompanyName;
                export.airtype = flight.AircraftType;
                export.aircraft = flight.AircraftType;
                export.airline = flight.AirlineWorkText;
                export.high = repetdal.GetFlyHigh(flight.RepetPlanID, flight.AirlineWorkText);
                export.planbegin = flight.SOBT.ToString("HH:mm");
                export.planend = flight.SIBT.ToString("HH:mm");
                export.remark = flight.Remark;
                export.airport = string.Join(",", repetdal.GetAirportName(flight.RepetPlanID));
                export.messiontype = repetdal.GetFlightTaskName(flight.FlightType);
                exportlist.Add(export);
            }
            return exportlist;
        }

        
    }
}
