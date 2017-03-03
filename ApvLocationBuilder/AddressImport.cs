﻿using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using Apv.MemberExcel.Services;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace ApvLocationBuilder
{
    public class AddressImport
    {
        private static readonly Logger Logger = LogManager.GetLogger("Location");

        public static void GetLocations()
        {
            var addresses = ExcelService.ReadAddresses(@"C:\Users\hsu\OneDrive\APV\Adressen.xlsx")
                .Where(dto => dto.Status == Status.Active
                    && dto.AddressLine1 != null
                    && dto.ZipCode != null
                    && dto.City != null);

            var locations = addresses.Select(ToLocation)
                .Where(dto => dto != null);
            var json = JsonConvert.SerializeObject(locations, new JsonSerializerSettings { Formatting = Formatting.Indented });
            using (var writer = File.CreateText("locations.json"))
            {
                writer.Write(json);
            }
        }

        private static LocationDto ToLocation(AddressDto addressDto)
        {
            var address = $"{addressDto.AddressLine1} , {addressDto.ZipCode} {addressDto.City}";
            var encodedAddress = HttpUtility.UrlEncode(address);

            Logger.Debug($"Check {encodedAddress}");
            using (var client = new HttpClient())
            {
                string json;
                try
                {
                    json = client.GetStringAsync("https://maps.googleapis.com/maps/api/geocode/json?address=" + encodedAddress + "&key=AIzaSyDY0kkJiTPVd2U7aTOAwhc9ySH6oHxOIYM").Result;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return null;
                }
                var obj = JObject.Parse(json);
                if ((string)obj["status"] == "OK" && ((JArray)obj["results"]).Count == 1)
                {
                    var location = ((JArray)obj["results"]).Single()["geometry"]["location"];
                    var lat = (double)location["lat"];
                    var lng = (double)location["lng"];
                    return Map(addressDto, lat, lng);
                }
                Logger.Warn($"Status: {(string)obj["status"]} {((JArray)obj["results"]).Count}");
                return null;
            }
        }

        private static LocationDto Map(AddressDto addressDto, double lat, double lng)
        {
            Logger.Debug($"Map {addressDto.AddressLine1} {lat} {lng}");
            Mapper.Initialize(cfg => cfg.CreateMap<AddressDto, LocationDto>());
            var locationDto = Mapper.Map<LocationDto>(addressDto);
            locationDto.Lat = lat;
            locationDto.Lng = lng;
            return locationDto;
        }
    }
}