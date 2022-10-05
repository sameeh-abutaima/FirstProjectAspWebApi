using AutoMapper;
using FirstprojectAspWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO.Pipelines;
using System;
using RestSharp;
using FirstprojectAspWebApi.DTOs.CSV;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FirstprojectAspWebApi.Controllers
{
    public class CsvController:ApiBaseController
    {
        private readonly FirstprojectAspWebApiDbContext _context;
        private readonly IMapper _mapper;

        public CsvController(FirstprojectAspWebApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/test/testtttttttttttttt")]
        public IActionResult Get()
        {
            var baseUrl = "https://mocki.io/v1/";
            var cleint = new RestClient(baseUrl);
            var request = new RestRequest("d4867d8b-b5d5-4a48-a4ab-79131b5809b8");
            var res = cleint.Execute(request);

            if (res.IsSuccessful)
            {
                var content = res.Content;
                var mappedResult = JsonConvert.DeserializeObject<List<RestResult>>(content);
                var ss = JsonConvert.SerializeObject(mappedResult);
                return Ok(mappedResult);
            }

            throw new Exception("Invalid request");
        }

        [HttpGet]
        [Route("test/api/{id}")]
        public IActionResult Get(int id)
        {
            var viewRes = _context.DetailsOfItems.Select(a => new { a.CategoryId });

            var modelView = _mapper.Map<List<CsvDTO>>(viewRes);

            var combinedRes = _context.Items
                                           .Select(a => new CsvDTO
                                           {
                                               ItemId = a.Id,
                                               SubCategoryId = a.SubCategoryId,
                                               ItemName = a.Name,
                                               SubCategoryName = a.SubCategory.Name,
                                               CategoryName = a.SubCategory.Category.Name,
                                               CategoryId = a.SubCategory.CategoryId,
                                               // Archived = a.Archived
                                           })
                                           .ToList();

            using (var writer = new StreamWriter("C:\\Users\\logitude\\Desktop\\tarining\\ItemsDB.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(modelView);
            }

            return Ok();
        }

    }
}
