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
    public class StudiosController : ControllerBase
    {
        private readonly DanceContext _context;
        public StudiosController(DanceContext context)
        {
            _context = context;
        }
        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var nomins = _context.DanceStudio.Include(n => n.Dancer).ToList();
            List<object> catNom = new List<object>();
            catNom.Add(new[] { "Танцівники", "Кількість танцівників" });
            foreach (var n in nomins)
            {
                catNom.Add(new object[] { n.Name, n.Dancer.Count() });

            }
            return new JsonResult(catNom);

        }
    }
}