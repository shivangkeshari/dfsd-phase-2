using API.Common.Interfaces.Services;
using API.Common.Models.PlannerToGcc4Assignment;
using API.Controllers;
using FakeItEasy;
using APIUnitTest.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Xunit;

namespace APIUnitTest.ControllerUnitTests
{
    public class PlannerToGcc4AssignmentControllerTests
    {
        IPlannerToGcc4AssignmentService _service;
        PlannerToGcc4AssignmentController _controller;

        public PlannerToGcc4AssignmentControllerTests()
        {
            ILoggerFactory loggerFactoryController = new LoggerFactory();
            ILogger<PlannerToGcc4AssignmentController> _loggerController = loggerFactoryController.CreateLogger<PlannerToGcc4AssignmentController>();

            _service = A.Dummy<IPlannerToGcc4AssignmentService>();
            _controller = new PlannerToGcc4AssignmentController(_loggerController, _service);
        }

        [Fact]
        public void Test_GetAllWithFilter_ReturnsCorrectData()
        {
            //Arrange
            string orderBy = "GCC4-DESC";
            long page = 1;
            long pageSize = 50;
            long totalCount = 1;

            PlannerToGcc4AssignmentFilter filter = new PlannerToGcc4AssignmentFilter()
            {
                Gcc4 = "",
                Role = "SCS"
            };

            List<PlannerToGcc4Assignment> result = new List<PlannerToGcc4Assignment>
            {
                new PlannerToGcc4Assignment
                {
                    Gcc4 = "",
                    Role = "SCS",
                    NameId = 68,
                    CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                    UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                    CreatedBy = "",
                    UpdatedBy = "",
                    PId = 267763,
                    UserId = "",
                    ManagerNameNew = ""
                }
            };

            A.CallTo(() => _service.GetAllWithFilter(orderBy, page, pageSize, filter)).Returns(result);
            A.CallTo(() => _service.RowCountWithFilter(filter)).Returns(totalCount);

            PlannerToGcc4AssignmentArray expected = new PlannerToGcc4AssignmentArray
            {
                plannerList = result,
                totalCount = totalCount
            };

            //Act
            var actionResult = _controller.GetAllWithFilter(orderBy, page, pageSize, filter);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var resultObject = TestHelpers.GetObjectResultContent<PlannerToGcc4AssignmentArray>(actionResult);
            Assert.Equal(expected.plannerList, resultObject.plannerList); 
            Assert.Equal(expected.totalCount, resultObject.totalCount);
        }

        [Fact]
        public void Test_GetAllWithFilter_ReturnsNoData()
        {
            //Arrange
            string orderBy = "GCC4-DESC";
            long page = 1;
            long pageSize = 50;
            long totalCount = 0;

            PlannerToGcc4AssignmentFilter filter = new PlannerToGcc4AssignmentFilter()
            {
                Gcc4 = "",
                Role = "SCS"
            };

            List<PlannerToGcc4Assignment> result = null;

            A.CallTo(() => _service.GetAllWithFilter(orderBy, page, pageSize, filter)).Returns(result);
            A.CallTo(() => _service.RowCountWithFilter(filter)).Returns(totalCount);

            string expected = "No Records Available!";

            //Act
            var actionResult = _controller.GetAllWithFilter(orderBy, page, pageSize, filter);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var resultObject = ((ObjectResult)actionResult.Result).Value; 
            Assert.Equal(expected, resultObject.ToString());
        }

        [Fact]
        public void Test_GetOnePlanner_ReturnsCorrectData()
        {
            //Arrange
            long pid = 267763;
            
            PlannerToGcc4Assignment expected = new PlannerToGcc4Assignment
            {
                Gcc4 = "",
                Role = "SCS",
                NameId = 68,
                CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                CreatedBy = "",
                UpdatedBy = "",
                PId = 267763,
                UserId = "",
                ManagerNameNew = ""
            };

            A.CallTo(() => _service.GetOnePlanner(pid)).Returns(expected);
            
            //Act
            var actionResult = _controller.GetOnePlanner(pid);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var resultObject = TestHelpers.GetObjectResultContent<PlannerToGcc4Assignment>(actionResult);
            Assert.Equal(expected, resultObject);
        }

