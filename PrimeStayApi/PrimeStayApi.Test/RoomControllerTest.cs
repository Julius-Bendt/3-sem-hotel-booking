using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStayApi.Controllers;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Version = PrimeStayApi.Database.Version;

namespace PrimeStayApi.Test
{
    [TestClass]
    public class RoomControllerTest
    {
        private string connectionString = new ENV().ConnectionStringTest;
        private static SqlDataContext _dataContext;
        private static List<Action> _dropDatabaseActions = new();

        [TestInitialize]
        public void SetUp()
        {
            _dataContext = new SqlDataContext(connectionString);
            Version.Upgrade(connectionString);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Parallel.Invoke(_dropDatabaseActions.ToArray());
        }

        [TestCleanup]
        public void CleanUp()
        {
            _dropDatabaseActions.Add(() => Version.Drop(connectionString));
        }

        //[TestMethod]
        //public void TestIndex()
        //{
        //    //Arrange
        //    RoomEntity[] rooms;

        //    //Act
        //    rooms = _controller.Index(null, null, null, null, null, null, null).ToArray();

        //    //Assert
        //    Assert.IsNotNull(rooms);
        //    Assert.IsTrue(rooms.Length > 0);
        //}

        //[TestMethod]
        //public void TestGetRoomsByHotelId()
        //{

        //    //Arrange
        //    RoomEntity[] rooms;

        //    //Act
        //    rooms = _controller.Index(null, null, null, null, null, null, 1).ToArray();

        //    //Assert
        //    Assert.IsNotNull(rooms);
        //    Assert.IsTrue(rooms.Length > 0);



    }
    /*
    [TestMethod]
    public void TestGetRoomById()
    {
        //Arrange
        Room room = null;

        //Act
        room = _controller.Details(1);

        //Assert
        Assert.IsNotNull(room);
    }

    [TestMethod]
    public void TestCreateRoom()
    {
        //Arrange
        var collection = new FormCollection(new Dictionary<string, StringValues>
        {
            { "numOfAvailableRooms", "321" },
            { "numOfAvailableBeds", "123" },
            { "hotelId", "1" },
            { "rating", "5" },
            { "type", "M�lkerummet" },
            { "description", "Ikke for folk der er lactose introlerent. Beklager guys" }
        });

        ActionResult room = null;


        //Act
        room = _controller.Create(collection);

        //Assert
        Assert.IsNotNull(room);
    }


    [TestMethod]
    public void TestUpdateRoom()
    {
        //Arrange
        var collection = new FormCollection(new Dictionary<string, StringValues>
        {
            { "numOfAvailableRooms", "35" },
            { "numOfAvailableBeds", "3" },
            { "hotelId", "1" },
            { "rating", "3" },
            { "type", "Test Suite" },
            { "description", "Kun de helt modige t�r v�re her!" }
        });

        ActionResult room = null;


        //Act
        room = _controller.Edit(1, collection);

        //Assert

        Assert.IsNotNull(room);
    }



    [TestMethod]
    public void TestDeleteRoom()
    {
        //Arrange


        //Act
        var result = _controller.Delete(1);
        Room room = _controller.Details(1);


        //Assert
        Assert.IsNull(room);
    }
}
    */
}
