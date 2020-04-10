using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace idealista.Model
{
    class Propertyb : IEquatable<Propertyb>
    {
        public const String YES = "YES";
        public const String NO = "NO";
        public static readonly String[] USAGE_ARRAY = new String[] { "Urbano", "Urbanizable", "No urbanizable" };
        public static readonly String[] USAGE_URL_ARRAY = new String[] { "con-terrenos-urbanos", "terrenos-urbanizables", "terrenos-no-urbanizables" };
        public int ID { get; set; }
        public String SourceID { get; set; }
        public int Price { get; set; }
        public String Country { get; set; }
        public String City { get; set; }
        public String PostalCode { get; set; }
        public String URLPost { get; set; }
        public String InternalPrefix { get; set; }
        public String Image { get; set; }
        public String Video { get; set; }
        public String ContactName { get; set; }
        public String ContactEmail { get; set; }
        public String ContactMobile { get; set; }
        public String Title { get; set; }
        public String Address { get; set; }
        public Decimal Lat_Coordinate { get; set; }
        public Decimal Long_Coordinate { get; set; }
        public String Usage { get; set; }
        public String Description { get; set; }
        public int InitialPrice { get; set; }
        public int LastPrice { get; set; }
        public int NumberPriceChanges { get; set; }
        public int Expenses { get; set; }
        public int PriceWithExp { get; set; }
        public String BasicCharacteristics { get; set; }
        public int TotalSurface { get; set; }
        public int SurfaceEdificable { get; set; }
        public int MiniumSellSurface { get; set; }
        public String Access { get; set; }
        public String UrbanisticSituation { get; set; }
        public String Equipment { get; set; }
        public String EquipmentWater
        {
            get
            {
                if (EquipmentWater != null) return EquipmentWater;
                if (Equipment == null) return null;
                if (Equipment.Contains("Agua")) return YES;
                return NO;
            }
            set { this.EquipmentWater = value; }
        }
        public String EquipmentElectricity
        {
            get
            {
                if (EquipmentElectricity != null) return EquipmentElectricity;
                if (Equipment == null) return null;
                if (Equipment.Contains("Electricidad")) return YES;
                return NO;
            }
            set { this.EquipmentElectricity = value; }
        }
        public String EquipmentSewerSystem
        {
            get
            {
                if (EquipmentSewerSystem != null) return EquipmentSewerSystem;
                if (Equipment == null) return null;
                if (Equipment.Contains("Alcantarillado")) return YES;
                return NO;
            }
            set { this.EquipmentSewerSystem = value; }
        }
        public String EquipmentNaturalGus
        {
            get
            {
                if (EquipmentNaturalGus != null) return EquipmentNaturalGus;
                if (Equipment == null) return null;
                if (Equipment.Contains("Gas natural")) return YES;
                return NO;
            }
            set { this.EquipmentNaturalGus = value; }
        }
        public String EquipmentStreetLighting
        {
            get
            {
                if (EquipmentStreetLighting != null) return EquipmentStreetLighting;
                if (Equipment == null) return null;
                if (Equipment.Contains("Alumbrado público")) return YES;
                return NO;
            }
            set { this.EquipmentStreetLighting = value; }
        }
        public String EquipmentPavements
        {
            get
            {
                if (EquipmentPavements != null) return EquipmentPavements;
                if (Equipment == null) return null;
                if (Equipment.Contains("Aceras")) return YES;
                return NO;
            }
            set { this.EquipmentPavements = value; }
        }


        public bool Equals(Propertyb to)
        {
            if (to == null)
            {
                return false;
            }
            if (ReferenceEquals(this, to))
            {
                return true;
            }
            Type type = typeof(Propertyb);
            List<String> ignoreList = new List<String>();
            ignoreList.Add("ID");
            //foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            //{
            //    if (!ignoreList.Contains(pi.Name))
            //    {
            //        object selfValue = type.GetProperty(pi.Name).GetValue(this, null);
            //        object toValue = type.GetProperty(pi.Name).GetValue(to, null);
            //        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
            //        {
            //            return false;
            //        }
            //    }
            //}
            //return true;
            var unequalProperties =
                from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where !ignoreList.Contains(pi.Name) && pi.GetUnderlyingType().IsSimpleType() && pi.GetIndexParameters().Length == 0
                let selfValue = type.GetProperty(pi.Name).GetValue(this, null)
                let toValue = type.GetProperty(pi.Name).GetValue(to, null)
                where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
                select selfValue;
            return !unequalProperties.Any();
        }
        public override bool Equals(Object obj) => Equals(obj as Propertyb);
        public static bool operator ==(Propertyb lhs, Propertyb rhs) => object.Equals(lhs, rhs);
        public static bool operator !=(Propertyb lhs, Propertyb rhs) => !(lhs == rhs);
        public override int GetHashCode() => ID.GetHashCode() ^ SourceID.GetHashCode();
    }
}
