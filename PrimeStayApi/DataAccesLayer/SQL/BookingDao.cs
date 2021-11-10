﻿using Dapper;
using Dapper.Transaction;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class BookingDao : BaseDao<IDataContext<IDbConnection>>, IDao<BookingEntity>
    {
        #region SQL-Queries
        private static readonly string SELECTBOOKINGBYID = @"SELECT * FROM Booking WHERE id = @id";

        private static readonly string SELECTALLBOOKINGS = @"SELECT * FROM Booking WHERE " +
                                                            "id = ISNULL(@id,id)" +
                                                            "AND start_date >= ISNULL(@Start_date, Start_date)" +
                                                            "AND end_date <= ISNULL(@End_date, End_date)" +
                                                            "AND guests LIKE ISNULL(@guests, guests)" +
                                                            "AND room_id = ISNULL(@Room_id, Room_id)" +
                                                            "AND customer_id = ISNULL(@Customer_id, Customer_id)";

        private static readonly string INSERTBOOKINGRETURNID = @"INSERT INTO Booking (Start_date, End_date, Guests,Room_id,Customer_id) " +
                                                                @"OUTPUT INSERTED.id " +
                                                                @"VALUES (@Start_date, @End_date, @Guests,@Room_id,@Customer_id)";

        private static readonly string GETAVAILABLEROOMRANDOM = "SELECT TOP 1 Room.id FROM Room " +
                                                                "WHERE room.room_type_id = @Room_type_id AND Room.id NOT IN " +
                                                                "( " +
                                                                    "SELECT R.id " +
                                                                    "FROM  Booking B " +
                                                                        "JOIN Room R " +
                                                                        "ON B.room_id = R.id " +
                                                                        "WHERE " +
                                                                        "( " +
                                                                            "(B.start_date <= @start_date AND B.end_date >= @start_date) " +
                                                                            "OR (B.start_date < @end_date AND B.end_date >= @end_date) " +
                                                                            "OR (@start_date <= B.start_date AND @end_date >= B.start_date)" +
                                                                        ") " +
                                                                        "AND Room.room_type_id = @room_type_id " +
                                                                ") " +
                                                                "ORDER BY NEWID()";
        #endregion
        public BookingDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }


        public int Create(BookingEntity model)
        {
            var res = -1;

            using (IDbTransaction transaction = DataContext.Open().BeginTransaction())
            {
                model.Room_id = transaction.ExecuteScalar<int>(GETAVAILABLEROOMRANDOM,
                    new { model.Room_type_id, model.Start_date, model.End_date });

                if (model.Room_id is not null && model.Room_id != 0 && model.Room_id != -1)
                {

                    res = transaction.ExecuteScalar<int>(INSERTBOOKINGRETURNID,
                        new { model.Start_date, model.End_date, model.Guests, model.Room_id, model.Customer_id });
                    transaction.Commit();
                }
                else transaction.Rollback();
            };
            return res;
        }

        public int Delete(BookingEntity model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<BookingEntity> ReadAll(BookingEntity model)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<BookingEntity>(SELECTALLBOOKINGS,
                    new { model.Id, model.Start_date, model.End_date, model.Guests, model.Room_id, model.Customer_id });

            };
        }

        public BookingEntity ReadById(int id)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<BookingEntity>(SELECTBOOKINGBYID, new { id });
            }
        }

        public int Update(BookingEntity model)
        {
            throw new System.NotImplementedException();
        }
    }
}
