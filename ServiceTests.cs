using API.Common.Interfaces.Database;
using API.Common.Interfaces.Services;
using API.Common.Models.PlannerToGcc4Assignment;
using API.Services.PlannerToGcc4AssignmentService;
using API.Services.PropertyMappings;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace APIUnitTest.ServicesUnitTests
{
    public class PlannerToGcc4AssignmentServiceTests
    {
        private readonly IDatabase _db;
        private readonly PlannerToGcc4AssignmentService _service;

        public PlannerToGcc4AssignmentServiceTests()
        {
            _db = A.Dummy<IDatabase>();
            IPropertyMappingService _propertyMappingService = new PropertyMappingService();
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger<PlannerToGcc4AssignmentService> _logger = loggerFactory.CreateLogger<PlannerToGcc4AssignmentService>();
            
            _service = new PlannerToGcc4AssignmentService(_db, _propertyMappingService, _logger);
        }


        [Fact]
        public void Test_GetAll_ReturnsData()
        {
            //Arrange
            string tableName = _service.GetQuery();
            string Statement = @$"select * from (select FPTGA.*,rownum as RI from {tableName} FPTGA ORDER BY GCC4 ASC ) ";
            string modelName = "API.Common.Models.PlannerToGcc4Assignment.PlannerToGcc4Assignment";

            PlannerToGcc4Assignment entry = new PlannerToGcc4Assignment
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

            IList<object> fetchedObjects = new List<object>
            {
                (object)entry
            };

            List<PlannerToGcc4Assignment> expected = new List<PlannerToGcc4Assignment>
            {
                entry
            };

            A.CallTo(() => _db.ReturnAsObjectList(Statement, modelName, null)).Returns(fetchedObjects);

            //Act
            var actual = _service.GetAll();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", 1, 50)]
        [InlineData("GCC4-ASC", -1, 49)]
        public void Test_GetAllWithFilter_ReturnsData(string orderBy, long page, long pageSize)
        {
            //Arrange
            long realPage = 1;
            long realPageSize = 50;

            PlannerToGcc4AssignmentFilter filter = new PlannerToGcc4AssignmentFilter()
            {
                Gcc4 = "",
                Role = "SCS"
            };

            string tableName = _service.GetQuery();
            long rowStart = ((realPage - 1) * realPageSize) + 1;
            long rowEnd = (realPage * realPageSize);

            string Statement = @$"select * from (select FPTGA.*,row_number() over (order by GCC4 ASC NULLS LAST) as RI from {tableName} FPTGA where (";
            Statement += _service.FilterQuery(filter, true, out Dictionary<string, object> parameters);
            Statement += $") WHERE RI BETWEEN {rowStart} AND {rowEnd} ";

            string modelName = "API.Common.Models.PlannerToGcc4Assignment.PlannerToGcc4Assignment";

            PlannerToGcc4Assignment entry = new PlannerToGcc4Assignment
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

            IList<object> fetchedObjects = new List<object>
            {
                (object)entry
            };

            List<PlannerToGcc4Assignment> expected = new List<PlannerToGcc4Assignment>
            {
                entry
            };

            A.CallTo(() => _db.ReturnAsObjectList(Statement, modelName, parameters)).WithAnyArguments().Returns(fetchedObjects);

            //Act
            var actual = _service.GetAllWithFilter(orderBy, page, pageSize, filter);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_GetOnePlanner_ReturnsData()
        {
            //Arrange
            long pid = 123;

            var Statement = @$"SELECT ptga.*, ptm.PLANNER_NAME AS USER_ID, ";
            Statement += "NVL(ptm.MANAGER_NAME,'NO_MANAGER') AS MANAGER_NAME_NEW ";
            Statement += "FROM . ptga ";
            Statement += "INNER JOIN . ptm ";
            Statement += "ON ptga.NAME_ID = ptm.PLANNER_NAME_ID ";
            Statement += @$"WHERE ptga.PID = {pid}";

            string modelName = "API.Common.Models.PlannerToGcc4Assignment.PlannerToGcc4Assignment";

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

            A.CallTo(() => _db.ReturnAsObject(Statement, modelName, null)).Returns((object)expected);

            //Act
            var actual = _service.GetOnePlanner(pid);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_AddOnePlanner_ReturnsData()
        {
            //Arrange
            string tableName = $".";
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

            string Statement1 = @$"SELECT PLANNER_NAME_ID from . where PLANNER_NAME = '{planner.UserId}'";
            string Statement2 = "select ..nextval from dual";
            long nameId = 123;
            long pid = 321;

            A.CallTo(() => _db.ReturnAs(Statement1, null)).Returns((object)nameId);
            A.CallTo(() => _db.ReturnAs(Statement2, null)).Returns((object)pid);

            planner.PId = pid;
            planner.CreatedTs = DateTime.UtcNow;
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"GCC4", planner.Gcc4},
                {"ROLE", planner.Role},
                {"NAME_ID", nameId},
                {"CREATED_TS", planner.CreatedTs},
                {"UPDATED_TS", null},
                {"CREATED_BY", planner.CreatedBy},
                {"UPDATED_BY", null},
                {"PID", planner.PId}
            };

            int result = 1;
            A.CallTo(() => _db.Insert(tableName, parameters)).WithAnyArguments().Returns(result);

            var expected = planner;

            //Act
            var actual = _service.AddOnePlanner(planner);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_AddOnePlanner_OracleExceptionReturnsError()
        {
            //Arrange
            string tableName = $".";
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

            string Statement1 = @$"SELECT PLANNER_NAME_ID from . where PLANNER_NAME = '{planner.UserId}'";
            string Statement2 = "select ..nextval from dual";
            long nameId = 123;
            long pid = 321;

            A.CallTo(() => _db.ReturnAs(Statement1, null)).Returns((object)nameId);
            A.CallTo(() => _db.ReturnAs(Statement2, null)).Returns((object)pid);

            planner.PId = pid;
            planner.CreatedTs = DateTime.UtcNow;
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"GCC4", planner.Gcc4},
                {"ROLE", planner.Role},
                {"NAME_ID", nameId},
                {"CREATED_TS", planner.CreatedTs},
                {"UPDATED_TS", null},
                {"CREATED_BY", planner.CreatedBy},
                {"UPDATED_BY", null},
                {"PID", planner.PId}
            };

            var oracleException = new Exception("ORA-00001");
            A.CallTo(() => _db.Insert(tableName, parameters)).WithAnyArguments().Throws(oracleException);
            var expected = planner;
            expected.PId = -1;

            //Act
            var actual = _service.AddOnePlanner(planner);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_AddOnePlanner_ThrowsException()
        {
            //Arrange
            string tableName = $".";
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

            string Statement1 = @$"SELECT PLANNER_NAME_ID from . where PLANNER_NAME = '{planner.UserId}'";
            string Statement2 = "select ..nextval from dual";
            long nameId = 123;
            long pid = 321;

            A.CallTo(() => _db.ReturnAs(Statement1, null)).Returns((object)nameId);
            A.CallTo(() => _db.ReturnAs(Statement2, null)).Returns((object)pid);

            planner.PId = pid;
            planner.CreatedTs = DateTime.UtcNow;
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"GCC4", planner.Gcc4},
                {"ROLE", planner.Role},
                {"NAME_ID", nameId},
                {"CREATED_TS", planner.CreatedTs},
                {"UPDATED_TS", null},
                {"CREATED_BY", planner.CreatedBy},
                {"UPDATED_BY", null},
                {"PID", planner.PId}
            };

            var oracleException = new Exception("ORA-00002");
            A.CallTo(() => _db.Insert(tableName, parameters)).WithAnyArguments().Throws(oracleException);

            //Act
            Action act = () => _service.AddOnePlanner(planner);

            //Assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal(oracleException.Message, exception.Message);
        }

        [Fact]
        public void Test_UpdateOnePlanner_ReturnsData()
        {
            //Arrange
            long pid = 321;
            string tableName = $".";
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

            string Statement1 = @$"SELECT PLANNER_NAME_ID from . where PLANNER_NAME = '{planner.UserId}'";
            long nameId = 123;

            A.CallTo(() => _db.ReturnAs(Statement1, null)).Returns((object)nameId);

            planner.UpdatedTs = DateTime.UtcNow;
            planner.PId = pid;
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"GCC4", planner.Gcc4},
                {"ROLE", planner.Role},
                {"NAME_ID", nameId},
                {"UPDATED_TS", planner.UpdatedTs},
                {"UPDATED_BY", planner.UpdatedBy}
            };

            Dictionary<string, object> whereParameters = new Dictionary<string, object> {
                {"PID", pid}
            };

            int result = 1;
            A.CallTo(() => _db.Update(tableName, parameters, whereParameters)).WithAnyArguments().Returns(result);

            var expected = planner;

            //Act
            var actual = _service.UpdateOnePlanner(pid, planner);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_UpdateOnePlanner_OracleExceptionReturnsError()
        {
            //Arrange
            long pid = 321;
            string tableName = $".";
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

            string Statement1 = @$"SELECT PLANNER_NAME_ID from . where PLANNER_NAME = '{planner.UserId}'";
            long nameId = 123;

            A.CallTo(() => _db.ReturnAs(Statement1, null)).Returns((object)nameId);

            planner.UpdatedTs = DateTime.UtcNow;
            planner.PId = pid;
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"GCC4", planner.Gcc4},
                {"ROLE", planner.Role},
                {"NAME_ID", nameId},
                {"UPDATED_TS", planner.UpdatedTs},
                {"UPDATED_BY", planner.UpdatedBy}
            };

            Dictionary<string, object> whereParameters = new Dictionary<string, object> {
                {"PID", pid}
            };

            var oracleException = new Exception("ORA-00001");
            A.CallTo(() => _db.Update(tableName, parameters, whereParameters)).WithAnyArguments().Throws(oracleException);
            var expected = planner;
            expected.PId = -1;

            //Act
            var actual = _service.UpdateOnePlanner(pid, planner);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_UpdateOnePlanner_ThrowsException()
        {
            //Arrange
            long pid = 321;
            string tableName = $".";
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

            string Statement1 = @$"SELECT PLANNER_NAME_ID from . where PLANNER_NAME = '{planner.UserId}'";
            long nameId = 123;

            A.CallTo(() => _db.ReturnAs(Statement1, null)).Returns((object)nameId);

            planner.UpdatedTs = DateTime.UtcNow;
            planner.PId = pid;
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"GCC4", planner.Gcc4},
                {"ROLE", planner.Role},
                {"NAME_ID", nameId},
                {"UPDATED_TS", planner.UpdatedTs},
                {"UPDATED_BY", planner.UpdatedBy}
            };

            Dictionary<string, object> whereParameters = new Dictionary<string, object> {
                {"PID", pid}
            };

            var oracleException = new Exception("ORA-00002");
            A.CallTo(() => _db.Update(tableName, parameters, whereParameters)).WithAnyArguments().Throws(oracleException);

            //Act
            Action act = () => _service.UpdateOnePlanner(pid, planner);

            //Assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal(oracleException.Message, exception.Message);
        }

        [Fact]
        public void Test_UpdateManyPlanners_ReturnsData()
        {
            //Arrange
            string tableName = $".";

            PlannerToGcc4AssignmentBulkUpdate bData = new PlannerToGcc4AssignmentBulkUpdate
            {
                FilterData = new PlannerToGcc4AssignmentFilter
                {
                    Gcc4 = "",
                    Role = "SCS",
                    UserId = "TEST1",
                    ManagerNameNew = "TEST2"
                },
                UpdateFlags = new PlannerToGcc4AssignmentBulkUpdateFlags
                {
                    Gcc4 = true,
                    Role = true,
                    User_Id = true
                },
                UpdateData = new PlannerToGcc4Assignment
                {
                    Gcc4 = "",
                    Role = "SCS",
                    NameId = 68,
                    CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                    UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                    CreatedBy = "",
                    UpdatedBy = "Gabriel_F",
                    PId = 267763,
                    UserId = "",
                    ManagerNameNew = ""
                }
            };

            bData.UpdateData.UpdatedTs = DateTime.UtcNow;
            
            long plannernameid = 1234;
            string Statement1 = @$"SELECT planner_name_id from . where planner_name = '{bData.UpdateData.UserId}'";
            A.CallTo(() => _db.ReturnAs(Statement1, null)).Returns((object)plannernameid);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            if (bData.UpdateFlags.Gcc4)
            {
                parameters.Add("GCC4_UPD", bData.UpdateData.Gcc4);
            }
            if (bData.UpdateFlags.Role)
            {
                parameters.Add("ROLE_UPD", bData.UpdateData.Role);
            }
            if (bData.UpdateFlags.User_Id)
            {
                parameters.Add("NAME_ID_UPD", plannernameid);
            }
            parameters.Add("UPDATED_TS_UPD", bData.UpdateData.UpdatedTs);
            parameters.Add("UPDATED_BY_UPD", bData.UpdateData.UpdatedBy);

            string Statement2 = @$"SELECT planner_name_id from . where planner_name = '{bData.FilterData.UserId}'";
            A.CallTo(() => _db.ReturnAs(Statement2, null)).Returns((object)plannernameid);

            string Statement = $"update {tableName} set {string.Join(", ", parameters.Keys.Select(x => x.Substring(0, x.Length - 4) + " = :" + x))} where (";
            Statement += _service.FilterQuery(bData.FilterData, false, out Dictionary<string, object> filterParameters);

            foreach (var key in filterParameters.Keys)
            {
                parameters.Add(key, filterParameters[key]);
            }

            int expected = 1;
            A.CallTo(() => _db.CustomBulkUpdate(Statement, parameters)).WithAnyArguments().Returns(expected);

            //Act
            var actual = _service.UpdateManyPlanners(bData);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_UpdateManyPlanners_OracleExceptionReturnsError()
        {
            //Arrange
            string tableName = $".;

            PlannerToGcc4AssignmentBulkUpdate bData = new PlannerToGcc4AssignmentBulkUpdate
            {
                FilterData = new PlannerToGcc4AssignmentFilter
                {
                    Gcc4 = "",
                    Role = "SCS",
                    UserId = "",
                    ManagerNameNew = "TEST2"
                },
                UpdateFlags = new PlannerToGcc4AssignmentBulkUpdateFlags
                {
                    Gcc4 = false,
                    Role = false,
                    User_Id = false
                },
                UpdateData = new PlannerToGcc4Assignment
                {
                    Gcc4 = "",
                    Role = "SCS",
                    NameId = 68,
                    CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                    UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                    CreatedBy = "",
                    UpdatedBy = "Gabriel_F",
                    PId = 267763,
                    UserId = "",
                    ManagerNameNew = ""
                }
            };

            bData.UpdateData.UpdatedTs = DateTime.UtcNow;

            long plannernameid = 1234;
            string Statement1 = @$"SELECT planner_name_id from . where planner_name = '{bData.UpdateData.UserId}'";
            A.CallTo(() => _db.ReturnAs(Statement1, null)).Returns((object)plannernameid);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            if (bData.UpdateFlags.Gcc4)
            {
                parameters.Add("GCC4_UPD", bData.UpdateData.Gcc4);
            }
            if (bData.UpdateFlags.Role)
            {
                parameters.Add("ROLE_UPD", bData.UpdateData.Role);
            }
            if (bData.UpdateFlags.User_Id)
            {
                parameters.Add("NAME_ID_UPD", plannernameid);
            }
            parameters.Add("UPDATED_TS_UPD", bData.UpdateData.UpdatedTs);
            parameters.Add("UPDATED_BY_UPD", bData.UpdateData.UpdatedBy);

            string Statement = $"update {tableName} set {string.Join(", ", parameters.Keys.Select(x => x.Substring(0, x.Length - 4) + " = :" + x))} where (";
            Statement += _service.FilterQuery(bData.FilterData, false, out Dictionary<string, object> filterParameters);

            foreach (var key in filterParameters.Keys)
            {
                parameters.Add(key, filterParameters[key]);
            }

            var oracleException = new Exception("ORA-00001");
            A.CallTo(() => _db.CustomBulkUpdate(Statement, parameters)).WithAnyArguments().Throws(oracleException);
            var expected = -1;

            //Act
            var actual = _service.UpdateManyPlanners(bData);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_UpdateManyPlanners_ThrowsException()
        {
            //Arrange
            string tableName = $".";

            PlannerToGcc4AssignmentBulkUpdate bData = new PlannerToGcc4AssignmentBulkUpdate
            {
                FilterData = new PlannerToGcc4AssignmentFilter
                {
                    Gcc4 = "",
                    Role = "SCS",
                    UserId = null,
                    ManagerNameNew = "TEST2"
                },
                UpdateFlags = new PlannerToGcc4AssignmentBulkUpdateFlags
                {
                    Gcc4 = true,
                    Role = true,
                    User_Id = true
                },
                UpdateData = new PlannerToGcc4Assignment
                {
                    Gcc4 = "",
                    Role = "SCS",
                    NameId = 68,
                    CreatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                    UpdatedTs = DateTime.Parse("2021-11-18T06:42:40.88498"),
                    CreatedBy = "",
                    UpdatedBy = "Gabriel_F",
                    PId = 267763,
                    UserId = "",
                    ManagerNameNew = ""
                }
            };

            bData.UpdateData.UpdatedTs = DateTime.UtcNow;

            long plannernameid = 1234;
            string Statement1 = @$"SELECT planner_name_id from . where planner_name = '{bData.UpdateData.UserId}'";
            A.CallTo(() => _db.ReturnAs(Statement1, null)).Returns((object)plannernameid);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            if (bData.UpdateFlags.Gcc4)
            {
                parameters.Add("GCC4_UPD", bData.UpdateData.Gcc4);
            }
            if (bData.UpdateFlags.Role)
            {
                parameters.Add("ROLE_UPD", bData.UpdateData.Role);
            }
            if (bData.UpdateFlags.User_Id)
            {
                parameters.Add("NAME_ID_UPD", plannernameid);
            }
            parameters.Add("UPDATED_TS_UPD", bData.UpdateData.UpdatedTs);
            parameters.Add("UPDATED_BY_UPD", bData.UpdateData.UpdatedBy);

            string Statement = $"update {tableName} set {string.Join(", ", parameters.Keys.Select(x => x.Substring(0, x.Length - 4) + " = :" + x))} where (";
            Statement += _service.FilterQuery(bData.FilterData, false, out Dictionary<string, object> filterParameters);

            foreach (var key in filterParameters.Keys)
            {
                parameters.Add(key, filterParameters[key]);
            }

            var oracleException = new Exception("ORA-00002");
            A.CallTo(() => _db.CustomBulkUpdate(Statement, parameters)).WithAnyArguments().Throws(oracleException);

            //Act
            Action act = () => _service.UpdateManyPlanners(bData);

            //Assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal(oracleException.Message, exception.Message);
        }

        [Fact]
        public void Test_DeleteOnePlanner_ReturnsNothing()
        {
            //Arrange
            long pid = 123;
            string tableName = $".";

            Dictionary<string, object> whereParameters = new Dictionary<string, object> {
                {"PID", pid}
            };

            int result = 1;
            A.CallTo(() => _db.Delete(tableName, whereParameters)).Returns(result).Once();

            //Act 
            _service.DeleteOnePlanner(pid);

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void Test_RowCountWithFilter_ReturnsData()
        {
            //Arrange
            PlannerToGcc4AssignmentFilter filter = new PlannerToGcc4AssignmentFilter
            {
                Gcc4 = "",
                Role = "SCS",
                UserId = "TEST1",
                ManagerNameNew = "TEST2"
            };

            string tableName = _service.GetQuery();
            string Statement = @$"select count(*) from {tableName} where (";
            Statement += _service.FilterQuery(filter, true, out Dictionary<string, object> parameters);

            long expected = 1;
            A.CallTo(() => _db.ReturnAs(Statement, parameters)).WithAnyArguments().Returns((object)expected);

            //Act
            var actual = _service.RowCountWithFilter(filter);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_GetDistGcc4_ReturnsData()
        {
            //Arrange
            string Statement = "SELECT DISTINCT VALUE FROM . ";
            Statement += " WHERE category = 'GCC4_CODE_MAPPING' AND KEY = 'GCC4_LIST' AND is_active = 'Y'";

            IList<string> result = new List<string>
            {
                "SCS"
            };
            List<string> expected = (List<string>)result;
                
            A.CallTo(() => _db.ReturnAsList(Statement, null)).Returns(result);

            //Act
            var actual = _service.GetDistGcc4();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_GetDistPlanners_ReturnsData()
        {
            //Arrange
            string Statement = "SELECT DISTINCT planner_name FROM .";

            IList<string> result = new List<string>
            {
                "TEST_PLANNER"
            };
            List<string> expected = (List<string>)result;

            A.CallTo(() => _db.ReturnAsList(Statement, null)).Returns(result);

            //Act
            var actual = _service.GetDistPlanners();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_GetManagerName_ReturnsManager()
        {
            //Arrange
            string userId = "TEST1";
            string Statement = @$"SELECT NVL(MANAGER_NAME,'NO_MANAGER') ";
            Statement += "FROM . ";
            Statement += "WHERE upper(PLANNER_NAME) = :userId ";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "userId", userId.ToUpper() }
            };

            string expected = "My_Manager";

            A.CallTo(() => _db.ReturnAs(Statement, parameters)).WithAnyArguments().Returns((object)expected);

            //Act
            var actual = _service.GetManagerName(userId);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_GetManagerName_ReturnsNoManager()
        {
            //Arrange
            string userId = "TEST1";
            string Statement = @$"SELECT NVL(MANAGER_NAME,'NO_MANAGER') ";
            Statement += "FROM . ";
            Statement += "WHERE upper(PLANNER_NAME) = :userId ";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "userId", userId.ToUpper() }
            };

            string result = null;
            string expected = "NO_MANAGER";

            A.CallTo(() => _db.ReturnAs(Statement, parameters)).WithAnyArguments().Returns((object)result);

            //Act
            var actual = _service.GetManagerName(userId);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("GCC4-ASC")]
        public void Test_GetGCC4CodesList_ReturnsData(string orderBy)
        {
            //Arrange
            string Statement = "SELECT * FROM . ";
            Statement += " WHERE category = 'GCC4_CODE_MAPPING' ";
            Statement += "order by VALUE ASC NULLS LAST";
            string modelName = "API.Common.Models.PlannerToGcc4Assignment.Gcc4Code";
            
            Gcc4Code entry = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            IList<object> fetchedObjects = new List<object>
            {
                (object)entry
            };

            List<Gcc4Code> expected = new List<Gcc4Code>
            {
                entry
            };

            A.CallTo(() => _db.ReturnAsObjectList(Statement, modelName, null)).Returns(fetchedObjects);

            //Act
            var actual = _service.GetGCC4CodesList(orderBy);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_GetOneGcc4Code_ReturnsData()
        {
            //Arrange
            long id = 2243;
            string Statement = "SELECT * FROM .  WHERE category = 'GCC4_CODE_MAPPING' AND ID = :id";
            string modelName = "API.Common.Models.PlannerToGcc4Assignment.Gcc4Code";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"id", id}
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

            A.CallTo(() => _db.ReturnAsObject(Statement, modelName, parameters)).WithAnyArguments().Returns((object)expected);

            //Act
            var actual = _service.GetOneGcc4Code(id);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_CreateGCC4Code_ReturnsData()
        {
            //Arrange
            Gcc4Code code = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            string tableName = $".FR_CONFIG";
            string Statement = @$"select ..NEXTVAL from dual";
            long id = 4321;

            A.CallTo(() => _db.ReturnAs(Statement, null)).Returns((object)id);

            code.Id = id;
            code.Category = "GCC4_CODE_MAPPING";
            code.Key = "GCC4_LIST";
            code.IsActive = "Y";
            code.UpdateDate = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"ID", code.Id},
                {"CATEGORY", code.Category},
                {"KEY", code.Key},
                {"VALUE", code.Value.ToUpper()},
                {"IS_ACTIVE", code.IsActive},
                {"UPDATE_DATE", code.UpdateDate}
            };

            int result = 1;
            A.CallTo(() => _db.Insert(tableName, parameters)).WithAnyArguments().Returns(result);

            var expected = code;

            //Act
            var actual = _service.CreateGCC4Code(code);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_CreateGCC4Code_OracleExceptionReturnsError()
        {
            //Arrange
            Gcc4Code code = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            string tableName = $".FR_CONFIG";
            string Statement = @$"select ..NEXTVAL from dual";
            long id = 4321;

            A.CallTo(() => _db.ReturnAs(Statement, null)).Returns((object)id);

            code.Id = id;
            code.Category = "GCC4_CODE_MAPPING";
            code.Key = "GCC4_LIST";
            code.IsActive = "Y";
            code.UpdateDate = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"ID", code.Id},
                {"CATEGORY", code.Category},
                {"KEY", code.Key},
                {"VALUE", code.Value.ToUpper()},
                {"IS_ACTIVE", code.IsActive},
                {"UPDATE_DATE", code.UpdateDate}
            };

            var oracleException = new Exception("ORA-00001");
            A.CallTo(() => _db.Insert(tableName, parameters)).WithAnyArguments().Throws(oracleException);
            var expected = code;
            expected.Id = -1;

            //Act
            var actual = _service.CreateGCC4Code(code);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_CreateGCC4Code_ThrowsException()
        {
            //Arrange
            Gcc4Code code = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            string tableName = $".FR_CONFIG";
            string Statement = @$"select ..NEXTVAL from dual";
            long id = 4321;

            A.CallTo(() => _db.ReturnAs(Statement, null)).Returns((object)id);

            code.Id = id;
            code.Category = "GCC4_CODE_MAPPING";
            code.Key = "GCC4_LIST";
            code.IsActive = "Y";
            code.UpdateDate = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"ID", code.Id},
                {"CATEGORY", code.Category},
                {"KEY", code.Key},
                {"VALUE", code.Value.ToUpper()},
                {"IS_ACTIVE", code.IsActive},
                {"UPDATE_DATE", code.UpdateDate}
            };

            var oracleException = new Exception("ORA-00002");
            A.CallTo(() => _db.Insert(tableName, parameters)).WithAnyArguments().Throws(oracleException);

            //Act
            Action act = () => _service.CreateGCC4Code(code);

            //Assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal(oracleException.Message, exception.Message);
        }

        [Fact]
        public void Test_EditGCC4Code_ReturnsData()
        {
            //Arrange
            Gcc4Code code = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            string tableName = $".FR_CONFIG";
            long id = 4321;
            code.Id = id;
            code.UpdateDate = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"VALUE", code.Value.ToUpper()},
                {"UPDATE_DATE", code.UpdateDate}
            };

            Dictionary<string, object> whereParameters = new Dictionary<string, object> {
                {"CATEGORY", code.Category},
                {"KEY", code.Key},
                {"ID", code.Id},
                {"IS_ACTIVE", code.IsActive}
            };

            int result = 1;
            A.CallTo(() => _db.Update(tableName, parameters, whereParameters)).WithAnyArguments().Returns(result);

            var expected = code;

            //Act
            var actual = _service.EditGCC4Code(id, code);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_EditGCC4Code_OracleExceptionReturnsError()
        {
            //Arrange
            Gcc4Code code = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            string tableName = $".FR_CONFIG";
            long id = 4321;
            code.Id = id;
            code.UpdateDate = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"VALUE", code.Value.ToUpper()},
                {"UPDATE_DATE", code.UpdateDate}
            };

            Dictionary<string, object> whereParameters = new Dictionary<string, object> {
                {"CATEGORY", code.Category},
                {"KEY", code.Key},
                {"ID", code.Id},
                {"IS_ACTIVE", code.IsActive}
            };
            var oracleException = new Exception("ORA-00001");
            A.CallTo(() => _db.Update(tableName, parameters, whereParameters)).WithAnyArguments().Throws(oracleException);
            var expected = code;
            expected.Id = -1;

            //Act
            var actual = _service.EditGCC4Code(id, code);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_EditGCC4Code_ThrowsException()
        {
            //Arrange
            Gcc4Code code = new Gcc4Code
            {
                Id = 2243,
                Category = "GCC4_CODE_MAPPING",
                Key = "GCC4_LIST",
                Value = "",
                IsActive = "Y",
                UpdateDate = DateTime.Parse("0001-01-01T00:00:00")
            };

            string tableName = $".FR_CONFIG";
            long id = 4321;
            code.Id = id;
            code.UpdateDate = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"VALUE", code.Value.ToUpper()},
                {"UPDATE_DATE", code.UpdateDate}
            };

            Dictionary<string, object> whereParameters = new Dictionary<string, object> {
                {"CATEGORY", code.Category},
                {"KEY", code.Key},
                {"ID", code.Id},
                {"IS_ACTIVE", code.IsActive}
            };

            var oracleException = new Exception("ORA-00002");
            A.CallTo(() => _db.Update(tableName, parameters, whereParameters)).WithAnyArguments().Throws(oracleException);

            //Act
            Action act = () => _service.EditGCC4Code(id, code);

            //Assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal(oracleException.Message, exception.Message);
        }

        [Fact]
        public void Test_DeleteGCC4Code_ReturnsData()
        {
            //Arrange
            long id = 4321;
            string tableName = $".FR_CONFIG";

            Dictionary<string, object> whereParameters = new Dictionary<string, object> {
                { "ID", id }
            };

            int expected = 1;
            A.CallTo(() => _db.Delete(tableName, whereParameters)).WithAnyArguments().Returns(expected);

            //Act
            var actual = _service.DeleteGCC4Code(id);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(FilterData))]
        public void Test_FilterQuery_ReturnsData(PlannerToGcc4AssignmentFilter filter, bool isQuery, string expected)
        {
            //Arrange
            long userId = string.IsNullOrWhiteSpace(filter.UserId) ? 0 : Convert.ToInt64(filter.UserId);
            A.CallTo(() => _db.ReturnAs(@$"SELECT planner_name_id from . where planner_name = '{filter.UserId}'", null)).Returns((object)userId);

            //Act
            var actual = _service.FilterQuery(filter, isQuery, out Dictionary<string, object> parameters);

            //Assert
            Assert.Equal(expected, actual);
        }


        public static IEnumerable<object[]> FilterData => new List<object[]>
        {
            new object[] {
                new PlannerToGcc4AssignmentFilter
                {
                    Gcc4 = "",
                    Role = "SCS",
                    ManagerNameNew = "TestManager",
                    UserId = "1234"
                }, true,
                "(upper(GCC4) like :GCC4) AND (upper(ROLE) like :ROLE) AND (upper(USER_ID) like :USER_ID) AND (upper(MANAGER_NAME_NEW) like :MANAGER_NAME_NEW))" },
            new object[] {
                new PlannerToGcc4AssignmentFilter
                {
                    Gcc4 = "",
                    Role = "SCS",
                    ManagerNameNew = "TestManager",
                    UserId = "1234"
                }, false,
                "(upper(GCC4) like :GCC4) AND (upper(ROLE) like :ROLE) AND (NAME_ID = :NAME_ID)) " },
            new object[] {
                new PlannerToGcc4AssignmentFilter
                {
                    Gcc4 = "",
                    Role = "SCS",
                    ManagerNameNew = "TestManager",
                    UserId = "0"
                }, false,
                "(upper(GCC4) like :GCC4) AND (upper(ROLE) like :ROLE) AND (upper(NAME_ID) like :NAME_ID OR NAME_ID is null))" },
            new object[] {
                new PlannerToGcc4AssignmentFilter
                {
                    Gcc4 = null,
                    Role = null,
                    ManagerNameNew = null,
                    UserId = null
                }, true,
                "(upper(GCC4) like :GCC4 OR GCC4 is null) AND (upper(ROLE) like :ROLE OR ROLE is null) AND (upper(USER_ID) like :USER_ID OR USER_ID is null) AND (upper(MANAGER_NAME_NEW) like :MANAGER_NAME_NEW OR MANAGER_NAME_NEW is null))" },
            new object[] {
                new PlannerToGcc4AssignmentFilter
                {
                    Gcc4 = null,
                    Role = null,
                    ManagerNameNew = null,
                    UserId = null
                }, false,
                "(upper(GCC4) like :GCC4 OR GCC4 is null) AND (upper(ROLE) like :ROLE OR ROLE is null))" },
        };
    }
}