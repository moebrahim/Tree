using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiReport1._0.Models;

namespace ApiReport1._0.Controllers
{
    [Authorize]
    public class TreeReportController : ApiController
    {
        public class ReportList
        {
            public string Distr_ID { get; set; }
            public string name { get; set; }
            public decimal? per_bp { get; set; }
            public decimal? SUM { get; set; }

            public int? Ratio { get; set; }



        }

        [HttpGet]
        public IHttpActionResult GetBp(string id)
        {
            List<ReportList> reportList = new List<ReportList>();
            var test = new ApiReportSpContainer();
            var result = test.GenerateReport(id).ToList();
            foreach (var item in result.ToList())
            {
                reportList.Add(new ReportList
                {
                    Distr_ID = item.DISTR_ID,
                    name = item.name,
                    per_bp = item.per_bp,
                    SUM = item.SUM,
                    Ratio = item.RATIO
                });
            }
            return Ok(reportList);
        }
    }
}
