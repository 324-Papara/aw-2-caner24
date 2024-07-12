using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Para.Api.Attribute;
using Para.Data.Context;
using Para.Data.Domain;
using Para.Data.UnitOfWork;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string? customerName, string? fields = "")
        {
            var entityList = customerName == null ? await _unitOfWork.CustomerRepository.GetAll(includeFields: fields) : await _unitOfWork.CustomerRepository.GetAll(x => x.FirstName == customerName, fields);
            return Ok(entityList);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> Get(long customerId)
        {
            var entity = await _unitOfWork.CustomerRepository.GetById(x => x.Id == customerId);
            return Ok(entity);
        }

        [HttpPost]
        [ServiceFilter(typeof(CustomerValidationAttribue))]
        public async Task<IActionResult> Post([FromBody] Customer value)
        {
            var entity = _unitOfWork.CustomerRepository.Insert(value);
            await _unitOfWork.CustomerRepository.Save();
            return StatusCode(201);
        }

        [HttpPut("{customerId}")]
        [ServiceFilter(typeof(CustomerValidationAttribue))]
        public async Task<IActionResult> Put(long customerId, [FromBody] Customer value)
        {
            var customer = await _unitOfWork.CustomerRepository.GetById(x => x.Id == customerId);
            if (customer is null)
                return StatusCode(StatusCodes.Status404NotFound);

            customer = value;
            //change tracking aktif olduğu için update yapmaya gerek yok
            //await _unitOfWork.CustomerRepository.Update(customer);
            await _unitOfWork.CustomerRepository.Save();
            return Ok();
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> Delete(long customerId)
        {
            var entity = await _unitOfWork.CustomerRepository.GetById(x => x.Id == customerId);
            await _unitOfWork.CustomerRepository.Delete(entity);
            await _unitOfWork.CustomerRepository.Save();
            return Ok();

        }
    }
}