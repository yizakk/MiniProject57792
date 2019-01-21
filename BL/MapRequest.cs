using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml;

namespace BL
{
    public static class MapRequest
    {
        public static double? Distance = null;

        public static void MapRequestLoop(string TraineeAddress, string TesterAddress)
        {
            DateTime Start = DateTime.Now;
            do
            {
                string origin = TraineeAddress; //כתובת התלמיד 
                string destination = TesterAddress;//כתובת המורה
                string KEY = @"WRxFyZM1sauIAbVb40X37h3RwNbTclYF";
                string url = @"https://www.mapquestapi.com/directions/v2/route" +
                             @"?key=" + KEY +
                             @"&from=" + origin +
                             @"&to=" + destination +
                             @"&outFormat=xml" +
                             @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
                             @"&enhancedNarrative=false&avoidTimedConditions=false";
                //request from MapQuest service the distance between the 2 addresses
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader sreader = new StreamReader(dataStream);
                string responsereader = sreader.ReadToEnd();
                response.Close();
                //the response is given in an XML format
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(responsereader);
                if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0") //we have the expected answer
                {
                    //display the returned distance
                    XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                    double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                    Distance = distInMiles * 1.609344;
                    //Console.WriteLine("Distance In KM: " + distInMiles * 1.609344);
                    //display the returned driving time
                    XmlNodeList formattedTime = xmldoc.GetElementsByTagName("formattedTime");
                    string fTime = formattedTime[0].ChildNodes[0].InnerText;
                   // Console.WriteLine("Driving Time: " + fTime);
                }
                else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
                { //we have an answer that an error occurred, one of the addresses is not found
                    throw new Exception("one of the addresses is incorrect");
                }
                else //busy network or other error...
                {
                    throw new Exception("Maybe the net is busy");
                }
            } while (Distance != null || DateTime.Now < Start.AddSeconds(5) );
        }
    }
}

