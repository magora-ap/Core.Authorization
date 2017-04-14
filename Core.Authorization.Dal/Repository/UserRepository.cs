using System;
using System.Collections.Generic;
using System.Linq;
using Core.Authorization.Common.Models.Repository;
using Core.Authorization.Common.Models.Types;
using Core.Dal.Common.Repository;
using Core.Authorization.Dal.Abstract;
using Core.Dal.Common.Models;
using Newtonsoft.Json;
using Npgsql;

namespace Core.Authorization.Dal.Repository
{
    /// <summary>
    ///     Repository for work with users
    /// </summary>
    /// Initial author Sergey Sushenko
    public class UserRepository:BaseRepository<UserModel, Guid>, IUserRepository
    {
        public UserRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override string TableName => "user";
        protected override IEnumerable<ColumnItem> GetColumns()
        {
            yield return new ColumnItem("id", true);
            yield return new ColumnItem("meta_info");
            yield return new ColumnItem("social_data");
            yield return new ColumnItem("birth_date");
            yield return new ColumnItem("create_timestamp");
            yield return new ColumnItem("update_timestamp");
            yield return new ColumnItem("delete_timestamp");
            yield return new ColumnItem("email");
            yield return new ColumnItem("first_name");
            yield return new ColumnItem("gender_id");
            yield return new ColumnItem("is_active");
            yield return new ColumnItem("is_confirm");
            yield return new ColumnItem("last_name");
            yield return new ColumnItem("password_hash");
            yield return new ColumnItem("photo_id");
            yield return new ColumnItem("salt");
            yield return new ColumnItem("time_zone_id");
            yield return new ColumnItem("location_from");
        }

        protected override IEnumerable<ValueItem> GetValues(UserModel model)
        {
            yield return new ValueItem(model.Id)
            {
                IsPrimary = true
            };
            yield return new ValueItem(model.MetaInfo);
            yield return new ValueItem(model.SocialData);
            yield return new ValueItem(model.BirthDate);
            yield return new ValueItem(model.CreateTimestamp);
            yield return new ValueItem(model.UpdateTimestamp);
            yield return new ValueItem(model.DeleteTimestamp);
            yield return new ValueItem(model.Email);
            yield return new ValueItem(model.FirstName);
            yield return new ValueItem(model.GenderId);
            yield return new ValueItem(model.IsActive);
            yield return new ValueItem(model.IsConfirm);
            yield return new ValueItem(model.LastName);
            yield return new ValueItem(model.PasswordHash);
            yield return new ValueItem(model.PhotoId);
            yield return new ValueItem(model.Salt);
            yield return new ValueItem(model.TimeZoneId);
            yield return new ValueItem(model.LocationFrom);
        }

        protected override Guid MapResult(Dictionary<string, object> row)
        {
            if (row == null) return Guid.Empty;
            return Guid.Parse(row.Values.ToString());
        }

        protected override UserModel MapToModel(Dictionary<string, object> row)
        {
            if (row == null) return null;
            return new UserModel
            {
                Id = Guid.Parse(row["id"].ToString()),
                MetaInfo = JsonConvert.DeserializeObject<MetaInfoModel>(row["meta_info"].ToString()),
                BirthDate = string.IsNullOrEmpty(row["birth_date"].ToString()) ? null : (DateTime?)row["birth_date"],
                UpdateTimestamp =
                    string.IsNullOrEmpty(row["update_timestamp"].ToString())
                        ? null
                        : (DateTime?)row["update_timestamp"],
                DeleteTimestamp =
                    string.IsNullOrEmpty(row["delete_timestamp"].ToString())
                        ? null
                        : (DateTime?)row["delete_timestamp"],
                CreateTimestamp = (DateTime)row["create_timestamp"],
                Email = row["email"].ToString(),
                FirstName = row["first_name"].ToString(),
                GenderId =
                    string.IsNullOrEmpty(row["gender_id"].ToString())
                        ? (int?)null
                        : Convert.ToInt32(row["gender_id"].ToString()),
                IsActive = (bool)row["is_active"],
                IsConfirm = (bool)row["is_confirm"],
                LastName = row["last_name"].ToString(),
                LocationFrom = JsonConvert.DeserializeObject<GeographyPoint>(row["location_from"].ToString()),
                PasswordHash = row["password_hash"].ToString(),
                PhotoId =
                    string.IsNullOrEmpty(row["photo_id"].ToString())
                        ? (Guid?)null
                        : Guid.Parse(row["photo_id"].ToString()),
                Salt = row["salt"].ToString(),
                TimeZoneId =
                    string.IsNullOrEmpty(row["time_zone_id"].ToString())
                        ? (int?)null
                        : Convert.ToInt32(row["time_zone_id"].ToString()),
                SocialData = JsonConvert.DeserializeObject<AuthJsonModel>(row["social_data"].ToString())
            };
        }

        public Guid Update(UserModel model)
        {
            return UpdateItem(model);
        }

        public UserModel GetById(Guid id)
        {
            return SelectOneByPrimaryKey(id.ToString());
        }

        public UserModel GetByEmail(string email)
        {
            return SelectOne("email", email);
        }

        public UserModel GetByGoogleId(string id)
        {
            var command = GetSelectAllCommand();
            command += $" WHERE social_data #> '{{GoogleModel,id}}' ='\"{id}\"'";
            return MapToModel(RunCommand(command).FirstOrDefault());
        }

        public UserModel GetByFacebookId(string id)
        {
            var command = GetSelectAllCommand();
            command += $" WHERE social_data #> '{{FacebookModel,id}}' ='\"{id}\"'";
            return MapToModel(RunCommand(command).FirstOrDefault());
        }

        public Guid Create(UserModel model)
        {
            return Insert(model);
        }
    }
}
