using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using AMS360.AMS360.API.Library.Interfaces;
using AMS360.API.Data.Interfaces;
using AMS360.API.Dtos;
using AMS360.API.Helpers;
using AMS360.API.Library.Interfaces;
using AMS360.API.Models;
using AutoMapper;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMS360.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class FundController : ControllerBase
    {
        private readonly IFundService _service;
        
        private readonly ICommonRepository _repoCommon;
        private readonly IExcelExportService<FundDto> _export;
        private readonly IExcelImportService _import;
        public FundController(IFundService service, ICommonRepository repoCommon,IExcelImportService import, IExcelExportService<FundDto> export)
        {
            _repoCommon = repoCommon;            
            _service = service;
            _export = export;
            _import = import;
        }
        [Authorize(Policy = "AdminView")]
        [HttpGet()]
        public IActionResult getFunds([DataSourceRequest]DataSourceRequest request)
        {

            var data = _service.GetFunds(request);
            return Ok(data);


        }
        [Authorize(Policy = "AdminView")]
        [HttpGet("full")]
        public async Task<IActionResult> getFundsFull()
        {

            var funds = await _service.GetFundFull();

            return Ok(funds);


        }
        [Authorize(Policy = "AdminView")]
        [HttpGet("{id}", Name = "GetFund")]
        public async Task<IActionResult> GetFund(string id)
        {

            var fund = await _service.GetFund(id);
            return Ok(fund);


        }
        [Authorize(Policy = "Admin")]
        [HttpPost()]
        public async Task<IActionResult> Create(FundDto model)
        {

            var validation = await _service.Create(model);
            if (validation.IsValid)
            {
                return CreatedAtRoute("GetFund", new { controller = "Fund", id = model.Id }, model);
            }
            else
            {
                return BadRequest(validation.Errors);
            }
        }
        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, FundDto model)
        {
            var validation = await _service.Update(id, model);
            if (validation.IsValid)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(validation.Errors);
            }

        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFund(string id)
        {

         var validation = await _service.Delete(id);
            if (validation.IsValid)
            {
                return Ok();
            }
            else
            {
                return BadRequest(validation.Errors);
            }

        }
        [Authorize(Policy = "Admin")]
        [HttpGet("lastCode", Name = "GetLastCodeFund")]
        public async Task<IActionResult> GetLastCode()
        {
            return Ok(await _repoCommon.GetLastestCode("F", "WipFund"));
        }
        [Authorize(Policy = "AdminView")]
        [HttpGet("export/{onlytemplate}")]
        public async Task<IActionResult> Export(bool onlyTemplate)
        {
            var fieldList = new List<ExportFieldDto>();
            fieldList.Add(new ExportFieldDto { FieldName = "ID", FieldDBName = "Id", IsRequired = true });
            fieldList.Add(new ExportFieldDto { FieldName = "Description", FieldDBName = "Description", IsRequired = true });
            var fundList = new List<FundDto>();

            if (!onlyTemplate)
            {
                fundList = await _service.GetFundFull();
            }
            var reportStream = _export.ExportGeneral(new ExportDto<FundDto>
            {
                TemplateOnly = onlyTemplate,
                Fields = fieldList,                
                NoOfFieldsRequired = 2,
                ObjectList = fundList,
                WorkBookName = "Fund"
            });
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Response.ContentType = contentType;
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            var fileContentResult = new FileContentResult(reportStream.ToArray(), contentType)
            {
                FileDownloadName = "Funds.xlsx"
            };
            return fileContentResult;



        }

        [Authorize(Policy = "Admin")]
        [HttpPost("import")]
        public async Task<IActionResult> Import([FromForm]ImportDto model)
        {
            var returnImport = await _import.Import(ImportType.Fund, "FUN", model);
            if (returnImport.Item1 == ImportResult.Success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(returnImport.Item2);
            }
        }

    }
}