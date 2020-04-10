using idealista.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idealista.Dao
{
    class PropertybDao : BaseDao
    {
        public Boolean IsExist(String sourceID)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) count FROM propertyb WHERE sourceid=@sourceID";
                command.Parameters.Add("sourceid", MySqlDbType.VarChar).Value = sourceID;
                MySqlDataReader dr = command.ExecuteReader();
                dr.Read();
                int count = GetValue<int>(dr["count"]);
                return count > 0;
            }
        }

        public Propertyb Select(String sourceID)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM propertyb WHERE sourceid=@sourceID";
                command.Parameters.Add("sourceid", MySqlDbType.VarChar).Value = sourceID;
                MySqlDataReader dr = command.ExecuteReader();
                Propertyb m = null;
                if (dr.Read())
                {
                    m = new Propertyb();
                    m.ID = GetValue<int>(dr["id"]);
                    m.SourceID = GetValue<String>(dr["sourceid"]);
                    m.Price = GetValue<int>(dr["price"]);
                    m.Country = GetValue<String>(dr["country"]);
                    m.City = GetValue<String>(dr["city"]);
                    m.PostalCode = GetValue<String>(dr["postal_code"]);
                    m.URLPost = GetValue<String>(dr["urlpost"]);
                    m.InternalPrefix = GetValue<String>(dr["internalprefix"]);
                    m.Image = GetValue<String>(dr["image"]);
                    m.Video = GetValue<String>(dr["video"]);
                    m.ContactName = GetValue<String>(dr["contactname"]);
                    m.ContactEmail = GetValue<String>(dr["contactemail"]);
                    m.ContactMobile = GetValue<String>(dr["contactmobile"]);
                    m.Title = GetValue<String>(dr["title"]);
                    m.Address = GetValue<String>(dr["address"]);
                    m.Lat_Coordinate = GetValue<Decimal>(dr["lat_coordinate"]);
                    m.Long_Coordinate = GetValue<Decimal>(dr["long_coordinate"]);
                    m.Usage = GetValue<String>(dr["usage"]);
                    m.Description = GetValue<String>(dr["description"]);
                    m.InitialPrice = GetValue<int>(dr["initialprice"]);
                    m.LastPrice = GetValue<int>(dr["lastprice"]);
                    m.NumberPriceChanges = GetValue<int>(dr["numberpricechanges"]);
                    m.Expenses = GetValue<int>(dr["expenses"]);
                    m.PriceWithExp = GetValue<int>(dr["pricewithexp"]);
                    m.BasicCharacteristics = GetValue<String>(dr["basiccharacteristics"]);
                    m.TotalSurface = GetValue<int>(dr["totalsurface"]);
                    m.SurfaceEdificable = GetValue<int>(dr["surfaceedificable"]);
                    m.MiniumSellSurface = GetValue<int>(dr["minimumsellsurface"]);
                    m.Access = GetValue<String>(dr["access"]);
                    m.UrbanisticSituation = GetValue<String>(dr["urbanisticsituation"]);
                    m.EquipmentWater = GetValue<String>(dr["equipment_water"]);
                    m.EquipmentElectricity = GetValue<String>(dr["equipment_electricity"]);
                    m.EquipmentSewerSystem = GetValue<String>(dr["equipment_sewer_system"]);
                    m.EquipmentNaturalGus = GetValue<String>(dr["equipment_natural_gas"]);
                    m.EquipmentStreetLighting = GetValue<String>(dr["equipment_street_lighting"]);
                    m.EquipmentPavements = GetValue<String>(dr["equipment_pavements"]);
                }
                return m;
            }
        }

        public int Insert(Propertyb m)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO propertyb(sourceid,price,country,city,postal_code,urlpost,internalprefix,image,video,contactname,contactemail,contactmobile,title,address,lat_coordinate,long_coordinate,usage,description,initialprice,lastprice,numberpricechanges,expenses,pricewithexp,basiccharacteristics,totalsurface,surfaceedificable,minimumsellsurface,access,urbanisticsituation,equipment_water,equipment_electricity,equipment_sewer_system,equipment_natural_gas,equipment_street_lighting,equipment_pavements)"
                                                   + " VALUES(@SourceID,@Price,@Country,@City,@PostalCode,@URLPost,@InternalPrefix,@Image,@Video,@ContactName,@ContactEmail,@ContactMobile,@Title,@Address" +
                                                   ",@Lat_Coordinate,@Long_Coordinate,@Usage,@Description,@InitialPrice,@LastPrice,@NumberPriceChanges" +
                                                   ",@Expenses,@PriceWithExp,@BasicCharacteristics,@TotalSurface,@SurfaceEdificable,@MiniumSellSurface,@Access,@UrbanisticSituation" +
                                                   ",@EquipmentWater,@EquipmentElectricity,@EquipmentSewerSystem,@EquipmentNaturalGus,@EquipmentStreetLighting,@EquipmentPavements)";
                command.Parameters.Add("SourceID", MySqlDbType.VarChar).Value = m.SourceID;
                command.Parameters.Add("Price", MySqlDbType.Int32).Value = m.Price;
                command.Parameters.Add("Country", MySqlDbType.VarChar).Value = m.Country;
                command.Parameters.Add("City", MySqlDbType.VarChar).Value = m.City;
                command.Parameters.Add("PostalCode", MySqlDbType.VarChar).Value = m.PostalCode;
                command.Parameters.Add("URLPost", MySqlDbType.VarChar).Value = m.URLPost;
                command.Parameters.Add("InternalPrefix", MySqlDbType.VarChar).Value = m.InternalPrefix;
                command.Parameters.Add("Image", MySqlDbType.VarChar).Value = m.Image;
                command.Parameters.Add("Video", MySqlDbType.VarChar).Value = m.Video;
                command.Parameters.Add("ContactName", MySqlDbType.VarChar).Value = m.ContactName;
                command.Parameters.Add("ContactEmail", MySqlDbType.VarChar).Value = m.ContactEmail;
                command.Parameters.Add("ContactMobile", MySqlDbType.VarChar).Value = m.ContactMobile;
                command.Parameters.Add("Title", MySqlDbType.VarChar).Value = m.Title;
                command.Parameters.Add("Address", MySqlDbType.VarChar).Value = m.Address;
                command.Parameters.Add("Lat_Coordinate", MySqlDbType.Decimal).Value = m.Lat_Coordinate;
                command.Parameters.Add("Long_Coordinate", MySqlDbType.Decimal).Value = m.Long_Coordinate;
                command.Parameters.Add("Usage", MySqlDbType.VarChar).Value = m.Usage;
                command.Parameters.Add("Description", MySqlDbType.Text).Value = m.Description;
                command.Parameters.Add("InitialPrice", MySqlDbType.Int32).Value = m.InitialPrice;
                command.Parameters.Add("LastPrice", MySqlDbType.String).Value = m.LastPrice;
                command.Parameters.Add("NumberPriceChanges", MySqlDbType.String).Value = m.NumberPriceChanges;
                command.Parameters.Add("Expenses", MySqlDbType.String).Value = m.Expenses;
                command.Parameters.Add("PriceWithExp", MySqlDbType.String).Value = m.PriceWithExp;
                command.Parameters.Add("BasicCharacteristics", MySqlDbType.String).Value = m.BasicCharacteristics;
                command.Parameters.Add("TotalSurface", MySqlDbType.String).Value = m.TotalSurface;
                command.Parameters.Add("SurfaceEdificable", MySqlDbType.String).Value = m.SurfaceEdificable;
                command.Parameters.Add("MiniumSellSurface", MySqlDbType.String).Value = m.MiniumSellSurface;
                command.Parameters.Add("Access", MySqlDbType.String).Value = m.Access;
                command.Parameters.Add("UrbanisticSituation", MySqlDbType.String).Value = m.UrbanisticSituation;
                command.Parameters.Add("EquipmentWater", MySqlDbType.String).Value = m.EquipmentWater;
                command.Parameters.Add("EquipmentElectricity", MySqlDbType.String).Value = m.EquipmentElectricity;
                command.Parameters.Add("EquipmentSewerSystem", MySqlDbType.String).Value = m.EquipmentSewerSystem;
                command.Parameters.Add("EquipmentNaturalGus", MySqlDbType.String).Value = m.EquipmentNaturalGus;
                command.Parameters.Add("EquipmentStreetLighting", MySqlDbType.String).Value = m.EquipmentStreetLighting;
                command.Parameters.Add("EquipmentPavements", MySqlDbType.String).Value = m.EquipmentPavements;
                return command.ExecuteNonQuery();
            }
        }

        public int Update(Propertyb m)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE propertyb SET " +
                                            "price=@Price," +
                                            "country=@Country," +
                                            "city=@City," +
                                            "postal_code=@PostalCode," +
                                            "urlpost=@URLPost," +
                                            "internalprefix=@InternalPrefix," +
                                            "image=@Image," +
                                            "video=@Video," +
                                            "contactname=@ContactName," +
                                            "contactemail=@ContactEmail," +
                                            "contactmobile=@ContactMobile," +
                                            "title=@Title," +
                                            "address=@Address," +
                                            "lat_coordinate=@Lat_Coordinate," +
                                            "long_coordinate=@Long_Coordinate," +
                                            "usage=@Usage," +
                                            "description=@Description," +
                                            "initialprice=@InitialPrice," +
                                            "lastprice=@LastPrice," +
                                            "numberpricechanges=@NumberPriceChanges," +
                                            "expenses=@Expenses," +
                                            "pricewithexp=@PriceWithExp," +
                                            "basiccharacteristics=@BasicCharacteristics," +
                                            "totalsurface=@TotalSurface," +
                                            "surfaceedificable=@SurfaceEdificable," +
                                            "minimumsellsurface=@MiniumSellSurface," +
                                            "access=@Access," +
                                            "urbanisticsituation=@UrbanisticSituation," +
                                            "equipment_water=@EquipmentWater," +
                                            "equipment_electricity=@EquipmentElectricity," +
                                            "equipment_sewer_system=@EquipmentSewerSystem," +
                                            "equipment_natural_gas=@EquipmentNaturalGus," +
                                            "equipment_street_lighting=@EquipmentStreetLighting," +
                                            "equipment_pavements=@EquipmentPavements " +
                                            "WHERE sourceid=@SourceID";
                command.Parameters.Add("Price", MySqlDbType.Int32).Value = m.Price;
                command.Parameters.Add("Country", MySqlDbType.VarChar).Value = m.Country;
                command.Parameters.Add("City", MySqlDbType.VarChar).Value = m.City;
                command.Parameters.Add("PostalCode", MySqlDbType.VarChar).Value = m.PostalCode;
                command.Parameters.Add("URLPost", MySqlDbType.VarChar).Value = m.URLPost;
                command.Parameters.Add("InternalPrefix", MySqlDbType.VarChar).Value = m.InternalPrefix;
                command.Parameters.Add("Image", MySqlDbType.VarChar).Value = m.Image;
                command.Parameters.Add("Video", MySqlDbType.VarChar).Value = m.Video;
                command.Parameters.Add("ContactName", MySqlDbType.VarChar).Value = m.ContactName;
                command.Parameters.Add("ContactEmail", MySqlDbType.VarChar).Value = m.ContactEmail;
                command.Parameters.Add("ContactMobile", MySqlDbType.VarChar).Value = m.ContactMobile;
                command.Parameters.Add("Title", MySqlDbType.VarChar).Value = m.Title;
                command.Parameters.Add("Address", MySqlDbType.VarChar).Value = m.Address;
                command.Parameters.Add("Lat_Coordinate", MySqlDbType.Decimal).Value = m.Lat_Coordinate;
                command.Parameters.Add("Long_Coordinate", MySqlDbType.Decimal).Value = m.Long_Coordinate;
                command.Parameters.Add("Usage", MySqlDbType.VarChar).Value = m.Usage;
                command.Parameters.Add("Description", MySqlDbType.Text).Value = m.Description;
                command.Parameters.Add("InitialPrice", MySqlDbType.Int32).Value = m.InitialPrice;
                command.Parameters.Add("LastPrice", MySqlDbType.String).Value = m.LastPrice;
                command.Parameters.Add("NumberPriceChanges", MySqlDbType.String).Value = m.NumberPriceChanges;
                command.Parameters.Add("Expenses", MySqlDbType.String).Value = m.Expenses;
                command.Parameters.Add("PriceWithExp", MySqlDbType.String).Value = m.PriceWithExp;
                command.Parameters.Add("BasicCharacteristics", MySqlDbType.String).Value = m.BasicCharacteristics;
                command.Parameters.Add("TotalSurface", MySqlDbType.String).Value = m.TotalSurface;
                command.Parameters.Add("SurfaceEdificable", MySqlDbType.String).Value = m.SurfaceEdificable;
                command.Parameters.Add("MiniumSellSurface", MySqlDbType.String).Value = m.MiniumSellSurface;
                command.Parameters.Add("Access", MySqlDbType.String).Value = m.Access;
                command.Parameters.Add("UrbanisticSituation", MySqlDbType.String).Value = m.UrbanisticSituation;
                command.Parameters.Add("EquipmentWater", MySqlDbType.String).Value = m.EquipmentWater;
                command.Parameters.Add("EquipmentElectricity", MySqlDbType.String).Value = m.EquipmentElectricity;
                command.Parameters.Add("EquipmentSewerSystem", MySqlDbType.String).Value = m.EquipmentSewerSystem;
                command.Parameters.Add("EquipmentNaturalGus", MySqlDbType.String).Value = m.EquipmentNaturalGus;
                command.Parameters.Add("EquipmentStreetLighting", MySqlDbType.String).Value = m.EquipmentStreetLighting;
                command.Parameters.Add("EquipmentPavements", MySqlDbType.String).Value = m.EquipmentPavements;
                command.Parameters.Add("SourceID", MySqlDbType.VarChar).Value = m.SourceID;
                return command.ExecuteNonQuery();
            }
        }

    }
}