        [Fact]
        public void Test_GetOnePlanner_ReturnsNoData()
        {
            //Arrange
            long pid = 999999;
            
            PlannerToGcc4Assignment result = null;
            string expected = "No Record Available!";

            A.CallTo(() => _service.GetOnePlanner(pid)).Returns(result);

            //Act
            var actionResult = _controller.GetOnePlanner(pid);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var resultObject = ((ObjectResult)actionResult.Result).Value;
            Assert.Equal(expected, resultObject.ToString());
        }


        [Fact]
        public void Test_InsertOnePlanner_ReturnsData()
        {
            //Arrange
            PlannerToGcc4Assignment planner = new PlannerToGcc4Assignment
            {
                Gcc4 = "",
                Role = "SCS",
                NameId = 68,
                CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                CreatedBy = "",
                UpdatedBy = "",
                PId = 267763,
                UserId = "",
                ManagerNameNew = ""
            };

            PlannerToGcc4Assignment expected = new PlannerToGcc4Assignment
            {
                Gcc4 = "",
                Role = "SCS",
                NameId = 68,
                CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                CreatedBy = "",
                UpdatedBy = "",
                PId = 267763,
                UserId = "",
                ManagerNameNew = ""
            };

            A.CallTo(() => _service.AddOnePlanner(planner)).Returns(expected);

            //Act
            var actionResult = _controller.InsertOnePlanner(planner);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var resultObject = TestHelpers.GetObjectResultContent<PlannerToGcc4Assignment>(actionResult);
            Assert.Equal(expected, resultObject);
        }

        [Fact]
        public void Test_InsertOnePlanner_ReturnsNotFound()
        {
            //Arrange
            PlannerToGcc4Assignment planner = new PlannerToGcc4Assignment
            {
                Gcc4 = "",
                Role = "SCS",
                NameId = 68,
                CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                CreatedBy = "",
                UpdatedBy = "",
                PId = 267763,
                UserId = "",
                ManagerNameNew = ""
            };

            PlannerToGcc4Assignment result = null;
            string expected = "Record Addition Failed!";

            A.CallTo(() => _service.AddOnePlanner(planner)).Returns(result);

            //Act
            var actionResult = _controller.InsertOnePlanner(planner);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var resultObject = ((ObjectResult)actionResult.Result).Value;
            Assert.Equal(expected, resultObject.ToString());
        }

        [Fact]
        public void Test_InsertOnePlanner_ReturnsErrorCode()
        {
            //Arrange
            PlannerToGcc4Assignment planner = new PlannerToGcc4Assignment
            {
                Gcc4 = "",
                Role = "SCS",
                NameId = 68,
                CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                CreatedBy = "",
                UpdatedBy = "",
                PId = 267763,
                UserId = "",
                ManagerNameNew = ""
            };

            PlannerToGcc4Assignment result = new PlannerToGcc4Assignment
            {
                Gcc4 = "",
                Role = "SCS",
                NameId = 68,
                CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                CreatedBy = "",
                UpdatedBy = "",
                PId = -1,
                UserId = "",
                ManagerNameNew = ""
            };

            var expectedStatusCode = new StatusCodeResult(717);

            A.CallTo(() => _service.AddOnePlanner(planner)).Returns(result);

            //Act
            var actionResult = _controller.InsertOnePlanner(planner);

            //Assert
            Assert.IsType<StatusCodeResult>(actionResult.Result);
            var resultObject = ((StatusCodeResult)actionResult.Result);
            Assert.Equal(expectedStatusCode.StatusCode, resultObject.StatusCode);
        }

