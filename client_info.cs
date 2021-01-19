using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
  class client_info : IPlugin
  {
    protected static Dictionary<String, int> statDictionary = null;
    public client_info()
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
      string[] words = request.getPropertyByKey("remoteendpoint").Split(':');

      sb.Append("<html><body><h1>ClientInfo:</h1>");
      sb.Append("<h3>Client IP address "+words[0]+"</h3>");
      sb.Append("<h3>Client Port "+words[1]+"</h3>");
      sb.Append("<h3>Browser Information "+request.getPropertyByKey("user-agent")+"</h3>");
      sb.Append("<h3>Accept-Language "+request.getPropertyByKey("accept-language")+"</h3>");
      sb.Append("<h3>Accept-Encoding "+request.getPropertyByKey("accept-encoding")+"</h3>");
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