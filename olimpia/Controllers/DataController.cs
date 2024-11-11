﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using olimpia.Models;

namespace Olimpia.Controllers
{
    [Route("Data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Data> Post(CreateDataDto createDataDto)
        {
            var data = new Data()
            {
                Id = Guid.NewGuid(),
                Country = createDataDto.Country,
                County = createDataDto.County,
                Description = createDataDto.Description,
                PlayerId = createDataDto.PlayerId,
            };

            if (data != null)
            {
                using (var context = new OlimpiaContext())
                {
                    context.Datas.Add(data);
                    context.SaveChanges();
                    return StatusCode(201, data);
                }
            }
            return BadRequest();
        }
    }
}