        [Fact]
        public void Test_UpdateOnePlanner_IsSuccessful()
        {
            //Arrange
            long pid = 267763;

            PlannerToGcc4Assignment result = new PlannerToGcc4Assignment
            {
                Gcc4 = "",
                Role = "SCS",
                NameId = 68,
                CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                CreatedBy = "",
                UpdatedBy = "",
                PId = 267763,
                UserId = "",
                ManagerNameNew = ""
            };

            A.CallTo(() => _service.GetOnePlanner(pid)).Returns(result);
            A.CallTo(() => _service.UpdateOnePlanner(pid, result)).Returns(result);

            //Act
            var actionResult = _controller.UpdateOnePlanner(pid, result);

            //Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public void Test_UpdateOnePlanner_ReturnsNotFound()
        {
            //Arrange
            long pid = 267763;

            PlannerToGcc4Assignment planner = new PlannerToGcc4Assignment
            {
                Gcc4 = "",
                Role = "SCS",
                NameId = 68,
                CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                CreatedBy = "",
                UpdatedBy = "",
                PId = 267763,
                UserId = "",
                ManagerNameNew = ""
            };

            PlannerToGcc4Assignment result = null;

            string expected = "No Record Available to Update!";

            A.CallTo(() => _service.GetOnePlanner(pid)).Returns(result);
            A.CallTo(() => _service.UpdateOnePlanner(pid, planner)).Returns(result);

            //Act
            var actionResult = _controller.UpdateOnePlanner(pid, planner);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult);
            var resultObject = ((ObjectResult)actionResult).Value;
            Assert.Equal(expected, resultObject.ToString());
        }

        [Fact]
        public void Test_UpdateOnePlanner_ReturnsErrorCode()
        {
            //Arrange
            long pid = -1;

            PlannerToGcc4Assignment planner = new PlannerToGcc4Assignment
            {
                Gcc4 = "",
                Role = "SCS",
                NameId = 68,
                CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                CreatedBy = "",
                UpdatedBy = "",
                PId = 267763,
                UserId = "",
                ManagerNameNew = ""
            };

            PlannerToGcc4Assignment result = new PlannerToGcc4Assignment
            {
                Gcc4 = "",
                Role = "SCS",
                NameId = 68,
                CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                CreatedBy = "",
                UpdatedBy = "",
                PId = -1,
                UserId = "",
                ManagerNameNew = ""
            };

            var expectedStatusCode = new StatusCodeResult(717);

            A.CallTo(() => _service.GetOnePlanner(pid)).Returns(result);
            A.CallTo(() => _service.UpdateOnePlanner(pid, planner)).Returns(result);

            //Act
            var actionResult = _controller.UpdateOnePlanner(pid, planner);

            //Assert
            Assert.IsType<StatusCodeResult>(actionResult);
            var resultObject = ((StatusCodeResult)actionResult);
            Assert.Equal(expectedStatusCode.StatusCode, resultObject.StatusCode);
        }

