namespace FlightLogNet.Controllers
{
    using System.Collections.Generic;

    using FlightLogNet.Facades;
    using FlightLogNet.Models;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [EnableCors]
    [Route("[controller]")]
    public class AirplaneController : ControllerBase
    {
        private readonly ILogger<FlightController> logger;
        private readonly AirplaneFacade _airplaneFacade;

        public AirplaneController(ILogger<FlightController> logger, AirplaneFacade airplaneFacade)
        {
            this.logger = logger;
            this._airplaneFacade = airplaneFacade;
        }
        // 3.1: Vystavte REST HTTPGet metodu vracející seznam klubových letadel
        // Letadla získáte voláním airplaneFacade
        // dotazované URL je /airplane
        // Odpověď by měla být kolekce AirplaneModel

        [HttpGet]
        public IEnumerable<AirplaneModel> GetPlanesInAir()
        {
            this.logger.LogDebug("Get airplanes.");
            return _airplaneFacade.GetClubAirplanes();
        }
    }
}
