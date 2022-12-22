using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//using System.Runtime.Serialization.DataContract;

namespace sscpback.Controllers;

[ApiController]
[Route("[controller]")]
public class AppointmentlistController : ControllerBase
{
    public static List<Appointmentlist> allAppointmentList = new List<Appointmentlist>()
    {
        new Appointmentlist()
        {
            name = null,
            id = "f748a05c-6",
            Date = "21st Dec  2022",
            Appointmentdate = "21st Dec  2022",
            Starttime = "01:42",
            Endtime = "02:42",
            Appointmentcontent = "meet with ravi1"
        },
        new Appointmentlist()
        {
            name = null,
            id = "f748a05c-7",
            Date = "21st Dec  2022",
            Appointmentdate = "21st Dec  2022",
            Starttime = "04:42",
            Endtime = "05:42",
            Appointmentcontent = "meet with ravi2"
        },
        new Appointmentlist()
        {
            name = null,
            id = "f748a05c-8",
            Date = "21st Dec  2022",
            Appointmentdate = "21st Dec  2022",
            Starttime = "07:42",
            Endtime = "08:42",
            Appointmentcontent = "meet with ravi3"
        }
    };

    public static List<Appointmentlist> filteredAppointmentList = new List<Appointmentlist> { };
    public int count { get; set; }

    [HttpGet("{GetId}")]
    public IActionResult GetOne(string GetId)
    {
        filteredAppointmentList = allAppointmentList.Where(x => x.Date == GetId).ToList();
        filteredAppointmentList.Sort((x, y) => x.Starttime.CompareTo(y.Starttime));
        return Ok(filteredAppointmentList);
        // return null;
    }

    [HttpPost]
    public IActionResult Addapp(Appointmentlist listval)
    {
        filteredAppointmentList = allAppointmentList
            .Where(x => x.Date == listval.Appointmentdate)
            .ToList();

        foreach (var num in filteredAppointmentList)
        {
            if (
                (
                    (TimeSpan.Parse(listval.Starttime) >= TimeSpan.Parse(num.Starttime))
                        && (TimeSpan.Parse(listval.Starttime) <= TimeSpan.Parse(num.Endtime))
                    || (
                        (TimeSpan.Parse(listval.Endtime) >= TimeSpan.Parse(num.Starttime))
                        && (TimeSpan.Parse(listval.Endtime) <= TimeSpan.Parse(num.Endtime))
                    )
                )
                || (
                    (TimeSpan.Parse(num.Starttime) >= TimeSpan.Parse(listval.Starttime))
                        && (TimeSpan.Parse(num.Starttime) <= TimeSpan.Parse(listval.Endtime))
                    || (
                        (TimeSpan.Parse(num.Endtime) >= TimeSpan.Parse(listval.Starttime))
                        && (TimeSpan.Parse(num.Endtime) <= TimeSpan.Parse(listval.Endtime))
                    )
                )
                || ((TimeSpan.Parse(listval.Starttime) >= TimeSpan.Parse(listval.Endtime)))
            )
            {
                count++;
                break;
            }
        }
        if (count == 0)
        {
            allAppointmentList.Add(listval);
            count = 0;
            filteredAppointmentList = allAppointmentList
                .Where(x => x.Date == listval.Appointmentdate)
                .ToList();
            filteredAppointmentList.Sort((x, y) => x.Starttime.CompareTo(y.Starttime));
            return Created("", filteredAppointmentList);
        }
        else
        {
            return Conflict();
        }
    }

    [HttpPatch]
    public IActionResult Patchapp(Patchappointmentlist patchval)
    {
        filteredAppointmentList = allAppointmentList
            .Where(x => x.Date == patchval.Appointmentdate)
            .ToList();

        foreach (var num1 in filteredAppointmentList.ToList())
        {
            if (num1.id == patchval.id)
            {
                filteredAppointmentList.Remove(num1);
            }
        }

        foreach (var num in filteredAppointmentList)
        {
            if (
                (
                    (TimeSpan.Parse(patchval.Starttime) >= TimeSpan.Parse(num.Starttime))
                        && (TimeSpan.Parse(patchval.Starttime) <= TimeSpan.Parse(num.Endtime))
                    || (
                        (TimeSpan.Parse(patchval.Endtime) >= TimeSpan.Parse(num.Starttime))
                        && (TimeSpan.Parse(patchval.Endtime) <= TimeSpan.Parse(num.Endtime))
                    )
                )
                || (
                    (TimeSpan.Parse(num.Starttime) >= TimeSpan.Parse(patchval.Starttime))
                        && (TimeSpan.Parse(num.Starttime) <= TimeSpan.Parse(patchval.Endtime))
                    || (
                        (TimeSpan.Parse(num.Endtime) >= TimeSpan.Parse(patchval.Starttime))
                        && (TimeSpan.Parse(num.Endtime) <= TimeSpan.Parse(patchval.Endtime))
                    )
                )
                || ((TimeSpan.Parse(patchval.Starttime) >= TimeSpan.Parse(patchval.Endtime)))
            )
            {
                count++;
                break;
            }
        }

        filteredAppointmentList = allAppointmentList
            .Where(x => x.Date == patchval.Appointmentdate)
            .ToList();

        if (count == 0)
        {
            foreach (var num1 in filteredAppointmentList)
            {
                if (num1.id == patchval.id)
                {
                    num1.Starttime = patchval.Starttime;
                    num1.Endtime = patchval.Endtime;
                    num1.Appointmentcontent = patchval.Appointmentcontent;
                }
            }
            foreach (var num in allAppointmentList)
            {
                if (num.id == patchval.id)
                {
                    num.Starttime = patchval.Starttime;
                    num.Endtime = patchval.Endtime;
                    num.Appointmentcontent = patchval.Appointmentcontent;
                }
            }
            filteredAppointmentList.Sort((x, y) => x.Starttime.CompareTo(y.Starttime));
            return Created("", filteredAppointmentList);
        }
        else
        {
            return Conflict();
        }
    }

    [HttpDelete("{deleteId}")]
    public IActionResult Deleteapp(string deleteId)
    {
        Console.WriteLine(deleteId);
        foreach (var num in allAppointmentList.ToList())
        {
            if (num.id == deleteId)
            {
                allAppointmentList.Remove(num);
            }
        }
        foreach (var num1 in filteredAppointmentList.ToList())
        {
            if (num1.id == deleteId)
            {
                filteredAppointmentList.Remove(num1);
            }
        }
        filteredAppointmentList.Sort((x, y) => x.Starttime.CompareTo(y.Starttime));
        return Ok(filteredAppointmentList);
    }
}
