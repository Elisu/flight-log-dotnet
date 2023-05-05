using System;
using Microsoft.Extensions.Logging;

namespace FlightLogNet.Operation
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using FlightLogNet.Models;
    using FlightLogNet.Repositories.Interfaces;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public class GetExportToCsvOperation
    {
        private readonly IFlightRepository flightRepository;

        public GetExportToCsvOperation(IFlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        public byte[] Execute()
        {
            StringBuilder sb = new StringBuilder();

            // Write the header row to the CSV content
            sb.AppendLine("FlightId,TakeoffTime,LandingTime,Immatriculation,Type,Pilot,Copilot,Task,TowplaneID,GliderID");

            var flightReports = flightRepository.GetReport();

            foreach (var flightReport in flightReports)
            {
                if (flightReport.Glider != null)
                {
                    sb.AppendLine(
                        $"{flightReport.Glider.Id}," +
                        $"{flightReport.Glider.TakeoffTime.ToString("dd.MM.yyyy HH:mm:ss")}," +
                        $"{flightReport.Glider.LandingTime?.ToString("dd.MM.yyyy HH:mm:ss")}," +
                        $"{flightReport.Glider.Airplane.Immatriculation}," +
                        $"{flightReport.Glider.Airplane.Type}," +
                        $"{flightReport.Glider.Pilot?.FirstName} {flightReport.Glider.Pilot?.LastName}," +
                        $"{(flightReport.Glider.Copilot != null ? flightReport.Glider.Copilot.FirstName + " " + flightReport.Glider.Copilot.LastName : " ")}," +
                        $"{flightReport.Glider.Task}," +
                        $"{flightReport.Towplane.Id}," +
                        $"{flightReport.Glider.Id}");
                }

                sb.AppendLine(
                    $"{flightReport.Towplane.Id}," +
                    $"{flightReport.Towplane.TakeoffTime.ToString("dd.MM.yyyy HH:mm:ss")}," +
                    $"{flightReport.Towplane.LandingTime?.ToString("dd.MM.yyyy HH:mm:ss")}," +
                    $"{flightReport.Towplane.Airplane.Immatriculation}," +
                    $"{flightReport.Towplane.Airplane.Type}," +
                    $"{flightReport.Towplane.Pilot?.FirstName} {flightReport.Towplane.Pilot?.LastName}," +
                    $"{(flightReport.Towplane.Copilot != null ? flightReport.Towplane.Copilot.FirstName + " " + flightReport.Towplane.Copilot.LastName : " ")}," +
                    $"{flightReport.Towplane.Task}," +
                    $"{flightReport.Towplane.Id}," +
                    $"{flightReport.Glider?.Id}");

            }
            /*
            foreach (var flight in flights)
            {
                sb.AppendLine(
                    $"{flight.Id}," +
                    $"{flight.TakeoffTime.ToString("dd.MM.yyyy HH:mm:ss")}," +
                    $"{flight.LandingTime?.ToString("dd.MM.yyyy HH:mm:ss")}," +
                    $"{flight.Airplane.Immatriculation}," +
                    $"{flight.Airplane.Type}," +
                    $"{flight.Pilot?.FirstName} {flight.Pilot?.LastName}," +
                    $"{(flight.Copilot != null ? flight.Copilot.FirstName + " " + flight.Copilot.LastName : "")}," +
                    $"{flight.Task}," +
                    $"{(flight.Task == "Tahac" ? flight.Id : "")}," +
                    $"{(flight.Task == "VLEK" ? flight.Id : "")}");
            }*/

            // Convert the CSV content to a byte array using UTF8 encoding
            byte[] csvBytes = Encoding.UTF8.GetBytes(sb.ToString());

            // Return the byte array
            return csvBytes;
        }
    }
}
