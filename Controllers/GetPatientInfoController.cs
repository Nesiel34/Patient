using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Xml.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetPatientInfoController : ControllerBase
    {

        [HttpGet("GetPatientById")]
        public IActionResult GetPatientById([FromBody] MyApiRequestParameters PatientId)
        {
            var patientArr = XDocument.Load(@"Patients.xml");
            Patient Patient = (from patient in patientArr.Descendants("Patient")
                                where patient.Element("PatientId").Value.CompareTo(PatientId.PatientId.ToString()) == 0
                                select  new Patient
                                {
                                    FirstName = patient.Element("FirstName").Value.ToString(),
                                    LastName = patient.Element("LastName").Value.ToString(),
                                    PatientId = (long)patient.Element("PatientId")

                                }).FirstOrDefault();
            if (Patient != null)
            {
                return Ok(Patient);
            }
            else
            {
                return NotFound("Patient Not Exits");  
            }
        }
    }
}
