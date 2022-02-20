using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Utility
{
    public class Address
    {
        public string city { get; set; }
        public string road { get; set; }
        public string cap { get; set; }
        public string province { get; set; }
        public string state { get; set; }
        public string region { get; set; }

        public static Address GetDefault()
        {
            return new Address("", "", "", "");
        }
        public Address(string _city, string _road, string _cap, string _province, string _state = "Italia", string _region = "")
        {
            city = _city;
            road = _road;
            cap = _cap;
            province = _province;
            region = _region;
            state = _state;
        }
        public override string ToString()
        {
            return road + ", " + city + " " + cap + " " + province + (string.IsNullOrEmpty(region) ? "" : ", " + region) + (string.IsNullOrEmpty(state) ? "" : ", " + state);
        }
        public string ToJson()
        {
            try
            {
                return UCode.ConvertToJson(this);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static Address GetAddress(string _json)
        {
            try
            {
                return UCode.ConvertFromJson<Address>(_json);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ToUrl()
        {
            return road.Replace(" ", "+") + ",+" + cap + "+" + city.Replace(" ", "+") + "+" + province + ",+" + (string.IsNullOrEmpty(state) ? "Italia" : state);
        }
        public string GetMapsRoute(Address destination)
        {
            return "https://www.google.com/maps/dir/" + ToUrl() + "/" + destination.ToUrl();
        }
    }
}
