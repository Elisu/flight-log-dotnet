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
            // TODO 5.1: Naimplementujte export do CSV
            // TIP: CSV soubor je pouze string, který se dá vytvořit pomocí třídy StringBuilder
            // TIP: Do bytové reprezentace je možné jej převést například pomocí metody: Encoding.UTF8.GetBytes(..)

            StringBuilder sb = new StringBuilder();

            // Write the header row to the CSV content
            sb.AppendLine("FlightId,TakeoffTime,LandingTime,Immatriculation,Type,Pilot,Copilot,Task,TowplaneID,GliderID");

            // Loop through each object in the list and write its properties to a new line in the CSV content
            var flights = flightRepository.GetAllFlights().OrderBy(x => x.TakeoffTime);
            foreach (var flight in flights)
            {
                sb.AppendLine(
                    $"{flight.Id}," +
                    $"{flight.TakeoffTime}," +
                    $"{flight.LandingTime}," +
                    $"{flight.Airplane.Immatriculation}," +
                    $"{flight.Airplane.Type}," +
                    $"{flight.Pilot?.FirstName} {flight.Pilot?.LastName}," +
                    $"{(flight.Copilot != null ? flight.Copilot.FirstName + " " + flight.Copilot.LastName : "")}," +
                    $"{flight.Task}," +
                    $"{(flight.Task == "Tahac" ? flight.Id : "")}," +
                    $"{(flight.Task == "VLEK" ? flight.Id : "")}");
            }

            // Convert the CSV content to a byte array using UTF8 encoding
            byte[] csvBytes = Encoding.UTF8.GetBytes(sb.ToString());

            // Return the byte array
            return csvBytes;
        }
    }
}
