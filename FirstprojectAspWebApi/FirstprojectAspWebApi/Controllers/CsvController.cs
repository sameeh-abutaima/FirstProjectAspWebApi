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
using FirstprojectAspWebApi.Core.Managers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using FirstprojectAspWebApi.Attributes;

namespace FirstprojectAspWebApi.Controllers
{
    public class CsvController:ApiBaseController
    {
        private readonly ICsvManager _csvManager;
        public CsvController(ICsvManager csvManager)
        {
            _csvManager = csvManager;
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
        [Route("api/test/export")]
        [Authorize]
        [FirstprojectAspWebApiAuthorize()]
        public IActionResult ExportToCsv()
        {
            
            _csvManager.ExportToCsv();
            return Ok();
        }

    }
}
