using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public ActionResult<Data> Get()
        {
            using (var context = new OlimpiaContext())
            {
                return Ok(context.Datas.ToList());
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Data> GetById(Guid id)
        {
            using (var context = new OlimpiaContext())
            {
                var data = context.Datas.FirstOrDefault(x => x.Id == id);
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound();
            }
        }
        [HttpPut("{id}")]
        public ActionResult<Data> Put(Guid Id, UpdateDataDto updateDataDto)
        {
            using (var context = new OlimpiaContext())
            {
                var existingData = context.Datas.FirstOrDefault(d => d.Id == Id);
                if (existingData != null)
                {
                    existingData.Country = updateDataDto.Country;
                    existingData.County = updateDataDto.County;
                    existingData.Description = updateDataDto.Description;
            
                    context.Datas.Update(existingData);
                    context.SaveChanges();

                    return Ok(existingData);

                }
                return NotFound();
            }

        }
    }
}