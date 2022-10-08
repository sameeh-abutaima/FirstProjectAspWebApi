using AutoMapper;
using CsvHelper;
using FirstprojectAspWebApi.Core.Managers.Interfaces;
using FirstprojectAspWebApi.DTOs.CSV;
using FirstprojectAspWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstprojectAspWebApi.Core.Managers
{
    public class CsvManager : ICsvManager
    {
        #region dependencyInjection
        private readonly FirstprojectAspWebApiDbContext _context;
        private readonly IMapper _mapper;
        public CsvManager(FirstprojectAspWebApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion dependencyInjection
        public void ExportToCsv()
        {
            var viewRes = _context.DetailsOfItems.ToList();

            var modelView = _mapper.Map<List<CsvDTO>>(viewRes);

            using (var writer = new StreamWriter(@"D:\MainProject\FirstprojectAspWebApi\FirstprojectAspWebApi\FirstprojectAspWebApi.csproj\ItemsDB.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(modelView);
            }
        }
    }
}
