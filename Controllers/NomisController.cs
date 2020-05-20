using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laba1_ISTTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NomisController : ControllerBase
    {
        private readonly DanceContext _context;
        public NomisController(DanceContext context)
        {
            _context = context;
        }
        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var nomins = _context.NominationList.Include(n => n.Nomination).ToList();
            List<object> catNom = new List<object>();
            catNom.Add(new[] { "Номінація", "Кількість номінацій" });
            foreach (var n in nomins)
            {
                catNom.Add(new object[] { n.Name, n.Nomination.Count() });

            }
            return new JsonResult(catNom);

        }
    }
}