        [Fact]
        public void Test_UpdateManyPlanners_IsSuccessful()
        {
            //Arrange
            PlannerToGcc4AssignmentBulkUpdate bulk = new PlannerToGcc4AssignmentBulkUpdate
            {
                FilterData = new PlannerToGcc4AssignmentFilter
                {
                    Gcc4 = "",
                    Role = "SCS",
                    UserId = "",
                    ManagerNameNew = ""
                },
                UpdateData = new PlannerToGcc4Assignment
                {
                    Gcc4 = "",
                    Role = "SCS",
                    NameId = 68,
                    CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                    UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                    CreatedBy = "",
                    UpdatedBy = "",
                    PId = 267763,
                    UserId = "",
                    ManagerNameNew = ""
                },
                UpdateFlags = new PlannerToGcc4AssignmentBulkUpdateFlags
                {
                    Gcc4 = true,
                    Role = false,
                    User_Id = true
                }
            };

            int result = 1;

            A.CallTo(() => _service.UpdateManyPlanners(bulk)).Returns(result);

            //Act
            var actionResult = _controller.UpdateManyPlanners(bulk);

            //Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public void Test_UpdateManyPlanners_ReturnsErrorCode()
        {
            //Arrange
            PlannerToGcc4AssignmentBulkUpdate bulk = new PlannerToGcc4AssignmentBulkUpdate
            {
                FilterData = new PlannerToGcc4AssignmentFilter
                {
                    Gcc4 = "",
                    Role = "SCS",
                    UserId = "",
                    ManagerNameNew = ""
                },
                UpdateData = new PlannerToGcc4Assignment
                {
                    Gcc4 = "",
                    Role = "SCS",
                    NameId = 68,
                    CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                    UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                    CreatedBy = "",
                    UpdatedBy = "",
                    PId = 267763,
                    UserId = "",
                    ManagerNameNew = ""
                },
                UpdateFlags = new PlannerToGcc4AssignmentBulkUpdateFlags
                {
                    Gcc4 = true,
                    Role = false,
                    User_Id = true
                }
            };

            int result = -1;
            var expectedStatusCode = new StatusCodeResult(717);

            A.CallTo(() => _service.UpdateManyPlanners(bulk)).Returns(result);

            //Act
            var actionResult = _controller.UpdateManyPlanners(bulk);

            //Assert
            Assert.IsType<StatusCodeResult>(actionResult);
            var resultObject = ((StatusCodeResult)actionResult);
            Assert.Equal(expectedStatusCode.StatusCode, resultObject.StatusCode);
        }

        [Fact]
        public void Test_DeleteOnePlanner_IsSuccessful()
        {
            //Arrange
            long pid = 267763;

            PlannerToGcc4Assignment result = new PlannerToGcc4Assignment
            {
                Gcc4 = "",
                Role = "SCS",
                NameId = 68,
                CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                CreatedBy = "",
                UpdatedBy = "",
                PId = 267763,
                UserId = "",
                ManagerNameNew = ""
            };

            A.CallTo(() => _service.GetOnePlanner(pid)).Returns(result);
            A.CallTo(() => _service.DeleteOnePlanner(pid)).DoesNothing();

            //Act
            var actionResult = _controller.DeleteOnePlanner(pid);

            //Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public void Test_DeleteOnePlanner_ReturnsNotFound()
        {
            //Arrange
            long pid = 999999;
            PlannerToGcc4Assignment result = null;
            string expected = "No Record Available to Delete!";

            A.CallTo(() => _service.GetOnePlanner(pid)).Returns(result);
            A.CallTo(() => _service.DeleteOnePlanner(pid)).DoesNothing();

            //Act
            var actionResult = _controller.DeleteOnePlanner(pid);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult);
            var resultObject = ((ObjectResult)actionResult).Value;
            Assert.Equal(expected, resultObject.ToString());
        }

        [Fact]
        public void Test_GetManagerName_ReturnsData()
        {
            //Arrange
            string userId = "";
            string expected = "";

            A.CallTo(() => _service.GetManagerName(userId)).Returns(expected);

            //Act
            var actionResult = _controller.GetManagerName(userId);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var resultObject = TestHelpers.GetObjectResultContent<string>(actionResult);
            Assert.Equal(expected, resultObject);
        }

        [Fact]
        public void Test_GetManagerName_ReturnsNoData()
        {
            //Arrange
            string userId = "";
            string result = null;
            string expected = "No Record Available!";

            A.CallTo(() => _service.GetManagerName(userId)).Returns(result);

            //Act
            var actionResult = _controller.GetManagerName(userId);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var resultObject = ((ObjectResult)actionResult.Result).Value;
            Assert.Equal(expected, resultObject.ToString());
        }

        [Fact]
        public void Test_GetDistGcc4_ReturnsData()
        {
            //Arrange
            List<string> expected = new List<string> { "VALUE1" };

            A.CallTo(() => _service.GetDistGcc4()).Returns(expected);

            //Act
            var actionResult = _controller.GetDistGcc4();

            //Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var resultObject = TestHelpers.GetObjectResultContent<List<string>>(actionResult);
            Assert.Equal(expected, resultObject);
        }

        [Fact]
        public void Test_GetDistGcc4_ReturnsNoData()
        {
            //Arrange
            List<string> result = null;
            string expected = "No Records Available!";

            A.CallTo(() => _service.GetDistGcc4()).Returns(result);

            //Act
            var actionResult = _controller.GetDistGcc4();

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var resultObject = ((ObjectResult)actionResult.Result).Value;
            Assert.Equal(expected, resultObject.ToString());
        }

        [Fact]
        public void Test_GetDistPlanners_ReturnsData()
        {
            //Arrange
            List<string> expected = new List<string> 
            { 
                "",
                "",
                "",
                "" 
            };

            A.CallTo(() => _service.GetDistPlanners()).Returns(expected);

            //Act
            var actionResult = _controller.GetDistPlanners();

            //Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var resultObject = TestHelpers.GetObjectResultContent<List<string>>(actionResult);
            Assert.Equal(expected, resultObject);
        }

        [Fact]
        public void Test_GetDistPlanners_ReturnsNoData()
        {
            //Arrange
            List<string> result = null;
            string expected = "No Records Available!";

            A.CallTo(() => _service.GetDistPlanners()).Returns(result);

            //Act
            var actionResult = _controller.GetDistPlanners();

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var resultObject = ((ObjectResult)actionResult.Result).Value;
            Assert.Equal(expected, resultObject.ToString());
        }


        [Fact]
        public void Test_GetGcc4Codes_ReturnsData()
        {
            //Arrange
            string orderBy = "";
            List<Gcc4Code> expected = new List<Gcc4Code>
            {
                new Gcc4Code
                {
                    Id = 2243,
                    Category = "GCC4_CODE_MAPPING",
                    Key = "GCC4_LIST",
                    Value = "",
                    IsActive = "Y",
                    UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
                }
            };

            A.CallTo(() => _service.GetGCC4CodesList(orderBy)).Returns(expected);

            //Act
            var actionResult = _controller.GetGcc4Codes(orderBy);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var resultObject = TestHelpers.GetObjectResultContent<List<Gcc4Code>>(actionResult);
            Assert.Equal(expected, resultObject);
        }

        [Fact]
        public void Test_GetGcc4Codes_ReturnsNotFound()
        {
            //Arrange
            string orderBy = "";
            List<Gcc4Code> result = null;
            string expected = "No Records Available!";

            A.CallTo(() => _service.GetGCC4CodesList(orderBy)).Returns(result);

            //Act
            var actionResult = _controller.GetGcc4Codes(orderBy);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var resultObject = ((ObjectResult)actionResult.Result).Value;
            Assert.Equal(expected, resultObject.ToString());
        }

        [Fact]
        public void Test_GetGcc4Code_ReturnsData()
        {
            //Arrange
            long id = 2243;

            Gcc4Code expected = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            A.CallTo(() => _service.GetOneGcc4Code(id)).Returns(expected);

            //Act
            var actionResult = _controller.GetGcc4Code(id);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var resultObject = TestHelpers.GetObjectResultContent<Gcc4Code>(actionResult);
            Assert.Equal(expected, resultObject);
        }

        [Fact]
        public void Test_GetGcc4Code_ReturnsNotFound()
        {
            //Arrange
            long id = 9999;
            Gcc4Code result = null;
            string expected = "No Record Available!";

            A.CallTo(() => _service.GetOneGcc4Code(id)).Returns(result);

            //Act
            var actionResult = _controller.GetGcc4Code(id);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var resultObject = ((ObjectResult)actionResult.Result).Value;
            Assert.Equal(expected, resultObject.ToString());
        }

        [Fact]
        public void Test_InsertGcc4Code_ReturnsData()
        {
            //Arrange
            Gcc4Code gcc4 = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            Gcc4Code expected = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            A.CallTo(() => _service.CreateGCC4Code(gcc4)).Returns(expected);

            //Act
            var actionResult = _controller.InsertGcc4Code(gcc4);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var resultObject = TestHelpers.GetObjectResultContent<Gcc4Code>(actionResult);
            Assert.Equal(expected, resultObject);
        }

        [Fact]
        public void Test_InsertGcc4Code_ReturnsNotFound()
        {
            //Arrange
            Gcc4Code gcc4 = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            Gcc4Code result = null;
            string expected = "Record Addition Failed!";

            A.CallTo(() => _service.CreateGCC4Code(gcc4)).Returns(result);

            //Act
            var actionResult = _controller.InsertGcc4Code(gcc4);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var resultObject = ((ObjectResult)actionResult.Result).Value;
            Assert.Equal(expected, resultObject.ToString());
        }

        [Fact]
        public void Test_InsertGcc4Code_ReturnsErrorCode()
        {
            //Arrange
            Gcc4Code gcc4 = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            Gcc4Code result = new Gcc4Code
            {
                Id = -1,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            var expectedStatusCode = new StatusCodeResult(717);

            A.CallTo(() => _service.CreateGCC4Code(gcc4)).Returns(result);

            //Act
            var actionResult = _controller.InsertGcc4Code(gcc4);

            //Assert
            Assert.IsType<StatusCodeResult>(actionResult.Result);
            var resultObject = ((StatusCodeResult)actionResult.Result);
            Assert.Equal(expectedStatusCode.StatusCode, resultObject.StatusCode);
        }

        [Fact]
        public void Test_UpdateGcc4Code_IsSuccessful()
        {
            //Arrange
            long id = 2243;

            Gcc4Code result = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            A.CallTo(() => _service.GetOneGcc4Code(id)).Returns(result);
            A.CallTo(() => _service.EditGCC4Code(id, result)).Returns(result);

            //Act
            var actionResult = _controller.UpdateGcc4Code(id, result);

            //Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public void Test_UpdateGcc4Code_ReturnsNotFound()
        {
            //Arrange
            long id = 9999;

            Gcc4Code gcc4 = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            Gcc4Code result = null;

            string expected = "No Record Available to Update!";

            A.CallTo(() => _service.GetOneGcc4Code(id)).Returns(result);
            A.CallTo(() => _service.EditGCC4Code(id, gcc4)).Returns(result);

            //Act
            var actionResult = _controller.UpdateGcc4Code(id, gcc4);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult);
            var resultObject = ((ObjectResult)actionResult).Value;
            Assert.Equal(expected, resultObject.ToString());
        }

        [Fact]
        public void Test_UpdateGcc4Code_ReturnsErrorCode()
        {
            //Arrange
            long id = 2243;

            Gcc4Code gcc4 = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            Gcc4Code result = new Gcc4Code
            {
                Id = -1,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            var expectedStatusCode = new StatusCodeResult(717);

            A.CallTo(() => _service.GetOneGcc4Code(id)).Returns(gcc4);
            A.CallTo(() => _service.EditGCC4Code(id, gcc4)).Returns(result);

            //Act
            var actionResult = _controller.UpdateGcc4Code(id, gcc4);

            //Assert
            Assert.IsType<StatusCodeResult>(actionResult);
            var resultObject = ((StatusCodeResult)actionResult);
            Assert.Equal(expectedStatusCode.StatusCode, resultObject.StatusCode);
        }

        [Fact]
        public void Test_DeleteGcc4Code_IsSuccessful()
        {
            //Arrange
            long id = 2243;

            Gcc4Code result = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            int affectedRows = 1;

            A.CallTo(() => _service.GetOneGcc4Code(id)).Returns(result);
            A.CallTo(() => _service.DeleteGCC4Code(id)).Returns(affectedRows);

            //Act
            var actionResult = _controller.DeleteGcc4Code(id);

            //Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public void Test_DeleteGcc4Code_ReturnsNotFound()
        {
            //Arrange
            long id = 9999;
            Gcc4Code result = null;
            int affectedRows = 0;
            string expected = "No Record Available to Delete!";

            A.CallTo(() => _service.GetOneGcc4Code(id)).Returns(result);
            A.CallTo(() => _service.DeleteGCC4Code(id)).Returns(affectedRows);

            //Act
            var actionResult = _controller.DeleteGcc4Code(id);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult);
            var resultObject = ((ObjectResult)actionResult).Value;
            Assert.Equal(expected, resultObject.ToString());
        }

        [Fact]
        public void Test_DeleteGcc4Code_ReturnsErrorCode()
        {
            //Arrange
            long id = 2243;

            Gcc4Code result = new Gcc4Code
            {
                Id = -1,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            int affectedRows = 0;
            var expectedStatusCode = new StatusCodeResult(717);

            A.CallTo(() => _service.GetOneGcc4Code(id)).Returns(result);
            A.CallTo(() => _service.DeleteGCC4Code(id)).Returns(affectedRows);

            //Act
            var actionResult = _controller.DeleteGcc4Code(id);

            //Assert
            Assert.IsType<StatusCodeResult>(actionResult);
            var resultObject = ((StatusCodeResult)actionResult);
            Assert.Equal(expectedStatusCode.StatusCode, resultObject.StatusCode);
        }
    }
}