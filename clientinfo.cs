using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
  class ClientInfo : IPlugin
  {
    protected static Dictionary<String, int> statDictionary = null;
    public ClientInfo()
    {
      if (statDictionary == null)
      {
        statDictionary = new Dictionary<String, int>();

      }
    }

    public void PreProcessing(HTTPRequest request)
    {
      if (statDictionary.ContainsKey(request.Url))
      {
        statDictionary[request.Url] = (int)statDictionary[request.Url] + 1;
      }
      else
      {
        statDictionary[request.Url] = 1;
      }
    }
    
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      String[] IpAddr = request.getPropertyByKey("remoteendpoint").Split(':');
      sb.Append("<html><body><h1>Client:</h1>");
      sb.Append("Client IP Address : "+IpAddr[0]+"<br>");
      sb.Append("Client Port : "+IpAddr[1]+"<br>");
      sb.Append("Browser Information : "+request.getPropertyByKey("user-agent")+"<br>");
      sb.Append("Accept Language : "+request.getPropertyByKey("accept-language")+"<br>");
      sb.Append("Accept Encoding : "+request.getPropertyByKey("accept-encoding")+"<br>");

      
      sb.Append("</body></html>");
      response = new HTTPResponse(200);
      response.body = Encoding.UTF8.GetBytes(sb.ToString());
      return response;
    }

    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }


  }
}