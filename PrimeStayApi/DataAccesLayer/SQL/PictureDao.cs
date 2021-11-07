﻿using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class PictureDao : BaseDao<IDataContext>, IDao<PictureEntity>
    {
        public PictureDao(IDataContext dataContext) : base(dataContext)
        {

        }
        public int Create(PictureEntity model)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(PictureEntity model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PictureEntity> ReadAll(PictureEntity model)
        {
            switch (model.Type)
            {
                case "hotel":
                    using (IDbConnection connection = DataContext.Open())
                    {
                        return connection.Query<PictureEntity>($"SELECT * FROM TablePictures " +
                                                            $"INNER JOIN picture ON picture.id = TablePictures.picture_id " +
                                                            $"WHERE type = @Type AND hotel_id = @Hotel_id",
                                                            new { model.Type, model.Hotel_id });

                    };
                case "room":
                    using (IDbConnection connection = DataContext.Open())
                    {
                        return connection.Query<PictureEntity>($"SELECT * FROM TablePictures " +
                                                            $"INNER JOIN picture ON picture.id = TablePictures.picture_id " +
                                                            $"WHERE type = @Type AND room_id = @room_id",
                                                            new { model.Type, model.Room_id });

                    };
                default:
                    throw new System.Exception("Invalid type " + model.Type);

            }

        }

        public PictureEntity ReadById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Update(PictureEntity model)
        {
            throw new System.NotImplementedException();
        }
    }
}
