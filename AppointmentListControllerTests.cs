using sscpback.Controllers;

namespace sscpback.unittest;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



public class AppointmentlistControllertest
{
    //FOR GET
    [Fact]
    public void Get_WhenCalled_ReturnsOkResult()
    {
        var v1 = new AppointmentlistController();
        var result = v1.GetOne("21st Dec  2022");
        // Assert
        Assert.IsType<OkObjectResult>(result as OkObjectResult);
    }

    [Fact]
    public void Get_WhenCalled_ReturnsAllItems()
    {
        // Act
        var v1 = new AppointmentlistController();
        var result = v1.GetOne("21st Dec  2022") as OkObjectResult;
        // Assert
        var items = Assert.IsType<List<Appointmentlist>>(result?.Value);
        Assert.Equal(3, items.Count);
    }

    //FOR ADD
    [Fact]
    public void Add_InvalidObjectPassed_ReturnsBadRequest()
    {
        var nameMissingItem = new Appointmentlist()
        {
            name = null,
            id = "f748a05c-6",
            Date = "21st Dec  2022",
            Appointmentdate = "21st Dec  2022",
            Starttime = "07:42",
            Endtime = "09:42",
            Appointmentcontent = "meet with ravi3"
        };
        var v1 = new AppointmentlistController();
        var result = v1.Addapp(nameMissingItem);
        Assert.IsType<ConflictResult>(result);
    }

    [Fact]
    public void Add_ValidObjectPassed_ReturnsCreatedResponse()
    {
        var nameMissingItem = new Appointmentlist()
        {
            name = null,
            id = "f748a05c-4",
            Date = "21st Dec  2022",
            Appointmentdate = "21st Dec  2022",
            Starttime = "11:42",
            Endtime = "12:42",
            Appointmentcontent = "meet with ravi3"
        };
        var v1 = new AppointmentlistController();
        var result = v1.Addapp(nameMissingItem);
        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
    {
        var nameMissingItem = new Appointmentlist()
        {
            name = null,
            id = "f748a05c-9",
            Date = "21st Dec  2022",
            Appointmentdate = "21st Dec  2022",
            Starttime = "21:42",
            Endtime = "22:42",
            Appointmentcontent = "meet with ravi3"
        };
        var v1 = new AppointmentlistController();
        var result = v1.Addapp(nameMissingItem) as CreatedResult;
        var items = Assert.IsType<List<Appointmentlist>>(result?.Value);
        Assert.Equal("f748a05c-9", items[3].id);
    }

    //FOR PATCH
    [Fact]
    public void Patchapp_InvalidObjectPassed_ReturnsBadRequest() 
    {
        var nameMissingItem = new Patchappointmentlist(){
            id = "f748a05c-6",
            Appointmentdate = "21st Dec  2022",
            Starttime = "07:42",
            Endtime = "09:42",
            Appointmentcontent = "meet with ravi3"
        };
        var v1 = new AppointmentlistController();
        var result = v1.Patchapp(nameMissingItem);
        Assert.IsType<ConflictResult>(result);
    }

    [Fact]
    public void Patchapp_ValidObjectPassed_ReturnsCreatedResponse(){
        var nameMissingItem = new Patchappointmentlist() {
            id = "f748a05c-6",
            Appointmentdate = "21st Dec  2022",
            Starttime = "09:42",
            Endtime = "10:42",
            Appointmentcontent = "meet with ravi3"
        };
        var v1 = new AppointmentlistController();
        var result = v1.Patchapp(nameMissingItem);
        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public void Patchapp_ValidObjectPassed_ReturnedResponseHasCreatedItem()
    {
        var nameMissingItem = new Patchappointmentlist(){
            id = "f748a05c-6",
            Appointmentdate = "21st Dec  2022",
            Starttime = "09:42",
            Endtime = "10:42",
            Appointmentcontent = "meet with ravi3"
        };
        var v1 = new AppointmentlistController();
        var result = v1.Patchapp(nameMissingItem) as CreatedResult;
        var items = Assert.IsType<List<Appointmentlist>>(result?.Value);
        Assert.Equal("09:42", items[2].Starttime);
    }

    //FOR DELETE
    [Fact]
    public void Remove_ExistingGuidPassed_ReturnsResult()
    {
        // Arrange
        var existingGuid = "f748a05c-6";
        // Act
        var v1 = new AppointmentlistController();
        var result = v1.Deleteapp(existingGuid) as OkObjectResult;
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Remove_ExistingGuidPassed_RemovesOneItem()
    {
        var v1 = new AppointmentlistController();
        var getresult = v1.GetOne("21st Dec  2022");
        // Arrange
        var existingGuid = "f748a05c-7";
        var result = v1.Deleteapp(existingGuid) as OkObjectResult;
        var items = Assert.IsType<List<Appointmentlist>>(result?.Value);
        Assert.Equal(2, items.Count());
    }
}
