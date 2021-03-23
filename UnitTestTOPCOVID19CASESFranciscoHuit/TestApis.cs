using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace UnitTestTOPCOVID19CASESFranciscoHuit
{
    [TestClass]
    public class TestApis
    {
        TOP10COVID19CASESFranciscoHuit.Controllers.COVID19ReportController controller;

        [TestMethod]
        public void testAPIRegions()
        {            
            //Arrange
            int miniumRegs = 0;

            //Act
            controller = new TOP10COVID19CASESFranciscoHuit.Controllers.COVID19ReportController();
            DataTable data = controller.getRegions();
            int apiRegsCount = data.Rows.Count;

            //Assert
            Assert.AreNotEqual(apiRegsCount,miniumRegs);
        }

        [TestMethod]
        public void testAPIReportForRegions()
        {
            //Arrange
            int miniumRegs = 0;

            //Act
            controller = new TOP10COVID19CASESFranciscoHuit.Controllers.COVID19ReportController();
            DataTable data = controller.getReportForRegions();
            int apiRegsCount = data.Rows.Count;

            //Assert
            Assert.AreNotEqual(apiRegsCount, miniumRegs);
        }

        [TestMethod]
        public void testAPIReportForProvinces()
        {
            //Arrange
            int miniumRegs = 0;
            String region = "US"; 

            //Act
            controller = new TOP10COVID19CASESFranciscoHuit.Controllers.COVID19ReportController();
            DataTable data = controller.getReportForProvinces(region);
            int apiRegsCount = data.Rows.Count;

            //Assert
            Assert.AreNotEqual(apiRegsCount, miniumRegs);
        }
    }
}
