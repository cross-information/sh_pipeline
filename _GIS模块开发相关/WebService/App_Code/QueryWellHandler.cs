using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
// using System.Data.OracleClient;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using Newtonsoft.Json.Linq;

/// <summary>
/// Summary description for ListHandler
/// </summary>
public class QueryWellHandler : IHttpHandler
{
	public QueryWellHandler()
	{
		//
		// TODO: Add constructor logic here
		//
	}

	public void ProcessRequest(HttpContext context)
	{
		context.Response.ContentType = "text/plain";
		JObject result = new JObject();
		JObject wells = new JObject();
		result.Add("wells", wells);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			OracleCommand cmd = conn.CreateCommand();
			cmd.CommandText = "SELECT OBJECTID,NAME,LOCATION,PROJECTID,SDE.ST_AsText(SHAPE) SHAPE FROM TBL_WELL";
			OracleDataReader reader = cmd.ExecuteReader();
			while (reader.Read()) {
				JObject well = new JObject();
				well.Add("id", (int)(decimal)reader["OBJECTID"]);
				well.Add("projectId", (int)(decimal)reader["PROJECTID"]);
				well.Add("name", (string)reader["NAME"]);
				well.Add("location", (string)reader["LOCATION"]);
				string shape = (string)reader["SHAPE"];
				shape = shape.Substring(9);
				shape = shape.Substring(0, shape.Length - 1);
				string[] ss = shape.Split(new char[] { ' ' });
				JArray coords = new JArray();
				coords.Add(decimal.Parse(ss[0].Trim()));
				coords.Add(decimal.Parse(ss[1].Trim()));
				well.Add("coords", coords);
				wells.Add("#" + reader["OBJECTID"].ToString(), well);
			}
			reader.Close();
		}
		context.Response.Write(result.ToString(Newtonsoft.Json.Formatting.Indented, null));
	}

	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
}